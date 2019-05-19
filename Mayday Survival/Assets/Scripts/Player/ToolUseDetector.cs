using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolUseDetector : MonoBehaviour
{
    public LayerMask mask;
    public List<GameObject> FindInteractables(float radius)
    {
        List<GameObject> objs = new List<GameObject>();
        Collider[] col = Physics.OverlapSphere(transform.position, radius, mask);
        for (int i = 0; i < col.Length; i++)
        {
            objs.Add(col[i].gameObject);
        }
        return objs;
    }
}
