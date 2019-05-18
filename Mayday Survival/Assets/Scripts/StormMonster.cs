using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormMonster : MonoBehaviour
{
    public bool debugging;
    public DayNightCycle cycle;
    public float size;
    public float speed;
    public bool isSleeping;
    public float sight;
    private float defaultSight;
    public bool flee;
    private Rigidbody rb;
    private Transform target;
    public LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        defaultSight = sight;
    }

    // Update is called once per frame
    void Update()
    {
        isSleeping = !cycle.isNight;
        if (!isSleeping)
        {
            FindTarget();
            Move();
        }
    }
    private void FindTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, sight, mask);
        if (colliders.Length > 0)
        {
            target = colliders[0].transform;
            foreach (Collider c in colliders)
            {
                if (c.transform.tag == "Crystal")
                {
                    float cDistance = Vector3.Distance(transform.position, c.transform.position);
                    if (cDistance < Vector3.Distance(transform.position, target.position))
                    {
                        target = c.transform;
                    }
                }
            }
        } 
        sight = Mathf.Lerp(sight, defaultSight, Time.deltaTime);
        if (target == null)
        {
            sight += Time.deltaTime;
        }
    }
    private void Move()
    {
        if (target != null)
        {
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        }
        float dist = target != null ? Mathf.Clamp01(Vector3.Distance(transform.position, target.position)) : 1;
        transform.Translate(transform.forward * Time.deltaTime * speed * dist);
    }

    private void OnDrawGizmos()
    {
        if (debugging)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, sight);
        }
    }
}
