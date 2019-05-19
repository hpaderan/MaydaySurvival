using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GroundGenerate GroundScript;
    // Start is called before the first frame update
    void Awake()
    {
        GroundScript = GetComponent<GroundGenerate>();
        InitGame();
    }

    void InitGame()
    {
        GroundScript.SetupScene(3);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
