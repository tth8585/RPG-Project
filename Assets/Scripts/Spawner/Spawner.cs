
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject monster;
    public bool respawn;
    public float spawnDelay;
    private float currentTime;
    private bool spawning;

    private void Start()
    {
        Spawn();
        currentTime = spawnDelay;
    }
    private void Update()
    {
        if (spawning)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                Spawn();
            }
        }
    }
    private void Spawn()
    {
        IEnemy instance = Instantiate(monster, transform.position, Quaternion.identity).GetComponent<IEnemy>();
        instance.Spawner = this;
        spawning = false;
    }

    public void Respawn()
    {
        spawning = true;
        currentTime = spawnDelay;
    }
}
