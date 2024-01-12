using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptSceneManager : MonoBehaviour
{
    [SerializeField] public ParentStageScript[] listStages;
    [SerializeField] private ParentStageScript winArea;
    [SerializeField] private ParentStageScript loseArea;
    private int currentStage = 0;

    void Awake()
    {
        for (int i = 1; i < listStages.Length; i++)
        {
            listStages[i].SetId(i);
        }
    }

    private void Start()
    {
        for (int i = 1; i < listStages.Length; i++)
        {
            listStages[i].UnloadStage();
        }
        winArea.UnloadStage();
        loseArea.UnloadStage();
    }

    public bool UpStage(GameObject player, Elevator tp)
    {
        if (currentStage < listStages.Length-1)
        {
            int index = listStages[currentStage].getTPIndex(tp);
            currentStage++;
            LoadUnloadStage(currentStage, currentStage - 1, player, index);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool DownStage(GameObject player, Elevator tp)
    {
        if (currentStage > 0)
        {
            int index = listStages[currentStage].getTPIndex(tp);
            currentStage--;
            LoadUnloadStage(currentStage, currentStage + 1, player, index);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void LoadUnloadStage(int idLoad, int idUnload, GameObject player, int index)
    {
        listStages[idLoad].LoadStage();
        listStages[idUnload].UnloadStage();
        player.transform.position = listStages[currentStage].GetPosTP(index);
    }

    public void GoToWinArea(GameObject player)
    {
        winArea.LoadStage();
        listStages[currentStage].UnloadStage();
        player.transform.position = winArea.GetPosTP(0);
    }

    public void GoToLoseArea(GameObject player)
    {
        loseArea.LoadStage();
        listStages[currentStage].UnloadStage();
        player.transform.position = loseArea.GetPosTP(0);
    }

    public ParentStageScript GetParentStage()
    {
        return listStages[currentStage];
    }

    public ParentStageScript GetParentStage(int id)
    {
        return listStages[id];
    }

    public ParentStageScript[] GetListParentStage()
    {
        return listStages;
    }

    public List<GameObject> GetListCoins()
    {
        return listStages[currentStage].GetListCoins();
    }
    public List<GameObject> GetListCoins(int id)
    {
        return listStages[id].GetListCoins();
    }

    public int GetNbrCoinsInStage(int id)
    {
        return listStages[id].GetNbrCoins();
    }

    public int GetCurrentStage()
    {
        return currentStage;
    }

    public List<GameObject> GetListIAs()
    {
        return listStages[currentStage].GetListIAs();
    }

    public List<GameObject> GetListIAs(int id)
    {
        return listStages[id].GetListIAs();
    }

    public int GetNbrIAsInStage(int id)
    {
        return listStages[id].GetNbrIAs();
    }
}
