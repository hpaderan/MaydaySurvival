using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightManager : MonoBehaviour
{    /* Harrold Paderan 18-May-2019 */

    public float totalTime;
    public float timeMultiplier;
    public int hours;

    public bool isNight;

    public AudioSource dayMusic;
    public AudioSource nightMusic;

    /*****************************************************************************************/
    /* 18-May-2019 https://www.javaworld.com/article/2073352/core-java-simply-singleton.html */
    private static DayNightManager instance = null;
    protected DayNightManager() {
        // Exists only to defeat instantiation.
    }
    public static DayNightManager getInstance(){
        if ( instance == null )
            instance = new DayNightManager();
        
        return instance;    }
    /*****************************************************************************************/

    private void Start()
    {
        totalTime = 0;
        hours = 0;
    }

    private void Update()
    {
        totalTime += Time.deltaTime * timeMultiplier;
        hours = (int) totalTime % 24;
    }

    public void ActivateNight ()
    {
        // Night time starts here
    }

    public void ActivateDay()
    {
        // Day time starts here
    }
}
