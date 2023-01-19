using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoadImage : MonoBehaviour
{
    public string URL;
    public Image image;
    
    public void LoadNewImage()
    {
        StartCoroutine("GetNewImage");
    }

    IEnumerator GetNewImage()
    {
        using (UnityWebRequest unityWebRequest = UnityWebRequestTexture.GetTexture(URL))
        {
            yield return unityWebRequest.SendWebRequest();

            image.material.mainTexture = DownloadHandlerTexture.GetContent(unityWebRequest);
            image.gameObject.SetActive(true);
        }
    }
}
