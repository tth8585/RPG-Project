using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Vector3 direction { get; set; }
    public float range { get; set; }
    public int damage { get; set; }

    Vector3 spawnPos;
    private void Start()
    {
        range = 20f;
        damage = 2;
        spawnPos = transform.position;
        GetComponent<Rigidbody>().AddForce(direction * 50f);
    }

    private void Update()
    {
        if(Vector3.Distance(spawnPos, transform.position)>= range)
        {
            Extinguish();
        }
    }

    void Extinguish()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            //collision.transform.GetComponent<IEnemy>().TakeDamage(damage);
        }
        Extinguish();
    }
}
