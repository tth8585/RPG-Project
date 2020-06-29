using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public Transform outPos;
    public Transform player;
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            player.GetComponent<MMO_Player_movement>().enabled = false;
            player.position = outPos.position;
            StartCoroutine(TurnOn());
            //player.GetComponent<MMO_Player_movement>().enabled = true;
        }
    }

    private IEnumerator TurnOn()
    {
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<MMO_Player_movement>().enabled = true;
    }
}
