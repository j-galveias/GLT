using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WeatherController : MonoBehaviour
{
    public string API_KEY; //7f587106d1ca6ee81d60278ba07bff0d

    public Text normalTemperature;
    public Text maxTemperature;
    public Text minTemperature;
    public List<GameObject> weatherIcons;

    string locationCode;

    public void WeatherButtonClick(string code)
    {
        locationCode = code;
        StartCoroutine("GetNewWeather");
    }

    IEnumerator GetNewWeather()
    {
        UnityWebRequest unityWebRequest = UnityWebRequest.Get("http://api.openweathermap.org/data/2.5/weather?id=" 
            + locationCode 
            + "&APPID=" 
            + API_KEY 
            + "&units=metric");

        yield return unityWebRequest.SendWebRequest();

        if (!string.IsNullOrEmpty(unityWebRequest.error))
        {
            Transform error = GameObject.Find("ErrorMessage").transform.GetChild(0);
            Text errorText = error.GetComponentInChildren<Text>();
            errorText.text = unityWebRequest.error;
            error.gameObject.SetActive(true);
        }
        else
        {
            var json = unityWebRequest.downloadHandler.text;

            WeatherContainer weather = JsonUtility.FromJson<WeatherContainer>(json);

            UpdateWeather(weather);
        }
    }

    void UpdateWeather(WeatherContainer weather)
    {
        normalTemperature.text = weather.main.temp.ToString() + "°C";
        minTemperature.text = weather.main.temp_min.ToString() + "°C";
        maxTemperature.text = weather.main.temp_max.ToString() + "°C";

        foreach(var icon in weatherIcons)
        {
            if (icon.name.Equals(weather.weather[0].main))
            {
                icon.SetActive(true);
            }
            else
            {
                icon.SetActive(false);
            }
        }
    }
}
