using System;
using System.Collections.Generic;

[Serializable]
public class WeatherContainer
{
    public string name;
    public List<Weather> weather;
    public Temperatures main;
}

[Serializable]
public class Weather
{
    public string main;
}

[Serializable]
public class Temperatures
{
    public int temp;
    public int temp_min;
    public int temp_max;
}
