using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storm : MonoBehaviour
{
    private SoundManager sm;
    public Light lightning;
    public float lightTime = 0.3f;
    private float lightStamp;
    public float minTime, maxTime;
    //public ParticleSystem rain, lightning, stuff;
    private float timeStamp, time;

    private void Update()
    {
        lightning.enabled = false;
        CallLightning();
    }
    private void CallLightning()
    {
        if (time == -1) { time = Random.Range(minTime, maxTime); }
        if (timeStamp + time < Time.time)
        {
            lightStamp += Time.deltaTime;
            lightning.enabled = true;
            if (lightStamp >= lightTime)
            {
                timeStamp = Time.time;
                time = -1;
                lightStamp = 0;

                lightning.enabled = false;
            }
        }
    }
}
