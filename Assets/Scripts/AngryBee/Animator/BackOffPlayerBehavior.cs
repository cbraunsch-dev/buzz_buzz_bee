using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackOffPlayerBehavior : StateMachineBehaviour
{
    private AngryBee angryBee;
    private Transform targetFlower;
    private BugOnSurface bugOnSurface;
    private float speed = 1.0f;
    private float distanceWhenToFindNewFlower = 0.5f;
    private GameObject planet;
    private float timeAfterWhenCanStartChasingPlayerAgain = 5.0f;
    private float amountOfTimeSinceStartedBackingOff = 0.0f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        planet = GameObject.FindGameObjectWithTag(Tags.Ground);
        bugOnSurface = animator.gameObject.transform.Find("BugOnSurface").GetComponent<BugOnSurface>();
        angryBee = animator.gameObject.GetComponent<AngryBee>();
        timeAfterWhenCanStartChasingPlayerAgain = Random.Range(3.0f, 8.0f);
        amountOfTimeSinceStartedBackingOff = 0.0f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (InsectUtils.TimeToSelectFlowerTarget(targetFlower, bugOnSurface.transform.position, distanceWhenToFindNewFlower))
        {
            Debug.Log("Find a new flower");
            targetFlower = InsectUtils.PickRandomFlower(planet.GetComponent<Planet>());
            bugOnSurface.TargetPosition = targetFlower.position;
        }
        if (targetFlower != null)
        {
            var targetRotation = Quaternion.LookRotation(targetFlower.position - animator.gameObject.transform.position);

            // Smoothly rotate towards the target point.
            animator.gameObject.transform.rotation = Quaternion.Slerp(animator.gameObject.transform.rotation, targetRotation, speed * Time.deltaTime);
        }
        amountOfTimeSinceStartedBackingOff += Time.deltaTime;
        if(amountOfTimeSinceStartedBackingOff >= timeAfterWhenCanStartChasingPlayerAgain)
        {
            animator.SetTrigger(Triggers.ReadyToFindPlayer);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}
}
