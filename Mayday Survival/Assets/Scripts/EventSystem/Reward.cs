using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    public bool isActive;
    public virtual void Activate()
    {
        isActive = true;
    }
}