using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DualTheme : Sound
{
    public AudioClip clipB;
    [HideInInspector]
    public AudioSource sourceB;
    [HideInInspector]
    public float VolDiff;
    [HideInInspector]
    public float maxVolume;
}
