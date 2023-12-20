using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public int id;
    public Transform destination;

    void Start()
    {
        ScriptGameManager.SGM.AddElevetor(id, destination.position);
    }
}
