using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentStageScript : MonoBehaviour
{
    [SerializeField] private GameObject parentGrounds;
    [SerializeField] private GameObject parentIAs;
    [SerializeField] public int nbrCoins;
    [SerializeField] private GameObject coins;
    [SerializeField] private GameObject stage;
    [SerializeField] private string pathCsv;
    [SerializeField] private List<Elevator> listTPs = new List<Elevator>();

    private List<GameObject> listGrounds;
    private List<GameObject> listIAs;
    private List<GameObject> listCoins;

    void Awake()
    {
        listGrounds = new List<GameObject>();
        listIAs = new List<GameObject>();
        listCoins = new List<GameObject>();
        int length = parentGrounds.transform.childCount;
        for (int i = 0; i < length; i++)
        {
            listGrounds.Add(parentGrounds.transform.GetChild(i).gameObject);
        }
        length = parentIAs.transform.childCount;
        for (int i = 0; i < length; i++)
        {
            listIAs.Add(parentIAs.transform.GetChild(i).gameObject);
        }

        for (int i = 0;i < nbrCoins; i++)
        {
            SpawnCoins();
        }
    }

    private void SpawnCoins()
    {
        int randIndex = UnityEngine.Random.Range(0, listGrounds.Count);
        GameObject o = Instantiate(coins, listGrounds[randIndex].transform.position, Quaternion.identity);
        o.transform.SetParent(listGrounds[randIndex].transform);
        listGrounds.Remove(listGrounds[randIndex]);
        listCoins.Add(o);
    }

    public List<GameObject> GetListCoins()
    {
        return listCoins;
    }

    public int GetNbrCoins()
    {
        return listCoins.Count;
    }

    public void RemoveCoin(GameObject coin)
    {
        listCoins.Remove(coin);
    }

    public void UnloadStage()
    {
        stage.SetActive(false);
    }

    public void LoadStage()
    {
        stage.SetActive(true);
    }

    public string GetPathCsv()
    {
        return pathCsv;
    }

    public List<GameObject> GetListIAs()
    {
        return listIAs;
    }

    public int GetNbrIAs()
    {
        return listIAs.Count;
    }

    public Vector3 GetPosTP(int index)
    {
        return listTPs[index].GetDestination();
    }

    public int getTPIndex(Elevator obj)
    {
        return listTPs.IndexOf(obj);
    }

}
