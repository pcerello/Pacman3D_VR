using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //important

//if you use this code you are contractually obligated to like the YT video
public class ShortestPathMovement : MonoBehaviour //don't forget to change the script name if you haven't
{
    public Transform[] Targets;
    public NavMeshAgent Agent;
    
    void Update()
    {
        Transform closestTarget = null;
        float closestTargetDistance = float.MaxValue;
        NavMeshPath Path = new NavMeshPath();

        for (int i = 0; i < Targets.Length; i++)
        {
            if (Targets[i] == null)
            {
                continue;
            }

            if (NavMesh.CalculatePath(transform.position, Targets[i].position, Agent.areaMask, Path))
            {
                float distance = Vector3.Distance(transform.position, Path.corners[i]);
                for (int j = 1; j < Path.corners.Length; j++)
                {
                    distance += Vector3.Distance(Path.corners[j - 1], Path.corners[j]);
                }

                if (distance < closestTargetDistance)
                {
                    closestTargetDistance = distance;
                    closestTarget = Targets[i];
                }

            }
        }
        if (closestTarget != null) {
            Agent.SetDestination(closestTarget.position);
        }
    }

}
