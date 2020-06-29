using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorldInteraction : MonoBehaviour
{
    //NavMeshAgent playerAgent;
    //[SerializeField] private LayerMask movementMask;
    //private void Start()
    //{
    //    playerAgent = GetComponent<NavMeshAgent>();
    //}
    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
    //    {
    //        //GetComponent<CharacterController>().enabled = false;
    //        //GetComponent<NavMeshAgent>().enabled = true;
            
    //        GetWorldInteraction();
    //    }
    //}
    //void GetWorldInteraction()
    //{
    //    Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);

    //    RaycastHit hit;
        
    //    if(Physics.Raycast(interactionRay, out hit, Mathf.Infinity))
    //    {
    //        GameObject interactedObj = hit.collider.gameObject;

    //        if(interactedObj.tag == "Interactable Object"|| interactedObj.tag == "NPC")
    //        {
    //            interactedObj.GetComponent<Interactable>().MoveToInteraction(playerAgent);
    //        }
    //        else
    //        {
    //            //playerAgent.stoppingDistance = 0f;
    //            //playerAgent.destination = hit.point;
    //        }
    //    }
    //}
}
