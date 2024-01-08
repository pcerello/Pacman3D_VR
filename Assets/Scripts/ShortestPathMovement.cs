using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //important

//if you use this code you are contractually obligated to like the YT video
public class ShortestPathMovement : MonoBehaviour //don't forget to change the script name if you haven't
{
    private Transform target;
    public NavMeshAgent Agent;

    private void Start()
    {
        target = ScriptGameManager.SGM.GetTransformPlayer();
    }

    void Update()
    {
        Transform closestTarget = null;
        float closestTargetDistance = float.MaxValue;
        NavMeshPath Path = new NavMeshPath();

        if (target == null)
        {
            Debug.Log("Either this is an error, either the player is not in the same navmesh. Probably must never happen.");
        }

        if (NavMesh.CalculatePath(transform.position, target.position, Agent.areaMask, Path))
        {
            float distance = Vector3.Distance(transform.position, Path.corners[0]);
            for (int j = 1; j < Path.corners.Length; j++)
            {
                distance += Vector3.Distance(Path.corners[j - 1], Path.corners[j]);
            }

            if (distance < closestTargetDistance)
            {
                closestTargetDistance = distance;
                closestTarget = target;
            }

        }
        if (closestTarget != null) {
            Agent.SetDestination(closestTarget.position);
        }
    }

}
