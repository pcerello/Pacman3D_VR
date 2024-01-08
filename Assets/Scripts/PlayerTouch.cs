using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouch : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TPUp"))
        {
            ScriptGameManager.SGM.UpStage(other.GetComponentInParent<Elevator>());
        }
        else if (other.CompareTag("TPDown"))
        {
            ScriptGameManager.SGM.DownStage(other.GetComponentInParent<Elevator>());
        }
        else if (other.CompareTag("Coin"))
        {
            ScriptGameManager.SGM.CollectCoin(other.gameObject);
        }
    }
}
