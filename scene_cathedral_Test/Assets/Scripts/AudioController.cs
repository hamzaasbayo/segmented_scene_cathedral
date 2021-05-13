using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public const string audioName = "wings_angel_audio.mp3";

    public AudioSource audioSource;
    public AudioClip audioClip;
    public string soundPath;

    public void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        soundPath = "file://" + Application.streamingAssetsPath + "/sound/";

        StartCoroutine(LoadAudio());
    }

    private IEnumerator LoadAudio()
    { 
        WWW request = GetAudioFromFile(soundPath, audioName);
        yield return request;

        audioClip = request.GetAudioClip(); 
        audioClip.name = audioName;

        PlayAudioFile();
    }

    private void PlayAudioFile()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
        audioSource.loop = true;
    }

    private WWW GetAudioFromFile (string path, string filename)
    {
        Debug.Log("Path -- : " + path);
        Debug.Log("Filename -- : " + filename);

        string audioToLoad = string.Format(path + "{0}", filename);

        Debug.Log("GetAudioFromFile -- : " + audioToLoad);
        WWW request = new WWW(audioToLoad);

        return request;
    }
}