using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using UnityEngine;

public class MapCalculator : MonoBehaviour
{
    [SerializeField] public int stageNumber;
    [SerializeField] public float tilesSize;
    [SerializeField] public Canvas canvas;
    [SerializeField] public GameObject wall;
    private float rate = (float)0.125;
    private GameObject MapPlayer;
    private Dictionary<GameObject, GameObject> MapAIs = new Dictionary<GameObject, GameObject>();
    private List<GameObject> MapCoins = new List<GameObject>(); // TODO : Lier aux coins pour les suppr ou etc

    void Start()
    {
        ReadCSVFile();
        LocatePlayerAndAIS();
        foreach (GameObject coin in ScriptGameManager.SGM.GetListCoins(stageNumber-1))
        {
            makeTile("MapCoin", coin.transform.position, wall.transform.position, (float)17 / 18, (float)0.475, (float)0.949);
        }
    }

    private void Update()
    {
        updateTile(MapPlayer, ScriptGameManager.SGM.GetTransformPlayer().position, wall.transform.position);

        foreach (KeyValuePair<GameObject, GameObject> pair in MapAIs) 
        {
            GameObject AI = pair.Key;
            GameObject mapAI = pair.Value;
            updateTile(mapAI, AI.transform.position, wall.transform.position);
        }
    }

    void LocatePlayerAndAIS()
    {
        List<GameObject> AIs = ScriptGameManager.SGM.GetListIAsStage(stageNumber-1);
        for (int i = 0; i < ScriptGameManager.SGM.GetListIAsStage(stageNumber-1).Count; i++)
        {
            MapAIs[AIs[i]] = makeTileFromCSV("MapAI"+i, AIs[i].transform.position, (float)0.8, 1, 1);
        }

        MapPlayer = makeTileFromCSV("MapPlayer", ScriptGameManager.SGM.GetTransformPlayer().position, 0, 1, 1);
    }

    void ReadCSVFile()
    {
        // Liste pour stocker les données lues à partir du fichier CSV
        List<string[]> csvData = new List<string[]>();

        // Lire le fichier CSV ligne par ligne
        using (StreamReader reader = new StreamReader("Assets/CSV/etage"+stageNumber+".csv"))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] values = line.Split(',');

                // Ajouter les données lues à la liste
                csvData.Add(values);
            }
        }

        int row_x = 0;
        csvData.Reverse();
        // Afficher les données lues pour vérification
        foreach (string[] row in csvData)
        {
            for (int i = 0; i < row.Length; i++)
            {
                SpawnObjLab(row, row_x, i);
            }
            row_x++;
        }
    }

    private GameObject makeTileFromCSV(string tileName, Vector3 position, float H, float S, float V)
    {
        GameObject tile = new GameObject(tileName);
        UnityEngine.UI.Image tileImage = tile.AddComponent<UnityEngine.UI.Image>();
        tileImage.color = UnityEngine.Color.HSVToRGB(H, S, V);
        position.z = 0;


        Vector3 canvasPos = canvas.transform.position;
        Vector2 wh = canvas.GetComponent<RectTransform>().sizeDelta;
        Debug.Log(wh.x);
        Debug.Log(wh.y);
        Debug.Log(rate);
        canvasPos = canvasPos - new Vector3((wh.x+rate)/2-rate, (wh.y+rate)/2-rate);
        tile.transform.position = canvasPos + rate * position;
        tile.transform.SetParent(canvas.transform);
        tile.GetComponent<RectTransform>().sizeDelta = new Vector2(rate, rate);


        return tile;
    }
    private void makeTile(string tileName, Vector3 playerPosition, Vector3 mapPosition, float H, float S, float V)
    {
        GameObject tile = new GameObject(tileName);
        UnityEngine.UI.Image tileImage = tile.AddComponent<UnityEngine.UI.Image>();
        tileImage.color = UnityEngine.Color.HSVToRGB(H, S, V);

        Vector3 localPosition = playerPosition - mapPosition;
        localPosition.y = localPosition.z;
        localPosition.z = 0;

        Vector3 canvasPos = canvas.transform.position;
        Vector2 wh = canvas.GetComponent<RectTransform>().sizeDelta;

        canvasPos = canvasPos - new Vector3((wh.x + rate) / 2 - rate, (wh.y + rate) / 2 - rate);

        tile.transform.position = canvasPos + rate * (localPosition / (float)tilesSize);
        tile.transform.SetParent(canvas.transform);
        tile.GetComponent<RectTransform>().sizeDelta = new Vector2(rate, rate);
    }

    private void updateTile(GameObject tile, Vector3 playerPosition, Vector3 mapPosition)
    {

        Vector3 localPosition = playerPosition - mapPosition;
        localPosition.y = localPosition.z;
        localPosition.z = 0;

        Vector3 canvasPos = canvas.transform.position;
        Vector2 wh = canvas.GetComponent<RectTransform>().sizeDelta;

        canvasPos = canvasPos - new Vector3((wh.x + rate) / 2 - rate, (wh.y + rate) / 2 - rate);

        tile.transform.position = canvasPos + rate * (localPosition / (float)tilesSize);
    }

    private void SpawnObjLab(string[] columns, int lineNumber, int col)
    {
        Vector3 position = new Vector3(col, lineNumber, 0);
        switch (columns[col])
        {
            //mur
            case "N":
                makeTileFromCSV("MapWall", position, 0, 0, 0);
                break;
            //sol
            case "W":
                makeTileFromCSV("MapGround", position, 0, 0, 1);
                break;
            //tp orange
            case "O":
                makeTileFromCSV("MapOTP", position, (float)0.1, 1, 1);
                break;
            //depart ghost   
            case "C":
                makeTileFromCSV("MapStartGhost", position, (float)0.5, 1, 1);
                break;
            //safe zone
            case "G":
                makeTileFromCSV("MapSafeZone", position, (float)1/3, 1, 1);
                break;
            //tp jaune
            case "Y":
                makeTileFromCSV("MapYTP", position, (float)1/6, 1, 1);
                break;
            //tp rouge
            case "R":
                makeTileFromCSV("MapRTP", position, 0, 1, 1);
                break;
            //tp violet
            case "P":
                makeTileFromCSV("MapPTP", position, (float)23/30, 1, 1);
                break;
        }
    }
}
