using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScripTableMap : MonoBehaviour
{
    [SerializeField] private GameObject line;
    [SerializeField] private Transform table;
    private List<GameObject> lines;

    private void Awake()
    {
        lines = new List<GameObject>();
    }

    public void CreateLine(int stage, int coin)
    {
        GameObject lineTmp = Instantiate(line);
        lineTmp.transform.SetParent(table);
        lineTmp.transform.localScale = Vector3.one;
        lineTmp.transform.localPosition = Vector3.zero;
        lineTmp.transform.localEulerAngles = Vector3.zero;
        lineTmp.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = stage.ToString();
        lineTmp.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = coin.ToString();
        lines.Add(lineTmp);
    }

    public void SetValue(int stage, int coins)
    {
        lines[stage].transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = coins.ToString();
    }
}
