/*
 * Copyright 2021 Scott Hwang. All Rights Reserved.
 * This code was modified slightly from ExampleStreaming.cs 
 * in unity-sdk-4.0.0. This continues to be licensed 
 * under the Apache License, Version 2.0 as noted below.
 * 
 * The only significant change is in the function OnRecognize().
 */

/**
* Copyright 2015 IBM Corp. All Rights Reserved.
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

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using IBM.Watson.SpeechToText.V1;
using IBM.Cloud.SDK;
using IBM.Cloud.SDK.Authentication.Iam;
using IBM.Cloud.SDK.Utilities;
using IBM.Cloud.SDK.DataTypes;

public class SpeechToText : MonoBehaviour
{
    public WatsonSettings settings;

    /* I have only included the English language recognition models
     * (UK and US versions). Others can be found at:
     * https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-models#models
     */
    public enum IBM_LanguageModels
    {
        Arabic_Modern_Standard,
        Chinese_Mandarin,
        Dutch,
        English_Australian,
        English_United_Kingdom,
        English_United_States,
        French,
        French_Canadian,
        German,
        Italian,
        Japanese,
        Korean,
        Portuguese_Brazilian,
        Spanish_Argentinian,
        Spanish_Castilian,
        Spanish_Chilean,
        Spanish_Colombian,
        Spanish_Mexican,
        Spanish_Peruvian
    }

    public Hashtable languageModels = new Hashtable
    {
        { IBM_LanguageModels.Arabic_Modern_Standard, "ar-MS_BroadbandModel" },

        { IBM_LanguageModels.Chinese_Mandarin, "zh-CN_BroadbandModel" },

        { IBM_LanguageModels.Dutch, "nl-NL_BroadbandModel" },

        { IBM_LanguageModels.English_Australian, "en-AU_BroadbandModel" },
        { IBM_LanguageModels.English_United_Kingdom, "en-GB_BroadbandModel" },
        { IBM_LanguageModels.English_United_States, "en-US_BroadbandModel" },

        { IBM_LanguageModels.French, "fr-FR_BroadbandModel" },
        { IBM_LanguageModels.French_Canadian, "fr-CA_BroadbandModel" },

        { IBM_LanguageModels.German, "de-DE_BroadbandModel" },

        { IBM_LanguageModels.Italian, "it-IT_BroadbandModel" },

        { IBM_LanguageModels.Japanese, "ja-JP_BroadbandModel" },

        { IBM_LanguageModels.Korean, "ko-KR_BroadbandModel" },

        { IBM_LanguageModels.Portuguese_Brazilian, "pt-BR_BroadbandModel" },

        { IBM_LanguageModels.Spanish_Argentinian, "es-AR_BroadbandModel" },
        { IBM_LanguageModels.Spanish_Castilian, "es-ES_BroadbandModel" },
        { IBM_LanguageModels.Spanish_Chilean, "es-CL_BroadbandModel" },
        { IBM_LanguageModels.Spanish_Colombian, "es-CO_BroadbandModel" },
        { IBM_LanguageModels.Spanish_Mexican, "es-MX_BroadbandModel" },
        { IBM_LanguageModels.Spanish_Peruvian, "es-PE_BroadbandModel" },
    };


    [SerializeField]
    private IBM_LanguageModels model = IBM_LanguageModels.English_United_States;
    private string languageModel;

    private int _recordingRoutine = 0;
    private string _microphoneID = null;
    private AudioClip _recording = null;
    private int _recordingBufferSize = 1;
    private int _recordingHZ = 22050;

    private SpeechToTextService _service;

    public enum ProcessingStatus { Idle, Processing, Processed };
    private ProcessingStatus status;

    [Tooltip("Text field to display the results of speech conversion.")]
    [SerializeField]
    private Text spokenText;

    [SerializeField]
    private InputField targetInputField;

    [SerializeField]
    private GameObject targetGameObject;

    [SerializeField]
    private InputField outputInputField;
    private Text outputText;

    private void Start()
    {
        Debug.Log(gameObject.GetComponent<SpeechToText>().settings);
        LogSystem.InstallDefaultReactors();
        //Runnable.Run(CreateService());
        status = ProcessingStatus.Idle;

        languageModel = (string)languageModels[model];

        outputInputField = gameObject.GetComponent<InputField>();
        if (outputInputField == null)
        {
            outputInputField = gameObject.AddComponent<InputField>();
            outputInputField.textComponent = gameObject.AddComponent<Text>();
        }


        if (outputInputField != null)
        {

        }

        // Active = false;

        //var temp = GameObject.Find("WatsonSettings").GetComponent<WatsonSettings>();
    }

    public IEnumerator CreateService()
    {
        if (string.IsNullOrEmpty(settings.stt_apikey))
        {
            throw new IBMException("Please provide IAM ApiKey for the service.");
        }

        IamAuthenticator authenticator = new IamAuthenticator(apikey: settings.stt_apikey);

        //  Wait for tokendata
        while (!authenticator.CanAuthenticate())
            yield return null;

        _service = new SpeechToTextService(authenticator);
        if (!string.IsNullOrEmpty(settings.stt_serviceUrl))
        {
            _service.SetServiceUrl(settings.stt_serviceUrl);
        }
        _service.StreamMultipart = true;

        Active = false;
        StartRecording();

    }

    public bool ServiceReady()
    {
        return _service != null;
    }

    public ProcessingStatus GetStatus()
    {
        return status;
    }

    public string GetResult()
    {
        status = ProcessingStatus.Idle;
        return spokenText.text;
    }

    public bool Active
    {
        get { return _service.IsListening; }
        set
        {
            if (value && !_service.IsListening)
            {
                _service.RecognizeModel =
                    (string.IsNullOrEmpty(languageModel)
                    ? "en-US_BroadbandModel" : languageModel);
                _service.DetectSilence = true;
                _service.EnableWordConfidence = true;
                _service.EnableTimestamps = true;
                _service.SilenceThreshold = 0.00f;
                _service.MaxAlternatives = 1;
                _service.EnableInterimResults = true;
                _service.OnError = OnError;
                _service.InactivityTimeout = -1;
                _service.ProfanityFilter = false;
                _service.SmartFormatting = true;
                _service.SpeakerLabels = false;
                _service.WordAlternativesThreshold = null;
                _service.StartListening(OnRecognize, OnRecognizeSpeaker);
            }
            else if (!value && _service.IsListening)
            {
                _service.StopListening();
            }
        }
    }

    public void StartRecording()
    {
        Debug.Log("StartRecording");
        if (_recordingRoutine == 0)
        {
            UnityObjectUtil.StartDestroyQueue();
            _recordingRoutine = Runnable.Run(RecordingHandler());
        }
    }

    public void StopRecording()
    {
        if (_recordingRoutine != 0)
        {
            Microphone.End(_microphoneID);
            Runnable.Stop(_recordingRoutine);
            _recordingRoutine = 0;
        }
    }

    private void OnError(string error)
    {
        Active = false;

        Log.Debug("SpeechInput.OnError()", "Error! {0}", error);
    }

    private IEnumerator RecordingHandler()
    {
        Log.Debug("SpeechInput.RecordingHandler()", "devices: {0}", Microphone.devices);
        _recording = Microphone.Start(_microphoneID, true, _recordingBufferSize, _recordingHZ);
        yield return null;      // let _recordingRoutine get set..

        if (_recording == null)
        {
            StopRecording();
            yield break;
        }

        bool bFirstBlock = true;
        int midPoint = _recording.samples / 2;
        float[] samples = null;

        while (_recordingRoutine != 0 && _recording != null)
        {
            int writePos = Microphone.GetPosition(_microphoneID);
            if (writePos > _recording.samples || !Microphone.IsRecording(_microphoneID))
            {
                Log.Error("SpeechInput.RecordingHandler()", "Microphone disconnected.");

                StopRecording();
                yield break;
            }

            if ((bFirstBlock && writePos >= midPoint)
                || (!bFirstBlock && writePos < midPoint))
            {
                // front block is recorded, make a RecordClip and pass it onto our callback.
                samples = new float[midPoint];
                _recording.GetData(samples, bFirstBlock ? 0 : midPoint);

                AudioData record = new AudioData();
                record.MaxLevel = Mathf.Max(Mathf.Abs(Mathf.Min(samples)), Mathf.Max(samples));
                record.Clip = AudioClip.Create("Recording", midPoint, _recording.channels, _recordingHZ, false);
                record.Clip.SetData(samples, 0);
                _service.OnListen(record);
                bFirstBlock = !bFirstBlock;
            }
            else
            {
                // calculate the number of samples remaining until we ready for a block of audio, 
                // and wait that amount of time it will take to record.
                int remaining = bFirstBlock ? (midPoint - writePos) : (_recording.samples - writePos);
                float timeRemaining = (float)remaining / (float)_recordingHZ;

                yield return new WaitForSeconds(timeRemaining);
            }

        }

        yield break;
    }

    private void OnRecognize(SpeechRecognitionEvent result)
    {
        status = ProcessingStatus.Processing;

        if (result != null && result.results.Length > 0)
        {
            foreach (var res in result.results)
            {
                foreach (var alt in res.alternatives)
                {
                    //string text = string.Format("{0} ({1}, {2:0.00})\n", alt.transcript, res.final ? "Final" : "Interim", alt.confidence);
                    string text = string.Format("{0}", alt.transcript);
                    Log.Debug("SpeechInput.OnRecognize()", text);

                    //Once the phrase of speech is final, set the results field to the final text.
                    //Set the processing status to show that it has been processed.
                    if (res.final)
                    {
                        if (spokenText != null)
                        {
                            spokenText.text = text;
                        }
                        if (targetInputField != null)
                        {
                            targetInputField.text = text;
                        }
                        if (targetGameObject != null)
                        {
                            InputField target = targetGameObject.GetComponent<InputField>();
                            if (target != null)
                            {
                                target.text = text;
                            }
                        }
                        status = ProcessingStatus.Processed;
                    }
                }

                if (res.keywords_result != null && res.keywords_result.keyword != null)
                {
                    foreach (var keyword in res.keywords_result.keyword)
                    {
                        Log.Debug("SpeechInput.OnRecognize()", "keyword: {0}, confidence: {1}, start time: {2}, end time: {3}", keyword.normalized_text, keyword.confidence, keyword.start_time, keyword.end_time);
                    }
                }

                if (res.word_alternatives != null)
                {
                    foreach (var wordAlternative in res.word_alternatives)
                    {
                        Log.Debug("SpeechInput.OnRecognize()", "Word alternatives found. Start time: {0} | EndTime: {1}", wordAlternative.start_time, wordAlternative.end_time);
                        foreach (var alternative in wordAlternative.alternatives)
                            Log.Debug("ExampleStreaming.OnRecognize()", "\t word: {0} | confidence: {1}", alternative.word, alternative.confidence);
                    }
                }
            }
        }
    }

    private void OnRecognizeSpeaker(SpeakerRecognitionEvent result)
    {
        if (result != null)
        {
            foreach (SpeakerLabelsResult labelResult in result.speaker_labels)
            {
                Log.Debug("SpeechInput.OnRecognizeSpeaker()", string.Format("speaker result: {0} | confidence: {3} | from: {1} | to: {2}", labelResult.speaker, labelResult.from, labelResult.to, labelResult.confidence));
            }
        }
    }
}
