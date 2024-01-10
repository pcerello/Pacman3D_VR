using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //important

//if you use this code you are contractually obligated to like the YT video
public class RandomMovement : MonoBehaviour //don't forget to change the script name if you haven't
{
    [SerializeField] public float range; //radius of sphere

    private NavMeshAgent agent;
    private Transform centrePoint; //centre of the area the agent wants to move around in
    //instead of centrePoint you can set it as the transform of the agent if you don't care about a specific area
    
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        centrePoint = GetComponent<Transform>();
    }

    
    void Update()
    {
        if(agent.remainingDistance <= agent.stoppingDistance) //done with path
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point)) //pass in our centre point and radius of area
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                agent.SetDestination(point);
            }
        }

        if(Time.time % 5 == 0)
        {
            source.Play();
        }

    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        int unit = 3;
        Vector3 randomPoint = center; //+ Random.insideUnitSphere * range; //random point in a sphere 
        int x_movement = 0;
        int z_movement = 0;
        if (Random.value > 0.5f)
        {
            if (Random.value > 0.5f) { x_movement = -unit; } else { x_movement = unit; }
            randomPoint.x = randomPoint.x + x_movement;
        }
        else
        {
            if (Random.value > 0.5f) { z_movement = -unit; } else { z_movement = unit; }
            randomPoint.z = randomPoint.z + z_movement;
        }
        
        
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        { 
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    
}
