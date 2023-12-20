using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerTouch : MonoBehaviour
{
    [SerializeField] public int points;
    [SerializeField] public TMP_Text scoreText;

    void Start()
    {
        points = 0;
        scoreText.text = "Score : " + points.ToString();

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
            scoreText.text = "Score : " + points.ToString();
        }
    }
}
