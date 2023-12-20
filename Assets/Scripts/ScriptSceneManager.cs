using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptSceneManager : MonoBehaviour
{
    [SerializeField] private string[] listScenes;
    private int currentStage = 0;
    private Vector3[] teleportPoints;
    [SerializeField] private int lenghtTps;

    void Awake()
    {
        teleportPoints = new Vector3[lenghtTps];
    }

    public void UpStage(int Id, GameObject player)
    {
        if (currentStage < listScenes.Length-1) {
            currentStage++;
            StartCoroutine(asyncLoad(currentStage, currentStage - 1));

            player.transform.position = teleportPoints[Id];
        }
    }

    public void DownStage(int Id, GameObject player)
    {
        if (currentStage > 0) {
            currentStage--;
            StartCoroutine(asyncLoad(currentStage, currentStage + 1));

            player.transform.position = teleportPoints[Id];
        }
    }

    private IEnumerator asyncLoad(int idLoad, int idUnload)
    {
        SceneManager.LoadSceneAsync(listScenes[idLoad], LoadSceneMode.Additive);
        yield return new WaitForSeconds(.01f);
        SceneManager.UnloadSceneAsync(listScenes[idUnload]);
    }

    public void AddElevetor(int id, Vector3 pos)
    {
        teleportPoints[id] = pos;
    }
}
