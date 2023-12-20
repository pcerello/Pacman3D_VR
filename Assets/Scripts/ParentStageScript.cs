using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ParentStageScript : MonoBehaviour
{
    [SerializeField] private GameObject parentGrounds;
    [SerializeField] public int nbrCoins;
    [SerializeField] private GameObject coins;
    [SerializeField] private GameObject stage;

    private List<GameObject> listGrounds;

    void Awake()
    {
        listGrounds = new List<GameObject>();
        int length = parentGrounds.transform.childCount;
        for (int i = 0; i < length; i++)
        {
            listGrounds.Add(parentGrounds.transform.GetChild(i).gameObject);
        }

        for (int i = 0;i < nbrCoins; i++)
        {
            SpawnCoins();
        }
    }

    private void SpawnCoins()
    {
        int randIndex = Random.Range(0, listGrounds.Count);
        GameObject o = Instantiate(coins, listGrounds[randIndex].transform.position, Quaternion.identity);
        o.transform.SetParent(listGrounds[randIndex].transform);
        listGrounds.Remove(listGrounds[randIndex]);
    }

    public int GetNbrCoins()
    {
        return nbrCoins;
    }

    public void LowerCoin()
    {
        nbrCoins -= 1;
    }

    public void UnloadStage()
    {
        stage.SetActive(false);
    }

    public void LoadStage()
    {
        stage.SetActive(true);
    }
}
