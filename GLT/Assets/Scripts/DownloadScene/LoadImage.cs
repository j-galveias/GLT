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

            if (!string.IsNullOrEmpty(unityWebRequest.error))
            {
                Transform error = GameObject.Find("ErrorMessage").transform.GetChild(0);
                Text errorText = error.GetComponentInChildren<Text>();
                if (error.gameObject.activeSelf)
                {
                    errorText.text += "\nImage error: " + unityWebRequest.error;
                }
                else
                {
                    errorText.text = "Image error: " + unityWebRequest.error;
                }
                error.gameObject.SetActive(true);
            }
            else
            {

                image.material.mainTexture = DownloadHandlerTexture.GetContent(unityWebRequest);
                image.gameObject.SetActive(true);
            }
        }
    }
}
