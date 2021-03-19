using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollinateFlowersBehavior : StateMachineBehaviour
{
    private BugOnSurface bugOnSurface;
    private GameObject targetBee;
    private float distanceWhenToChasePlayer = 4.0f;
    private GameObject planet;
    private Transform targetFlower;
    private float distanceWhenToFindNewFlower = 0.5f;
    private float speed = 1.0f;
    private AngryBee angryBee;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        planet = GameObject.FindGameObjectWithTag(Tags.Ground);
        bugOnSurface = animator.gameObject.transform.Find("BugOnSurface").GetComponent<BugOnSurface>();
        targetBee = animator.gameObject.GetComponent<AngryBee>().targetBee;
        angryBee = animator.gameObject.GetComponent<AngryBee>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (CloseEnoughToPlayer())
        {
            Debug.Log("Start chasing player");
            animator.SetTrigger(Triggers.FoundPlayer);
        }
        else if (InsectUtils.TimeToSelectFlowerTarget(targetFlower, bugOnSurface.transform.position, distanceWhenToFindNewFlower))
        {
            Debug.Log("Find a new flower");
            targetFlower = InsectUtils.PickRandomFlower(planet.GetComponent<Planet>());
            bugOnSurface.TargetPosition = targetFlower.position;
        }

        if (targetFlower != null)
        {
            // Move bee at a constant pace towards destination, regardless of how far away it is
            var targetRotation = Quaternion.LookRotation(targetFlower.position - animator.gameObject.transform.position);
            var currentProjectedPointOnSurface = animator.gameObject.transform.forward * planet.GetComponent<Planet>().Radius;
            var targetProjectedPointOnSurface = targetFlower.position;

            animator.gameObject.transform.rotation = Quaternion.SlerpUnclamped(animator.gameObject.transform.rotation, targetRotation, (1 / Vector3.Distance(currentProjectedPointOnSurface, targetProjectedPointOnSurface)) * 0.05f);
        }
    }

    private bool CloseEnoughToPlayer()
    {
        return Vector3.Distance(bugOnSurface.transform.position, targetBee.transform.position) < distanceWhenToChasePlayer;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}
}
