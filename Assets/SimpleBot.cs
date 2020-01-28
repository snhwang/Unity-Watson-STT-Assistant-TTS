/*
 * Copyright 2019 Scott Hwang. All Rights Reserved.
 * This code was modified from ExampleAssistantV2.cs 
 * in unity-sdk-4.0.0. This continueds to be licensed 
 * under the Apache License, Version 2.0 as noted below.
 * 
 * I added the Watson text-to-speech service and
 * a flag to communicate with SpeechInput.cs.
 * 
 * I also incorporated the use of the chatbot to execute
 * commands as demonstrated by:
 * 
 * https://www.youtube.com/watch?v=OsbV1xqX0hQ
 * https://github.com/IBM/vr-speech-sandbox-cardboard
 * https://developer.ibm.com/patterns/create-a-virtual-reality-speech-sandbox/
 */

/**
* Copyright 2018 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/
#pragma warning disable 0649

using System;
using System.Collections;
using System.Net;
using IBM.Cloud.SDK;
using IBM.Cloud.SDK.Authentication.Iam;
using IBM.Cloud.SDK.Utilities;
using IBM.Watson.Assistant.V2;
using IBM.Watson.Assistant.V2.Model;
using IBM.Watson.TextToSpeech.V1;

using UnityEngine;
using UnityEngine.UI;

namespace IBM.Watson.Examples
{
    public class SimpleBot: MonoBehaviour
    {
        public string tts_apikey;

        #region PLEASE SET THESE VARIABLES IN THE INSPECTOR
        [Space(10)]
        [Tooltip("The IAM apikey.")]
        [SerializeField]
        private string Assistant_apikey; // The apikey for IBM Watson Assistant
        [Tooltip("The service URL (optional). This defaults to \"https://gateway.watsonplatform.net/assistant/api\"")]
        [SerializeField]
        private string serviceUrl;
        [Tooltip("The version date with which you would like to use the service in the form YYYY-MM-DD.")]
        [SerializeField]
        private string versionDate;
        [Tooltip("The assistantId to run the example.")]
        [SerializeField]
        private string assistantId;
        #endregion

        private AssistantService Assistant_service;
        private TextToSpeechService tts_service;

        private bool createSessionTested = false;
        private bool messageTested = false;
        private bool deleteSessionTested = false;
        private string sessionId;

        public string inputSpeech;
        public string _testString;
        public Text inputText;
        public SpeechInput speechStreamer;
        public string processStatus;

        private AudioSource source;
        private IamAuthenticator tts_authenticator;

        private int counter;

        private void Start()
        {
            // Enable TLS 1.2
            //ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

            // Disable old protocols
            //ServicePointManager.SecurityProtocol &= ~(SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11);

            counter = 0;
            LogSystem.InstallDefaultReactors();
            Runnable.Run(CreateService());
            processStatus = "Idle";
            source = gameObject.GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (processStatus == "Process")
            {
                Runnable.Run(ProcessChat());
            }

            // If the bot is finished processing the chat then start listening for more speech
            if (processStatus == "Finished" && !source.isPlaying)
            {
                speechStreamer.Active = true;
            }

        }

        public IEnumerator CreateService()
        {

            if (string.IsNullOrEmpty(Assistant_apikey))
            {
                throw new IBMException("Please provide IAM ApiKey for the service.");
            }

            //  Create credential and instantiate service
//            IamAuthenticator authenticator = new IamAuthenticator(apikey: Assistant_apikey, url: serviceUrl);
            IamAuthenticator authenticator = new IamAuthenticator(apikey: Assistant_apikey);

            //  Wait for tokendata
            while (!authenticator.CanAuthenticate())
                yield return null;

            Assistant_service = new AssistantService(versionDate, authenticator);

            Assistant_service.SetServiceUrl(serviceUrl);

            if (string.IsNullOrEmpty(tts_apikey))
            {
                throw new IBMException("Please provide IAM ApiKey for the service.");
            }


            //  Create credential and instantiate service
            tts_authenticator = new IamAuthenticator(apikey: tts_apikey);

            //  Wait for tokendata
            while (!tts_authenticator.CanAuthenticate())
                yield return null;

            tts_service = new TextToSpeechService(tts_authenticator);

            // Set speech processing status to "Processing"
            processStatus = "Processing";

            // Create services
            Runnable.Run(speechStreamer.CreateService());

            // Ignore input speech while the bot is speaking.
            speechStreamer.Active = false;


            Assistant_service.CreateSession(OnCreateSession, assistantId);

            while (!createSessionTested)
            {
                yield return null;
            }

            messageTested = false;
            var inputMessage = new MessageInput()
            {
                Text = inputSpeech,
                Options = new MessageInputOptions()
                {
                    ReturnContext = true
                }
            };

            Assistant_service.Message(OnMessage, assistantId, sessionId);
            while (!messageTested)
            {
                messageTested = false;
                yield return null;
            }

 
            //_testString = "I am Bob";
//            if (!String.IsNullOrEmpty(_testString))
//            {
                byte[] synthesizeResponse = null;
                AudioClip clip = null;
                tts_service.Synthesize(
                    callback: (DetailedResponse<byte[]> response, IBMError error) =>
                    {
                        synthesizeResponse = response.Result;
                        clip = WaveFile.ParseWAV("myClip" + counter.ToString(), synthesizeResponse);
                        PlayClip(clip);
                    },
                    text: _testString,
                    //voice: "en-US_AllisonV3Voice",
                    voice: "en-US_MichaelV3Voice",
                    //voice: "en-US_MichaelVoice",
                    accept: "audio/wav"
                );

                while (synthesizeResponse == null)
                    yield return null;

                counter++;

                processStatus = "Finished";

                yield return new WaitForSeconds(clip.length);

//            }

        }

        private IEnumerator ProcessChat()
        {

            if (source.isPlaying)
            {
                yield return null;
            }
            
            while (processStatus != "Process")
            {
                yield return null;
            }
            
            // When processing the chat, ignore input speech
            if (processStatus == "Process")
            {
                speechStreamer.Active = false;
            }

            processStatus = "Processing";


            if (String.IsNullOrEmpty(inputSpeech))
            {
                yield return null;
            }

            messageTested = false;
            var inputMessage = new MessageInput()
            {
                Text = inputSpeech,
                Options = new MessageInputOptions()
                {
                    ReturnContext = true
                }
            };


            Assistant_service.Message(OnMessage, assistantId, sessionId, input: inputMessage);

            while (!messageTested)
            {
                messageTested = false;
                yield return null;
            }


            //_testString = "I am Bob";
            if (!String.IsNullOrEmpty(_testString))
            {
                byte[] synthesizeResponse = null;
                AudioClip clip = null;
                tts_service.Synthesize(
                    callback: (DetailedResponse<byte[]> response, IBMError error) =>
                    {
                        synthesizeResponse = response.Result;
                        clip = WaveFile.ParseWAV("myClip" + counter.ToString(), synthesizeResponse);
                        PlayClip(clip);
                    },
                    text: _testString,
                    //voice: "en-US_AllisonV3Voice",
                    voice: "en-US_MichaelV3Voice",
                    //voice: "en-US_MichaelVoice",
                    accept: "audio/wav"
                );

                while (synthesizeResponse == null)
                    yield return null;

                counter++;

                // Set the flag to know that speech processing has finished
                processStatus = "Finished";

                yield return new WaitForSeconds(clip.length);

            }
        }

        private void OnDeleteSession(DetailedResponse<object> response, IBMError error)
        {
            deleteSessionTested = true;
        }

        private void OnMessage(DetailedResponse<MessageResponse> response, IBMError error)
        {
            _testString = response.Result.Output.Generic[0].Text.ToString();
            messageTested = true;
        }

        private void OnCreateSession(DetailedResponse<SessionResponse> response, IBMError error)
        {
            Log.Debug("SimpleBOt.OnCreateSession()", "Session: {0}", response.Result.SessionId);
            sessionId = response.Result.SessionId;
            createSessionTested = true;
        }

        private void PlayClip(AudioClip clip)
        {
            if (Application.isPlaying && clip != null)
            {
                //GameObject audioObject = new GameObject("AudioObject");
                //AudioSource source = audioObject.AddComponent<AudioSource>();

                source.spatialBlend = 0.0f;
                source.loop = false;
                source.clip = clip;
                source.Play();
                //Destroy(audioObject, clip.length);
            }
        }

    }

}
