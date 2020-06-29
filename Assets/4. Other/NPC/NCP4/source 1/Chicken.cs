using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chicken : NPC
{
    private Animator anim;

    private const string IDLE_ANIM_BOOL = "idle";
    private const string WALK_ANIM_BOOL = "walk";

    private NavMeshAgent agent;
    float rangeDetected = 4f;
    float distance;
    Vector3 newPos;

    public LayerMask aggroLayerMask;
    private Collider[] withinAggroCollifer;

    public Transform player;
    public GameObject chiken;
    int multiplier = 1; // or more

    bool isFleeing = false;

    public float wanderRadius = 10;
    public float wanderTimer;
    float timer = 0;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = chiken.GetComponent<Animator>();
        wanderTimer = Random.Range(3f, 6f);
        agent.stoppingDistance = 1;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        //Debug.Log(timer);

        if (player != null)
        {
            Flee();

            if (!isFleeing)
            {
                Wander();
            }
        }

        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            AnimateIdle();
            isFleeing = false;
            agent.ResetPath();
        }
        else
        {
            AnimateWalk();
        }
    }

    private void Flee()
    {
        Vector3 runTo = transform.position + ((transform.position - player.position) * multiplier)+ Random.insideUnitSphere;
        distance = Vector3.Distance(transform.position, player.position);
        if (distance < rangeDetected)
        {
            isFleeing = true;
            agent.speed = 3;
            agent.SetDestination(runTo);
        }
    }

    private void Wander()
    {
       // Debug.Log("???");
        if (timer > wanderTimer)
        {
            newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.speed = 2;
            agent.SetDestination(newPos);
            timer = 0;
            wanderTimer = Random.Range(5f, 15f);
        }

        //if (wanderTimer - timer <= 3f) // time to stand and Idle
        //{
            
        //}
    }

    private void FixedUpdate()
    {
        if (player == null)
        {
            withinAggroCollifer = Physics.OverlapSphere(transform.position, rangeDetected, aggroLayerMask);
            if (withinAggroCollifer.Length > 0)
            {
                player = withinAggroCollifer[0].transform;
            }
        }
    }

    private void AnimateWalk()
    {
        Animate(WALK_ANIM_BOOL);
    }

    private void AnimateIdle()
    {
        Animate(IDLE_ANIM_BOOL);
    }
  
    private void Animate(string boolName)
    {
        DisableOtherAnim(anim, boolName);
        anim.SetBool(boolName, true);
    }
    private void DisableOtherAnim(Animator animator, string animation)
    {
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            if (parameter.name != animation && parameter.type == AnimatorControllerParameterType.Bool)
            {
                animator.SetBool(parameter.name, false);
            }
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}
