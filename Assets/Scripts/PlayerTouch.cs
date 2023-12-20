using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerTouch : MonoBehaviour
{

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
            ScriptGameManager.SGM.CollectCoin(other.gameObject);
        }
    }
}
