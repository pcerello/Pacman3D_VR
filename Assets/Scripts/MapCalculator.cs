using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using TMPro;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MapCalculator : MonoBehaviour
{
    [SerializeField] public string filePath;
    [SerializeField] public Canvas canvas;
    private int size = 1;
    private float rate = (float)0.125;
    private 

    void Start()
    {
        ReadCSVFile();
        LocatePlayerAndAIS();
    }

    private void Update()
    {
        GameObject player = GameObject.Find("Player");
        GameObject AI1 = GameObject.Find("PlayerRandomIA");
        GameObject AI2 = GameObject.Find("PlayerLinearIA");

        updateTile("MapPlayer", player.transform.position);
        updateTile("MapAI1", AI1.transform.position);
        updateTile("MapAI2", AI2.transform.position);

    }

    void LocatePlayerAndAIS()
    {
        GameObject player = GameObject.Find("Player");
        GameObject AI1 = GameObject.Find("PlayerRandomIA");
        GameObject AI2 = GameObject.Find("PlayerLinearIA");

        makeTile("MapPlayer", player.transform.position, 0, 1, 1);
        makeTile("MapAI1", AI1.transform.position, (float)0.8, 1, 1);
        makeTile("MapAI2", AI2.transform.position, (float)0.9, 1, 1);
    }

    void ReadCSVFile()
    {
        // Liste pour stocker les données lues à partir du fichier CSV
        List<string[]> csvData = new List<string[]>();

        // Lire le fichier CSV ligne par ligne
        using (StreamReader reader = new StreamReader(filePath))
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

    private GameObject LocateOrCreateBlock(string block_name, bool explorable)
    {
        GameObject explorableBlock = GameObject.Find(block_name);

        // If ExplorableBlocks doesn't exist, create it
        if (explorableBlock == null)
        {
            explorableBlock = new GameObject(block_name);
            if (!explorable)
            {
                NavMeshModifier modifierN = explorableBlock.AddComponent<NavMeshModifier>();
                modifierN.overrideArea = true;
                modifierN.area = 1;
            }
            else
            {
                explorableBlock.AddComponent<NavMeshSurface>();
            }
        }

        return explorableBlock;
    }

    private void makeTile(string tileName, Vector3 position, float H, float S, float V)
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
    }

    private void updateTile(string tileName, Vector3 position)
    {
        GameObject tile = GameObject.Find(tileName);
        position.y = position.z;
        position.z = 0;

        Vector3 canvasPos = canvas.transform.position;
        Vector2 wh = canvas.GetComponent<RectTransform>().sizeDelta;
        canvasPos = canvasPos - new Vector3((wh.x + rate) / 2 - rate, (wh.y + rate) / 2 - rate);
        tile.transform.position = canvasPos + rate * (position/3);
    }

    private void SpawnObjLab(string[] columns, int lineNumber, int col)
    {
        Vector3 position = new Vector3(col, lineNumber, 0);
        switch (columns[col])
        {
            //mur
            case "N":
                makeTile("MapWall", position, 0, 0, 0);
                break;
            //sol
            case "W":
                makeTile("MapGround", position, 0, 0, 1);
                break;
            //tp orange
            case "O":
                makeTile("MapOTP", position, (float)0.1, 1, 1);
                break;
            //depart ghost   
            case "C":
                makeTile("MapStartGhost", position, (float)0.5, 1, 1);
                break;
            //safe zone
            case "G":
                makeTile("MapSafeZone", position, (float)1/3, 1, 1);
                break;
            //tp jaune
            case "Y":
                makeTile("MapYTP", position, (float)1/6, 1, 1);
                break;
            //tp rouge
            case "R":
                makeTile("MapRTP", position, 0, 1, 1);
                break;
            //tp violet
            case "P":
                makeTile("MapPTP", position, (float)23/30, 1, 1);
                break;
        }
    }
}
