using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolUseDetector : MonoBehaviour
{
    public List<GameObject> detected;

    void OnTriggerEnter(Collider col)
    {
        if ( col.gameObject.CompareTag("Object") )
        {
            detected.Add(col.gameObject);
        }
    }

    void OnDisable()
    {
        detected.Clear();
    }
}
