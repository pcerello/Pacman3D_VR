using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTouch : MonoBehaviour
{
    [SerializeField] public int points;

    void Start()
    {
        points = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TPUp"))
        {
            int id = other.GetComponentInParent<Elevator>().id;
            ScriptGameManager.SGM.UpStage(id);
        }
        else if (other.CompareTag("TPDown"))
        {
            int id = other.GetComponentInParent<Elevator>().id;
            ScriptGameManager.SGM.DownStage(id);
        }
        else if (other.CompareTag("Coin"))
        {
            points += 1;
            Destroy(other.gameObject);
            print(points);
        }
    }
}
