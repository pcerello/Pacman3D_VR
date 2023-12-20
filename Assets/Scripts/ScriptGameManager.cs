using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private int points;
    [SerializeField] private ScriptSceneManager ssm;
    [SerializeField] private GameObject player;

    public static ScriptGameManager SGM;

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
