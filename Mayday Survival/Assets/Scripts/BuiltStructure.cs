using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuiltStructure : GatherNode
{
    public float charge = 100f;
    private float maxCharge;
    public float drainRate = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        maxCharge = charge;
        minDrop = 1;
        maxDrop = 2;
    }
    private void Update()
    {
        charge -= drainRate * Time.deltaTime;
    }

    public void Recharge(Crystal crystal)
    {
        if (crystal.charge > 0)
        {
            charge += crystal.charge;
            charge = Mathf.Clamp(charge, 0, 100);
        }
    }

}
