using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimator : MonoBehaviour
{
    public Animator Anim;
    public bool MissionCompleteTest;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            //start running
            Anim.SetTrigger("Running");
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            //pick up item
            Anim.SetTrigger("PickUp");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            //put down item
            Anim.SetTrigger("PutDown");
            //if mission complete, waves
            Anim.SetBool("MissionComplete", true);
            //else, goes idle
            Anim.SetBool("MissionComplete", true);
        }
    }
}
