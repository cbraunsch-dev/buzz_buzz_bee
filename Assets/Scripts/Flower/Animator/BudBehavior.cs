using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BudBehavior : StateMachineBehaviour
{
    private GameObject flowerBud;
    private GameObject flowerBloomed;
    private GameObject flowerPollinated;
    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        flowerBud = animator.gameObject.transform.Find("FlowerBud").gameObject;
        flowerBloomed = animator.gameObject.transform.Find("FlowerBloomed").gameObject;
        flowerPollinated = animator.gameObject.transform.Find("FlowerPollinated").gameObject;

        flowerBud.SetActive(true);
        flowerBloomed.SetActive(false);
        flowerPollinated.SetActive(false);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
