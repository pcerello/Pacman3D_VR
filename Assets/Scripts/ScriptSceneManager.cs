using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject[] listStages;
    private int currentStage = 0;
    private Vector3[] teleportPoints;
    [SerializeField] private int lenghtTps;



    void Awake()
    {
        teleportPoints = new Vector3[lenghtTps];
        for (int i = 1; i < lenghtTps; i++)
        {
            listStages[i].SetActive(false);
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
        listStages[idLoad].SetActive(true);
        listStages[idUnload].SetActive(false); 
    }

    public void AddElevetor(int id, Vector3 pos)
    {
        teleportPoints[id] = pos;
    }
}
