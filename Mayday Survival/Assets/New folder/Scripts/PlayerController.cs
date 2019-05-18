using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]

public class PlayerController : MonoBehaviour
{
    public Interactable focus;
    public LayerMask movementMask;
    PlayerMotor motor;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        //this is to check if we are currently hovering the UI, if so then the player will not
        //be able to move
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(0))
        {//control the left mouse button
            //cast camera towards of whatever we clicked on
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, movementMask))
                //(target to ray cast, out put to object, range, layermask)
            {
                Debug.Log("Checkpoint01");
                //Move the player to what we hit
                motor.MoveToPoint(hit.point);
                //Stop focusing to any object
                RemoveFocus();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {//control the left mouse button
            //cast camera towards of whatever we clicked on
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            //(target to ray cast, out put to object, range, layermask)
            {
                Debug.Log("Checkpoint02");
                //Check if we hit an interactable
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                //if so set it to our focus
                if (interactable != null)
                {//set the interactable into our focus
                    SetFocus(interactable);
                }
            }
        }
    }

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
            {

                focus.OnDeFocus();
            }
            focus = newFocus;
            motor.FollowTarget(newFocus);
        }
        newFocus.OnFocus(transform);

    }

    void RemoveFocus()
    {
        if (focus != null)
        {
            focus.OnDeFocus();
        }
        focus = null;

        motor.StopFollowingTarget();
    }
}
