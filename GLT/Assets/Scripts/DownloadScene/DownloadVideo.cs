using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
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
            
            if (!string.IsNullOrEmpty(unityWebRequest.error))
            {
                Transform error = GameObject.Find("ErrorMessage").transform.GetChild(0);
                Text errorText = error.GetComponentInChildren<Text>();
                if (error.gameObject.activeSelf)
                {
                    errorText.text += "\nVideo error: " + unityWebRequest.error;
                }
                else
                {
                    errorText.text = "Video error: " + unityWebRequest.error;
                }
                error.gameObject.SetActive(true);
            }
            else
            {


                videoPlayer.source = VideoSource.Url;
                videoPlayer.url = URL;
                videoPlayer.isLooping = true;
                videoPlayer.Play();
            }
        }

    }
}
