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
    public GameObject useTarg;

    // Drop item
    private bool isDroppingItem;

    // Cycle inventory
    private bool isCyclingInv;

    // References
    public ToolUseDetector toolUseDetector;
    public PickupDetector pickupDetector;
    private Rigidbody rbody;
    //inventory

    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        isMoveLocked = false;
        isPickingup = false;
        isUsingTool = false;
        isDroppingItem = false;
        isCyclingInv = false;
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
            if (moveVals.normalized != Vector3.zero)
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
        if ( Input.GetAxis("Pickup") > 0.8 && !isPickingup) {
            // do action here
            Debug.Log("Picking up...");
            StartCoroutine("PickUp");
        }

        if ( Input.GetAxis("Use") > 0.8 && !isUsingTool )
        {
            // do action here
            Debug.Log("Using...");
            isMoveLocked = true;
            if ( currentTool != null ) { }
            StartCoroutine("Use");


        } else { isMoveLocked = false; }

        if ( Input.GetAxisRaw("Drop") == 1 && !isDroppingItem)
        {
            // do action here
            Debug.Log("Dropping...");
            StartCoroutine("Drop");
        }

        if ( Input.GetAxis("CycleInv") != 0 && !isCyclingInv)
        {
            StartCoroutine("CycleInv");
        }
    }

    void Use() {
        //  if ( obj.GetComponent<scriptHere>().toolRequirement == currentTool.GetName() )
        //      obj.GetComponent<scriptHere>().Use();
        foreach ( GameObject obj in toolUseDetector.detected ) {
            if ( obj.toolReq == currentTool.GetName() )
                obj.Use();
        }
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
            // Ask inventory to place closest in
            //Inventory.ADD(closest);
        }

        pickupDetector.enabled = false;
        isPickingup = false;
    }

    IEnumerator Drop() {
        isDroppingItem = true;
        // Inventory.remove(GetItem());
        // spawn item
        yield return new WaitForSeconds(0.8f); // short delay to prevent dropping everything
        isDroppingItem = false;
    }

    IEnumerator CycleInv()
    {
        isCyclingInv = true;

        Debug.Log(Input.GetAxis("CycleInv"));
        if ( Input.GetAxis("CycleInv") == 1 ) {
            //currentTool = inventory.getItemToRight; iff itemToRight is a tool. else null
            Debug.Log("Move inv to right");
        } else if ( Input.GetAxis("CycleInv") < 0 ) {
            //currentTool = inventory.getItemToLeft; iff itemToLeft is a tool. else null
            Debug.Log("Move inv to left");
        }

        yield return new WaitForSeconds(0.2f);
        isCyclingInv = false;
    }
}
