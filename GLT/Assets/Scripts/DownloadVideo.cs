using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Video;

public class DownloadVideo : MonoBehaviour
{
    public string URL;
    public VideoPlayer videoPlayer;

    public void LoadNewVideo()
    {
        StartCoroutine("GetNewVideo");
    }

    IEnumerator GetNewVideo()
    {
        using (UnityWebRequest unityWebRequest = UnityWebRequest.Get(URL))
        {
            yield return unityWebRequest.SendWebRequest();


            videoPlayer.source = VideoSource.Url;
            videoPlayer.url = URL;
            videoPlayer.isLooping = true;
            videoPlayer.Play();

        }

    }
}
