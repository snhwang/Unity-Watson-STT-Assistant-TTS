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
        tts.settings = settings;
        chat.settings = settings;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
