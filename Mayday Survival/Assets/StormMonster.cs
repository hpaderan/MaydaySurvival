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
    [SerializeField]
    private Transform target;
    public LayerMask mask;

    private float defaultSpeed;
    Vector3 wanderDir = Vector3.zero;
    private float wanDir;
    [SerializeField]
    private bool isWandering;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        defaultSight = sight;
        defaultSpeed = speed;
        isWandering = false;
        wanDir = Random.Range(-180.0f,180.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //isSleeping = !cycle.isNight;
        isSleeping = false;
        if ( !isSleeping )
        {
            FindTarget();
            Move();
        }
    }
    private void FindTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position,sight,mask);
        if ( colliders.Length > 0 )
        {/*
            if ( target == null || target.tag != "Crystal" )
                target = colliders[0].transform;*/
            int idx = 0;
            while ( idx < colliders.Length ) {
                if ( target == null && colliders[idx].transform.CompareTag("Crystal") )
                {
                    isWandering = false;
                    target = colliders[idx].transform;
                } else {
                    float cDistance = Vector3.Distance(transform.position,colliders[idx].transform.position);
                    if ( cDistance < Vector3.Distance(transform.position,target.position) )
                    {
                        target = colliders[idx].transform;
                    }
                }

                idx++;
            }


        } else
        {
            target = null;
            if ( !isWandering )
            {
                isWandering = true;
                Debug.Log("Start wanderingDir");
                StartCoroutine("ChooseWanderDir");
            }
        }

        sight = Mathf.Lerp(sight,defaultSight,Time.deltaTime);
        if ( target == null )
        {
            sight += Time.deltaTime;
        }
    }
    private void Move()
    {
        if ( target != null )
        {
            transform.LookAt(new Vector3(target.position.x,transform.position.y,target.position.z));
            if ( Vector3.Distance(transform.position,target.transform.position) <= 1.2f )
            {
                speed = 0;
            } else
            {
                speed = defaultSpeed;
            }
        } 
        float foo = target != null ? Mathf.Clamp01(Vector3.Distance(transform.position,target.position)) : 1;
        transform.Translate(transform.forward * Time.deltaTime * speed * foo);
        //Debug.Log(foo);
    }

    private void OnDrawGizmos()
    {
        if ( debugging )
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position,sight);
        }
    }

    IEnumerator ChooseWanderDir()
    {
        yield return new WaitForSeconds(3.0f);

        int antiInf = 0;
        while ( wanDir + transform.rotation.y < 360 && wanDir + transform.rotation.y > -360 ) {
            wanDir = Random.Range(-180.0f,180.0f);
            if ( antiInf > 1000 )
            {
                Debug.Log("infinite loop");
                break;
            }
            antiInf++;
        }
        wanderDir = new Vector3(0, wanDir,0);

        transform.localEulerAngles = wanderDir;
        
        if ( isWandering )
        {
            StartCoroutine("WanderPause");
            //Debug.Log("Start wanderPause");
        }
    }

    IEnumerator WanderPause()
    {
        //wanderDir = Vector3.zero;
        speed = 0;

        yield return new WaitForSeconds(2.0f);
        speed = defaultSpeed;
        if ( isWandering )
        {
            StartCoroutine("ChooseWanderDir");
           // Debug.Log("start choosewanderdir");
        }
    }
}
