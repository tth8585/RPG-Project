using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenRandomIdle : StateMachineBehaviour
{
    float idleTimeout = 3f;

    float idleTime;

    string[] list = { "1", "2" };
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (idleTime <= 0)
        {
            RandomIdle(animator);
            idleTime = idleTimeout;
        }
        else
        {
            idleTime -= Time.deltaTime;
        }
    }

    void RandomIdle(Animator animator)
    {
        //System.Random random = new System.Random();
        //int posIdle = random.Next(list.Length);
        //string stringIdle = list[posIdle];
        animator.SetTrigger("2");
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
