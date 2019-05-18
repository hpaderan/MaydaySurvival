using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{   /* Harrold Paderan - 17 May 2019 */

    // Stats
    public int playerHealth;

    // Movement
    public float moveSpeed;
    private Vector3 moveVals;

    // Pick up
    private GameObject pickupTarg;

    // References
    public PickupDetector pickupDetector;
    private Rigidbody rbody;

    void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        GetInput();
    }

    void FixedUpdate()
    {
        moveVals = moveVals.normalized * moveSpeed * Time.deltaTime;
        rbody.MovePosition(transform.position + moveVals);
    }

    void GetInput()
    {
        // Move inputs
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        moveVals.Set(h,0f,v);

        // Action inputs
        if ( Input.GetKeyUp(KeyCode.F) ) {
            // do action here
            StartCoroutine("PickUp");
        }
    }

    IEnumerator PickUp()
    {
        if ( pickupDetector.enabled == false )
            pickupDetector.enabled = true;
        
        yield return new WaitForSeconds(0.2f);

        if ( pickupDetector.detected.Count <= 0 )
        {
            GameObject closest = pickupDetector.detected[0];
            foreach ( GameObject pickup in pickupDetector.detected )
            {
                if ( Vector3.Distance(pickup.transform.position,transform.position) <
                     Vector3.Distance(closest.transform.position,transform.position) )
                {
                    closest = pickup;
                }
            }
        }
        // Ask inventory to place closest in
        // InventorySingleton.instance.PutIn(closest);

        pickupDetector.enabled = false;
    }


}
