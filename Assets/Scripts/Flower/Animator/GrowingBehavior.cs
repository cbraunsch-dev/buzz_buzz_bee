using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingBehavior : StateMachineBehaviour
{
    private GameObject flowerBud;
    private GameObject flowerBloomed;
    private GameObject flowerPollinated;

    private float rateOfGrowth = 0.0f;
    private Vector3 defaultFullScale;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        flowerBud = animator.gameObject.transform.Find("FlowerBud").gameObject;
        flowerBloomed = animator.gameObject.transform.Find("FlowerBloomed").gameObject;
        flowerPollinated = animator.gameObject.transform.Find("FlowerPollinated").gameObject;

        flowerBud.SetActive(false);
        flowerBloomed.SetActive(true);
        flowerPollinated.SetActive(false);

        defaultFullScale = flowerBloomed.transform.localScale;
        flowerBloomed.transform.localScale *= 0.1f;
        rateOfGrowth = 1.05f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        flowerBloomed.transform.localScale *= rateOfGrowth;   
        if (flowerBloomed.transform.localScale.magnitude >= defaultFullScale.magnitude)
        {
            flowerBloomed.transform.localScale = defaultFullScale;
            animator.SetTrigger(Triggers.Grown);
        }
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
