using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour
{
    public bool isNight;
    public bool isSun;
    public int dayNum;
    public Light sun;
    public float secondsInFullDay = 120f;
    [Range(0, 1)]
    public float currentTimeOfDay = 0;
    [HideInInspector]
    public float timeMultiplier = 1f;
    public float rotation = 170f;

    float sunInitialIntensity;

    void Start()
    {
        dayNum = 0;
        sun = GetComponent<Light>();
        sunInitialIntensity = sun.intensity;
    }

    void Update()
    {
        if(isSun)
        {
            UpdateSun();
        }
        else
        {
            UpdateMoon();
        }
        

        currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;

        if (currentTimeOfDay >= 1)
        {
            dayNum++;
            currentTimeOfDay = 0;
        }
    }

    void UpdateSun()
    {
        sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90, rotation, 0);

        float intensityMultiplier = 1;
        if (currentTimeOfDay <= 0.23f || currentTimeOfDay >= 0.75f)
        {
            isNight = true;
            intensityMultiplier = 0;
        }
        else if (currentTimeOfDay <= 0.25f)
        {
            isNight = false;
            intensityMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.23f) * (1 / 0.02f));
        }
        else if (currentTimeOfDay >= 0.73f)
        {
            isNight = false;
            intensityMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - 0.73f) * (1 / 0.02f)));
        }

        sun.intensity = sunInitialIntensity * intensityMultiplier;
    }

    void UpdateMoon()
    {
        DayNightCycle realSun = transform.parent.GetComponent<DayNightCycle>();

        //Volumes are always opposite
        float halfPoint = realSun.sunInitialIntensity / 2f;
        float diff = realSun.timeMultiplier - halfPoint;
        timeMultiplier = halfPoint - diff;

    }
}
