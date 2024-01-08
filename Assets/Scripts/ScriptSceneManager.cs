using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptSceneManager : MonoBehaviour
{
    [SerializeField] private ParentStageScript[] listStages;
    private int currentStage = 0;
    [SerializeField] private int lenghtTps;

    void Awake()
    {
        for (int i = 1; i < lenghtTps; i++)
        {
            listStages[i].UnloadStage();
        }
    }

    public void UpStage(GameObject player, Elevator tp)
    {
        if (currentStage < listStages.Length-1)
        {
            int index = listStages[currentStage].getTPIndex(tp);
            currentStage++;
            LoadUnloadStage(currentStage, currentStage - 1, player, index);

        }
    }

    public void DownStage(GameObject player, Elevator tp)
    {
        if (currentStage > 0)
        {
            int index = listStages[currentStage].getTPIndex(tp);
            currentStage--;
            LoadUnloadStage(currentStage, currentStage + 1, player, index);

        }
    }

    private void LoadUnloadStage(int idLoad, int idUnload, GameObject player, int index)
    {
        listStages[idLoad].LoadStage();
        listStages[idUnload].UnloadStage();
        player.transform.position = listStages[currentStage].GetPosTP(index);
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
