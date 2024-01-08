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
    [SerializeField] private ScripTableMap tableMap;

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
            int coins = ssm.GetNbrCoinsInStage(i);
            totalCoins += coins;
            tableMap.CreateLine(i, coins);
        }
        scoreText.text = "Score : " + points.ToString() + " / " + totalCoins.ToString();

    }

    public void CollectCoin(GameObject coin)
    {
        points += 1;
        ssm.GetParentStage().RemoveCoin(coin);
        Destroy(coin);
        scoreText.text = "Score : " + points.ToString() + " / " + totalCoins.ToString();
        
        tableMap.SetValue(ssm.GetCurrentStage(), ssm.GetParentStage().GetNbrCoins());
    }

    public ScriptSceneManager GetScriptSceneManager { get { return ssm; } }

    public void AddPoints(int value)
    {
        points += value;
    }

    public void UpStage(Elevator tp)
    {
        ssm.UpStage(player, tp);
    }

    public void DownStage(Elevator tp)
    {
        ssm.DownStage(player, tp);
    }

    public List<GameObject> GetListCoins()
    {
        return ssm.GetListCoins();
    }

    public List<GameObject> GetListCoins(int id)
    {
        return ssm.GetListCoins(id);
    }

    public int GetNumberParentStage()
    {
        return ssm.GetListParentStage().Length;
    }

    public Transform GetTransformPlayer()
    {
        return player.transform;
    }

    public List<GameObject> GetListIAsStage()
    {
        return ssm.GetListIAs();
    }

    public List<GameObject> GetListIAsStage(int id)
    {
        return ssm.GetListIAs(id);
    }

    public int GetNbrIAsInStage(int id)
    {
        return ssm.GetNbrIAsInStage(id);
    }
}
