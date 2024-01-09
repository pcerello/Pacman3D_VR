using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShortestPathMovement : MonoBehaviour
{
    private Transform target;
    public NavMeshAgent Agent;
    private float lastActionTime;
    private bool paused;

    private void Start()
    {
        target = ScriptGameManager.SGM.GetTransformPlayer();
        lastActionTime = -1;
        paused = false;
    }

    void Update()
    {
        Transform closestTarget = null;
        float closestTargetDistance = float.MaxValue;
        NavMeshPath Path = new NavMeshPath();
        Debug.Log(paused);

        if (target == null)
        {
            Debug.Log("Either this is an error, either the player is not in the same navmesh. Probably must never happen.");
        }
        else if (lastActionTime == -1)
        {
            lastActionTime = Time.time;
        }
        else if (Time.time - lastActionTime >= 0.5 && paused)
        {
            paused = false;
            lastActionTime = Time.time;
            Agent.isStopped = false;
        }
        else if (Time.time - lastActionTime >= 2.25 && !paused)
        {
            paused = true;
            lastActionTime = Time.time;
            Agent.isStopped = true;
        }

        if (NavMesh.CalculatePath(transform.position, target.position, Agent.areaMask, Path) && !paused)
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
