using IBM.Cloud.SDK.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatUnit : MonoBehaviour
{
    [SerializeField]
    private WatsonSettings settings;

    // Start is called before the first frame update
    void Start()
    {
        SpeechToText stt = GetComponentInChildren<SpeechToText>();
        TextToSpeech tts = GetComponentInChildren<TextToSpeech>();
        SimpleBot chat = GetComponentInChildren<SimpleBot>();

        stt.settings = settings;
        Runnable.Run(stt.CreateService());

        tts.settings = settings;
        Runnable.Run(tts.CreateService());

        chat.settings = settings;
        Runnable.Run(chat.CreateService());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
