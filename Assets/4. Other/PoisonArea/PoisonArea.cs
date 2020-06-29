
using UnityEngine;

public class PoisonArea : MonoBehaviour
{
    float tickTime;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            tickTime -= Time.deltaTime;
            if (tickTime < 0)
            {
                tickTime = 1;
                other.GetComponent<PlayerController>().TakeDamage(200, true);
            }
        }
    }
}
