using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;


namespace AzureSuiteForUnity.CognitiveServices.BingSpeech
{
    public class BingSpeechAPIManager : CognitiveServicesBehaviour<BingSpeechAPIManager>, ICognitiveServicesBehaviour
    {
        private IBingSpeechAPI _bingSpeechAPI { get; set; }
        public AudioClip audioGenerated;
        public string APIKey;

        public void Start()
        {
            _bingSpeechAPI = CognitiveServicesServiceFactory.Instance.GetBingSpeechAPI(APIKey);
            //new BingSpeechAPI("apikey", this);

            //_bingSpeechAPI.TextToSpeechAsync("Hello There");
            _bingSpeechAPI.OnRecognise += _bingSpeechAPI_OnRecognise;
            //_bingSpeechAPI.RecogniseAsync(_audioToRecognise);
            //StartCoroutine(RecordSeconds(1));
        }

        public void OnEnable()
        {
            StartCoroutine(RecordSeconds(2));
        }

        private void _bingSpeechAPI_OnRecognise(IBingSpeechAPI sender, RecogniseEventArgs args)
        {
            Debug.Log(args.JsonResponse);
            BingSpeechResponse response = BingSpeechResponse.CreateFromJSON(args.JsonResponse);
            if (response.header.name != null)
            {
                BroadcastMessage("OnResponse", response);
            }
        }

        private void _bingSpeechAPI_OnTextToSpeech(IBingSpeechAPI sender, TextToSpeechEventArgs args)
        {
            Debug.Log("Got response");
            audioGenerated = args.GeneratedAudio;
        }



        public AudioClip _audioToRecognise;
        private int _RATE = 16000;
        private int _SECONDS = 3;
        private AudioClip _recordingClip;

	    public IEnumerator RecordSeconds(int seconds)
        {
            Debug.Log("Recording... ");
            _recordingClip = Microphone.Start(null, false, seconds, _RATE);

            Debug.Log("Recording... STARTED");
            yield return new WaitForSeconds(seconds);
            Debug.Log("Recording... DONE");

            // Write the audio to disk
            Debug.Log("Saving...");
            //var wavData = new WavData(_recordingClip);
            //wavData.Save("c:\\projects\\vaughan\\test.wav");

            _bingSpeechAPI.RecogniseAsync(_recordingClip);
            Debug.Log("Saving... DONE");
        }
    }

}