using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 **IMPORTANT
 when changing to size for MapManager gameObject you need to also change to size in
 ground generator because the object that spawns on the map relies on the number in
 GroundGenerator.
     */


public class MapManager : MonoBehaviour
{
    public GroundGenerate GroundScript;
    public ObjectGenerator ObjectScript;
    // Start is called before the first frame update
    void Awake()
    {
        GroundScript = GetComponent<GroundGenerate>();
        ObjectScript = GetComponent<ObjectGenerator>();
        InitGame();
    }

    void InitGame()
    {
        GroundScript.SetupScene(3);
        ObjectScript.GenerateAll();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
