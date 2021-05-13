using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControllerMysql : MonoBehaviour
{

    public const string audioName = "wings_angel_audio.mp3";

    public AudioSource audioSource;
    public AudioClip audioClip;
    public string soundPath;

    public void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        StartCoroutine(LoadAudio());
    }

    private IEnumerator LoadAudio()
    {
        WWW www = new WWW("http://localhost/sqlconnect/loadAudio.php"); // Get to a page & get information from it 
        yield return www;

        soundPath = www.text.Split('\t')[1];
        Debug.Log("Path:" + www.text.Split('\t')[1]);

        // ################################

        WWW request = GetAudioFromFile(soundPath, audioName);  
        yield return request;

        audioClip = request.GetAudioClip(); // The most important thing (Separate the audioClip from the Path)
        audioClip.name = audioName;

        PlayAudioFile();
    }

    private void PlayAudioFile()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
        audioSource.loop = true;
    }

    private WWW GetAudioFromFile(string path, string filename)
    {
        string audioToLoad = string.Format(path + "{0}", filename);
        WWW request = new WWW(audioToLoad);

        Debug.Log("GetAudioFromFile Mysql -- : " + audioToLoad);
        return request;
    }
}
