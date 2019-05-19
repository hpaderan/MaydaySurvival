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
    private LayerMask crystalMask;
    private LayerMask crysPlayerMask;
    private LayerMask mask;

    private float defaultSpeed;
    Vector3 wanderDir = Vector3.zero;
    private float wanDir;
    private bool isWandering;

    public float crystalHealth;
    private float defCrysHealth;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        defaultSight = sight;
        defaultSpeed = speed;
        isWandering = false;
        wanDir = Random.Range(-180.0f,180.0f);
        defCrysHealth = crystalHealth;
        crystalMask = LayerMask.GetMask("Crystal");
        crysPlayerMask = LayerMask.GetMask("Crystal","Player");
        mask = crystalMask;
    }

    // Update is called once per frame
    void Update()
    {
        isSleeping = !cycle.isNight;
        if ( !isSleeping )
        {
            FindTarget();
            Move();
        }
    }
    private void FindTarget()
    {
        CheckInv();
        Collider[] colliders = Physics.OverlapSphere(transform.position,sight,mask);
        if ( colliders.Length > 0 )
        {
            int idx = 0;
            while ( idx < colliders.Length ) {
                if ( target == null && colliders[idx].transform.CompareTag("Crystal") )
                {
                    isWandering = false;
                    target = colliders[idx].transform;
                    // reset crystal health timer

                    crystalHealth = defCrysHealth;
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
                crystalHealth -= Time.deltaTime;
                if ( crystalHealth <= 0 ) {
                    Destroy(target.gameObject);
                }
            } else
            {
                speed = defaultSpeed;
            }
        }
       // Debug.Log(rb.velocity);
        transform.Translate(transform.forward * Time.deltaTime * speed, Space.World);
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
        }
    }

    IEnumerator WanderPause()
    {
        speed = 0;

        yield return new WaitForSeconds(2.0f);
        speed = defaultSpeed;
        if ( isWandering )
        {
            StartCoroutine("ChooseWanderDir");
        }
    }

    private void CheckInv() {
        if ( Inventory.instance.items.Count > 0 )
        {
            foreach ( Item item in Inventory.instance.items )
            {
                if ( item.ItemName == "Crystal" )
                {
                    mask = crysPlayerMask;
                    break;
                }
                mask = crystalMask;
            }
        }
    }
}
