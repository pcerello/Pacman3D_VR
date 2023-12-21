using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditorInternal.VersionControl.ListControl;

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
        ssm.GetListCoins().Remove(coin);
        Destroy(coin.gameObject);
        scoreText.text = "Score : " + points.ToString() + " / " + totalCoins.ToString();

        tableMap.SetValue(ssm.GetCurrentStage(), ssm.GetParentStage().GetNbrCoins());

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
