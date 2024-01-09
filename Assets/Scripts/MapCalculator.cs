using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MapCalculator : MonoBehaviour
{
    [SerializeField] public int stageNumber;
    [SerializeField] public float tilesSize;
    [SerializeField] public Canvas canvas;
    [SerializeField] public GameObject wall;
    [SerializeField] public Sprite wallSprite;
    [SerializeField] public Sprite groundSprite;
    [SerializeField] public Sprite playerSprite;
    [SerializeField] public Sprite ghostSprite;
    [SerializeField] public Sprite ghostSpawnSprite;
    [SerializeField] public Sprite safeZoneSprite;
    [SerializeField] public Sprite elevatorSprite;
    [SerializeField] public Sprite coinSprite;
    [SerializeField] public Sprite tp1Sprite;
    [SerializeField] public Sprite tp2Sprite;
    private float rate = (float)0.125;
    private GameObject MapPlayer;
    private Dictionary<GameObject, GameObject> MapAIs = new Dictionary<GameObject, GameObject>();

    void Start()
    {
        ReadCSVFile();
        foreach (GameObject coin in ScriptGameManager.SGM.GetListCoins(stageNumber - 1))
        {
            GameObject coinMinimap = makeTile("MapCoin", coin.transform.position, wall.transform.position);
            coinMinimap.GetComponent<Image>().sprite = coinSprite;
            CoinBehavior coinBehavior = coin.AddComponent<CoinBehavior>();
            coinBehavior.setMinimapCoin(coinMinimap);
        }
        LocatePlayerAndAIS();
    }

    private void Update()
    {
        updateTile(MapPlayer, ScriptGameManager.SGM.GetTransformPlayer().position, wall.transform.position);

        foreach (KeyValuePair<GameObject, GameObject> pair in MapAIs) 
        {
            GameObject AI = pair.Key;
            GameObject mapAI = pair.Value;
            if (AI != null)
            {
                updateTile(mapAI, AI.transform.position, wall.transform.position);
            } else
            {
                removeTile(mapAI);
            }
        }
    }

    void removeTile(GameObject mapAI)
    {
        if (mapAI != null)
        {
            Destroy(mapAI);
        }
    }

    void LocatePlayerAndAIS()
    {
        List<GameObject> AIs = ScriptGameManager.SGM.GetListIAsStage(stageNumber-1);
        for (int i = 0; i < ScriptGameManager.SGM.GetListIAsStage(stageNumber-1).Count; i++)
        {
            MapAIs[AIs[i]] = makeTileFromCSV("MapAI"+i, AIs[i].transform.position);
            MapAIs[AIs[i]].GetComponent<Image>().sprite = ghostSprite;
        }

        MapPlayer = makeTileFromCSV("MapPlayer", ScriptGameManager.SGM.GetTransformPlayer().position);
        MapPlayer.GetComponent<Image>().sprite = playerSprite;
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

    private GameObject makeTileFromCSV(string tileName, Vector3 position)
    {
        GameObject tile = new GameObject(tileName);
        Image tileImage = tile.AddComponent<Image>();
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
    private GameObject makeTile(string tileName, Vector3 playerPosition, Vector3 mapPosition)
    {
        GameObject tile = new GameObject(tileName);
        Image tileImage = tile.AddComponent<Image>();

        Vector3 localPosition = playerPosition - mapPosition;
        localPosition.y = localPosition.z;
        localPosition.z = 0;

        Vector3 canvasPos = canvas.transform.position;
        Vector2 wh = canvas.GetComponent<RectTransform>().sizeDelta;

        canvasPos = canvasPos - new Vector3((wh.x + rate) / 2 - rate, (wh.y + rate) / 2 - rate);

        tile.transform.position = canvasPos + rate * (localPosition / (float)tilesSize);
        tile.transform.SetParent(canvas.transform);
        tile.GetComponent<RectTransform>().sizeDelta = new Vector2(rate, rate);
        return tile;
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
        GameObject tile = null;
        switch (columns[col])
        {
            //mur
            case "N":
                tile = makeTileFromCSV("MapWall", position); 
                tile.GetComponent<Image>().sprite = wallSprite;
                break;
            //sol
            case "W":
                tile = makeTileFromCSV("MapGround", position);
                tile.GetComponent<Image>().sprite = groundSprite;
                break;
            //tp orange
            case "O":
                tile = makeTileFromCSV("MapOTP", position);
                tile.GetComponent<Image>().sprite = elevatorSprite;
                break;
            //depart ghost   
            case "C":
                tile = makeTileFromCSV("MapStartGhost", position);
                tile.GetComponent<Image>().sprite = ghostSpawnSprite;
                break;
            //safe zone
            case "G":
                tile = makeTileFromCSV("MapSafeZone", position);
                tile.GetComponent<Image>().sprite = safeZoneSprite;
                break;
            //tp jaune
            case "Y":
                tile = makeTileFromCSV("MapYTP", position);
                tile.GetComponent<Image>().sprite = elevatorSprite;
                break;
            //tp rouge
            case "R":
                GameObject tileGround = makeTileFromCSV("MapGround", position);
                tileGround.GetComponent<Image>().sprite = groundSprite;
                tile = makeTileFromCSV("MapRTP", position);
                tile.GetComponent<Image>().sprite = tp1Sprite;
                break;
            //tp violet
            case "P":
                GameObject tileGround2 = makeTileFromCSV("MapGround", position);
                tileGround2.GetComponent<Image>().sprite = groundSprite;
                tile = makeTileFromCSV("MapPTP", position);
                tile.GetComponent<Image>().sprite = tp2Sprite;
                break;
        }
    }
}
