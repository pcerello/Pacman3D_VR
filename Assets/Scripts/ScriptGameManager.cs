using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScriptGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private int points;
    [SerializeField] private ScriptSceneManager ssm;
    [SerializeField] private GameObject player;
    [SerializeField] public TMP_Text scoreText;

    public static ScriptGameManager SGM;

    private int totalCoins;

    private void Awake()
    {
        if(SGM == null)
        {
            SGM = this;
        }
        else
        {
            Destroy(this );
        }
    }

    void Start()
    {
        points = 0;
        totalCoins = 0;

        for (int i = 0; i < ssm.GetListParentStage().Length; i++)
        {
            totalCoins += ssm.GetListParentStage()[i].GetNbrCoins();
        }
        scoreText.text = "Score : " + points.ToString() + " / " + totalCoins.ToString();
    }

    public void CollectCoin(GameObject coin)
    {
        points += 1;
        ssm.GetListCoins().Remove(coin);
        Destroy(coin.gameObject);
        ssm.GetParentStage().LowerCoin();
        scoreText.text = "Score : " + points.ToString() + " / " + totalCoins.ToString();
        
    }

    public ScriptSceneManager GetScriptSceneManager { get { return ssm; } }

    public void AddPoints(int value)
    {
        points += value;
    }

    public void UpStage(int Id)
    {
        ssm.UpStage(Id, player);
    }

    public void DownStage(int Id)
    {
        ssm.DownStage(Id, player);
    }

    public void AddElevetor(int id, Vector3 pos)
    {
        ssm.AddElevetor(id, pos);
    }
}
