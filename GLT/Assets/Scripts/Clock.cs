using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    public Text date;

    // Update is called once per frame
    void Update()
    {
        var date1 = DateTime.Now;
        date.text = date1.ToString("dd-MM-yyyy HH:mm:ss");
    }
}
