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
    private bool isMoveLocked;

    // Pick up
    private bool isPickingup;
    private GameObject pickupTarg;

    // Use tool
    private bool isUsingTool;
    public GameObject currentTool;

    // References
    public PickupDetector pickupDetector;
    private Rigidbody rbody;

    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        isMoveLocked = false;
        isPickingup = false;
        isUsingTool = false;
    }
    
    void Update()
    {
        GetInput();
    }

    void FixedUpdate()
    {
        if ( !isMoveLocked )
        {
            moveVals = moveVals.normalized * moveSpeed * Time.deltaTime;
            rbody.MovePosition(transform.position + moveVals);
            transform.forward = moveVals.normalized;
        }
    }

    void GetInput()
    {
        // Move inputs
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        moveVals.Set(h,0f,v);

        // Action inputs
        if ( Input.GetAxis("Interact") > 0.8 && !isPickingup) {
            // do action here
            Debug.Log("Interacting...");
            StartCoroutine("PickUp");
        }

        if ( Input.GetAxis("Use") > 0.8 && !isUsingTool )
        {
            // do action here
            Debug.Log("Using...");
            isMoveLocked = true;
            if ( currentTool != null )
                currentTool.GetComponent<IUsable>().Use();


        } else { isMoveLocked = false; }
    }

    IEnumerator PickUp()
    {
        isPickingup = true;
        if ( pickupDetector.enabled == false )
            pickupDetector.enabled = true;
        
        yield return new WaitForSeconds(0.2f);

        if ( pickupDetector.detected.Count > 0 )
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
        // InventorySingleton.PutIn

        pickupDetector.enabled = false;
        isPickingup = false;
    }


}
