using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //this is the distance between the player and the object to be interactable
    public float radius = 3f;
    public Transform interactionTransform;
    bool isFocus = false;
    Transform player;

    bool hasInteracted = false;

    public virtual void interact()
    {/*this function is set tovirtual so that it is a base class for different kinds of items interact 
        differently to the player*/
        Debug.Log("Interacting with items");

    }

    void Update()
    {
        if (isFocus && !hasInteracted)
        {//check if the object is currently being focus and if so
            //get the distance between the player and the selected object
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if(distance <= radius)
            {
                Debug.Log("Interacting");
                //add code to interact with items
                interact();
                hasInteracted = true;
            }
        }
    }

    public void OnFocus(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        //make sure it will interact once each time the camera focus on an object
        hasInteracted = false;
    }

    public void OnDeFocus()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }
    private void OnDrawGizmosSelected()
    {
        if(interactionTransform == null)
        {
            interactionTransform = transform;
        }
        Gizmos.color = Color.yellow;
        //draw color on the screen
        Gizmos.DrawWireSphere(interactionTransform.position,radius);
    }
}
