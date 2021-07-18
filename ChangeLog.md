07/18/2021
Unity is update to 2019.4.28f1. Watson is updated to unity-sdk-5.1.1 and unity-sdk-core-1.2.4.

You can now choose more language models (other than US English) by selecting the SpeechToText prefab and choosing from the Model dropdown.

You can now choose from more speaking voices for different languages by selecting the TextToSpeech prefab and choosing from the Voice dropdown.

I moved all the Assets to a folder called "Unity-Watson-Chatbot" to help you keep the files organized and separate from your files.

I also created a .unitypackage file that you can now download from Releases and import into your Unity package. You may have to go to Edit -> Project Settings.. in the Unity menu and then select Player Settings. Api compatibility must be set to .NET 4.x.

05/26/2021

I updated to Unity2019.4.26f1. The Watson unity-sdk-core sdk was updated to 1.2.3. There was an error with a missing WatsonSettings in the ChatUnit gameobject which was fixed. I verifed that this work with the latest UMA avatar and Salsa.

05/26/2021

I updated to Unity2019.4.26f1. The Watson unity-sdk-core sdk was updated to 1.2.3. There was an error with a missing WatsonSettings in the ChatUnit gameobject which was fixed. I verifed that this work with the latest UMA avatar and Salsa.

03/29/2021

There is a new Unity scene called MultiChatbot. This contains 2 "bot" GameObjects which have their own chatbot units (speech-to-text, chat, and text-to-speech). Each bot has a separate WatsonSettings file. Right now, the API keys and other credentials are the same for each of the WatsonSettings, but theoretically each bot could have different settings so that they provide different conversations. One bot has a male voice and the other bot has a female voice. Each bot only has a colored cube as an avatar. You can replace the cube with UMA or other 3D model. SALSA lipsync or other lipsync software can be added but is not included since these are not free items. I will have a version with Oculus Lipsync and talking heads uploaded to a different repo in the near future. The player GameObject is represented by a white sphere and contains the main camera. You can move around using the left/right arrow keys for rotation and up/down arrwos for translation forward/backward. Only the bot that is nearest to the player will respond to spoken speech. However, the bot must be within a minimum distance before it will respond. The distance threshold is currently set to 10. It can be changed by the parameter field in the GameObject MultiBotControl. I will find time to provide better documentation.

If I messed something up with the other scenes, the previous version can be found in this repo under the branch "Unity2019.4.3-unity-sdk-5.0.1-core-1.2.1".

This new master branch is upgraded to Unity2019.4.23f1, the IBM Watson unity-sdk-5.0.2 and unity-sdk-core-1.2.2.

12/08/2020
I modified the file SimpleBot.cs so that the chatbot responds with "I don't know how to respond to that" if Assistant does not return a usable response.

11/18/2020
Unity-sdk-4.7.1 does not work anymore with the current IBM Watson Assistant, at least not with this Unity project. I replaced it with unity-sdk-4.8.0. If you have significantly modified the project, you may want to upgrade your version by yourself. You just need to delete the unity-sdk-4.7.1 folder in assets, download 4.8.0 ( https://github.com/watson-developer-cloud/unity-sdk/releases ), and copy it into Assets.

I was asked how to change the voice of the chatbot. You just need to select the gameobject in the Unity Hierarchy with the TextToSpeech script in it. In this project, the gameobject is usually called TextToSpeech. Then, go to the Unity Inspector to find the TextToSpeech (Script). There is a dropdown menu labeled Voice. You can change the voice in the menu. The voices labeled with a V3 sound better but take longer to process. They result in a greater lag before you hear the spoken response, especiallly if the response is long.

11/17/2020

Some people have been trying to use the inccorect Service URL parameter for the Watson Assistant credentials in the Unity project. You should use the Service URL found at the IBM Cloud website on the page when you first open the specific Assistant as shown here:

![Correct_Assistant_URL](Correct_Assistant_Service_URL.jpg)

The URL found on the "Assistant settings" page under "API details" is not correct (for this Unity project) although it is labeled "Assistant URL":

![Wrong_Assistant_URL](Wrong_Assistant_Service_URL.jpg)

10/11/2020
I created a new YouTube video which just focuses on how to set up the text-to-speech portion of this project: https://youtu.be/Yrtgig6qdhU

07/05/2020
Updated Unity to latest LTS version: 2019.4.3f1. Also, updated to the latest IBM SDKs: unity-sdk-4.7.1 and unity-sdk-core-1.2.1. It seems to work without any other changes.

04/04/2020

It looks like the IBM cloud API documentation has been updated, at least for the portions of interest (text-to-speech, speech-to-text, and Assistant). Other sections are also likely updated. It now has code for multiple languages including C#/Unity that you can just copy with a convenient button and paste into your project. In the near future, I will check the code I obtained from examples contained in their earlier version of the SDK and clean it up where needed. It seems that the latest version for Assistant is now 2020-04-01, so I will also update this. I wish they had this last year! But, I still appreciate it. I was able to reproduce the Watson functionality of this Unity project in a Go project in less than an hour even though I hardly know the languages.

03/30/2020

I have a first draft of a tutorial showing how to incorporate the Unity Multipurpose Avatar (UMA 2) and SALSA lipsync to create a 3D chatbot. I used the text-to-speech services in this project to generate the audio for the tutorial:

https://youtu.be/m39KGVmi0GI

3/28/2020
The Unity project was updated to Unity 2019.3.7f1.

The ibm Unity SDK was updated to the latest version 4.5.0. The Unity SDK core is still at 1.2.0.

I've added a small menu to the Unity Editor call SNH-Watson. This will create a scriptable object named WatsonSetting.asset. This will be the the Assets/Resources folder. All the API keys and other credentials are now placed in this asset. This is more convenient but if you delete it by accident, the link to the prefabs is lost. Just create a new WatsonSettings file with the menu and drag and drop it into the settings slot in each of the prefabs for text-to-speech, speech-to-text, and chat in the inspector.

I've been trying to figure out how to keep API keys safe if an app is distributed. So, I'm turning this into something you can run from a server where all the API keys can be kept away from client apps. So far, I've only made this work for text-to-speech.



03/18/2020
I've tried to clean up the code and make it more modular with prefabs for chat, text-to-speech, and speech-to-text. I hope this makes it easier to use. I made use of InputFields to trigger the transfer for text/string data between the modules.

There is now a ChangeLog.txt file where I will keep track of older notices and changes.

The README below needs a lot of updating. It's unchanged since the previous version which is now store in branch v1.

I've started working on a brief tutorial to show how to hook up 3D characters/avatars with Salsa Lipsync to the audio output of text-to-speech.


01/28/2020
As of 01/28/2020 7PM CDT time zone, this Unity Project works with Unity 2017.2.8f1, IBM Unity SDK 4.1.1, and Unity SDK core 1.2.0. I'm pretty sure it will work with IBM Unity SDK 4.3.0. You just need to delete the unity-sdk-4.1.1 folder and replace it with unity-sdk-4.3.0. I fixed the mistake of not using the Watson service URLs correctly. So, it should now work with different IBM cloud regions. I've only tried it using the Dallas, Washington DC, and London regions. The video clips and pics for the instructions in this README file still need to be updated, but I did modify the text to explain what needs to be done to set this up.

