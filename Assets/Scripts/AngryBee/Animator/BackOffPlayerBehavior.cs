using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackOffPlayerBehavior : StateMachineBehaviour
{
    private AngryBee angryBee;
    private Transform targetFlower;
    private BeeOnSurface beeOnSurface;
    private float speed = 1.0f;
    private float distanceWhenToFindNewFlower = 0.5f;
    private GameObject planet;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        planet = GameObject.FindGameObjectWithTag(Tags.Ground);
        beeOnSurface = animator.gameObject.transform.Find("BeeOnSurface").GetComponent<BeeOnSurface>();
        angryBee = animator.gameObject.GetComponent<AngryBee>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (AngryBee.TimeToSelectFlowerTarget(targetFlower, beeOnSurface.transform.position, distanceWhenToFindNewFlower))
        {
            Debug.Log("Find a new flower");
            targetFlower = AngryBee.PickRandomFlower(planet.GetComponent<Planet>());
            beeOnSurface.TargetPosition = targetFlower.position;
        }

        if (targetFlower != null)
        {
            var targetRotation = Quaternion.LookRotation(targetFlower.position - animator.gameObject.transform.position);

            // Smoothly rotate towards the target point.
            animator.gameObject.transform.rotation = Quaternion.Slerp(animator.gameObject.transform.rotation, targetRotation, speed * Time.deltaTime);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}
}
