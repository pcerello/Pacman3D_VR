using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform destination;

    public Vector3 GetDestination()
    {
        return destination.position;
    }
}
