using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayerBehavior : StateMachineBehaviour
{
    private float timeToSpendChasingPlayer = 5.0f;
    private float timeSpentChasingPlayer = 0.0f;
    private float timeSinceLastChangedPlayerTarget = 0.0f;
    private float timeBeforeChangePlayerTarget = 0.5f;
    private Vector3 currentTargetPosition = Vector3.zero;
    private BeeOnSurface beeOnSurface;
    private GameObject targetBee;
    private GameObject planet;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        planet = GameObject.FindGameObjectWithTag(Tags.Ground);
        beeOnSurface = animator.gameObject.transform.Find("BeeOnSurface").GetComponent<BeeOnSurface>();
        targetBee = animator.gameObject.GetComponent<AngryBee>().targetBee;
        timeSpentChasingPlayer = 0.0f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeSpentChasingPlayer += Time.deltaTime;
        if (timeSpentChasingPlayer > timeToSpendChasingPlayer)
        {
            Debug.Log("Stop chasing player");
            animator.SetTrigger(Triggers.GiveUp);
        }

        FollowPlayer(animator.gameObject.transform);
    }

    private void FollowPlayer(Transform transform)
    {
        timeSinceLastChangedPlayerTarget += Time.deltaTime;
        if (timeSinceLastChangedPlayerTarget > timeBeforeChangePlayerTarget)
        {
            timeSinceLastChangedPlayerTarget = 0.0f;
            Debug.Log("Find new position near player");
            currentTargetPosition = FindNewTargetNearPlayer();
        }

        if (currentTargetPosition != Vector3.zero)
        {
            beeOnSurface.TargetPosition = targetBee.transform.position;

            var targetRotation = Quaternion.LookRotation(currentTargetPosition - transform.position);
            var currentProjectedPointOnSurface = transform.forward * planet.GetComponent<Planet>().Radius;
            var targetProjectedPointOnSurface = currentTargetPosition;

            // Moves bee without slowing it down as it approaches its destination making for more frantic bee movement
            transform.rotation = Quaternion.SlerpUnclamped(transform.rotation, targetRotation, (1 / Vector3.Distance(currentProjectedPointOnSurface, targetProjectedPointOnSurface)) * 0.1f);
        }
    }

    private Vector3 FindNewTargetNearPlayer()
    {
        // Add a bit of variance to the target rotation to make it interesting
        var variance = 1.0f;
        var targetPosWithVariance = new Vector3(targetBee.gameObject.transform.position.x + Random.Range(-variance, variance), targetBee.gameObject.transform.position.y + Random.Range(-variance, variance), targetBee.gameObject.transform.position.z + Random.Range(-variance, variance));
        //var testCube = Instantiate(testCubePrefab);
        //testCube.transform.position = targetPosWithVariance;
        return targetPosWithVariance;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}
}
