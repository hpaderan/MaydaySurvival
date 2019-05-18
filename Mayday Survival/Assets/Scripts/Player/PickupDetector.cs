using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupDetector : MonoBehaviour
{
    public List<GameObject> detected;

    void OnTriggerEnter (Collider col)
    {
        if ( col.gameObject.CompareTag("Pickup") ) {
            detected.Add(col.gameObject);
        }
    }

    void OnDisable () 
    {
        detected.Clear();
    }
}
