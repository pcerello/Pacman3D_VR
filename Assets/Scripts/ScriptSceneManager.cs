using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptSceneManager : MonoBehaviour
{
    [SerializeField] private ParentStageScript[] listStages;
    private int currentStage = 0;
    private Vector3[] teleportPoints;
    [SerializeField] private int lenghtTps;

    void Awake()
    {
        teleportPoints = new Vector3[lenghtTps];
        for (int i = 1; i < lenghtTps; i++)
        {
            listStages[i].UnloadStage();
        }
    }

    public void UpStage(int Id, GameObject player)
    {
        if (currentStage < listStages.Length-1) {
            currentStage++;
            LoadUnloadStage(currentStage, currentStage - 1);

            player.transform.position = teleportPoints[Id];
        }
    }

    public void DownStage(int Id, GameObject player)
    {
        if (currentStage > 0) {
            currentStage--;
            LoadUnloadStage(currentStage, currentStage + 1);

            player.transform.position = teleportPoints[Id];
        }
    }

    private void LoadUnloadStage(int idLoad, int idUnload)
    {
        listStages[idLoad].LoadStage();
        listStages[idUnload].UnloadStage(); 
    }

    public void AddElevetor(int id, Vector3 pos)
    {
        teleportPoints[id] = pos;
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
