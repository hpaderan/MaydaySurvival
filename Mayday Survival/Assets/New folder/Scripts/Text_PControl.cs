using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text_PControl : MonoBehaviour
{
    public Interactable focus;
    Camera cam;
    public Item metal;
        // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal")*Time.deltaTime,0f,Input.GetAxis("Vertical")*Time.deltaTime);
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 100))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    //Set
                }
            }
            //interactable
        }
        Inventory backpack = Inventory.instance;
        if (Input.GetKeyUp(KeyCode.F)) {
            //set invenotry [0] = this

            backpack.ADD(metal);
        }
        if (Input.GetKeyUp(KeyCode.G)) {
            //remove inventory[0]
            backpack.Remove(metal);
        }
    }
}
