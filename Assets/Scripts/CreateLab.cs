using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.AI.Navigation;
using UnityEditor;
using System.Diagnostics.Tracing;

public class CreateLab : MonoBehaviour
{
    public string csvFilePath = "Assets/CSV/etage3.csv";

    [SerializeField] private float size = 1;
    [SerializeField] private GameObject mur;
    [SerializeField] private GameObject sol;
    [SerializeField] private GameObject tp_orange;
    [SerializeField] private GameObject tp_jaune;
    [SerializeField] private GameObject tp_violet;
    [SerializeField] private GameObject tp_rouge;
    [SerializeField] private GameObject spawn_ghost;
    [SerializeField] private GameObject safe_zone;

    private GameObject stageParent;

    void Start()
    {
        stageParent = new GameObject();
        stageParent.name = "Stage";
        stageParent.transform.position = Vector3.zero;
        ReadCSVFile();
        GameObject ground = LocateOrCreateBlock("Grounds", true);
        NavMeshSurface surface = ground.GetComponent<NavMeshSurface>();
        surface.layerMask = LayerMask.GetMask("Default");

        surface.AddData(); // Add the data of the environment
        surface.BuildNavMesh(); // Build the area explorable by AIs
    }

    void ReadCSVFile()
    {
        // Liste pour stocker les données lues à partir du fichier CSV
        List<string[]> csvData = new List<string[]>();

        // Lire le fichier CSV ligne par ligne
        using (StreamReader reader = new StreamReader(csvFilePath))
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
        GameObject explorableBlock = GameObject.Find(block_name);//TODO : Replace with dictionnary and reference -> faster

        // If ExplorableBlocks doesn't exist, create it
        if (explorableBlock == null)
        {
            explorableBlock = new GameObject(block_name);
            explorableBlock.transform.SetParent(stageParent.transform);
            explorableBlock.transform.position = Vector3.zero;
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

    private void SpawnObjLab(string[] columns, int lineNumber, int col)
    {
        Vector3 position = new Vector3(col, 0, lineNumber);
        GameObject creation = null;
        switch (columns[col])
        {
            // mur
            case "N":
                creation = (GameObject)PrefabUtility.InstantiatePrefab(mur);
                GameObject walls = LocateOrCreateBlock("Walls", explorable: false);
                creation.transform.parent = walls.transform; // Non explorable (wall)

                break;
            // sol
            case "W":
                creation = (GameObject)PrefabUtility.InstantiatePrefab(sol);
                GameObject ground = LocateOrCreateBlock("Grounds", explorable: true);
                creation.transform.parent = ground.transform; // Explorable (ground)
                break;
            // tp orange
            case "O":
                creation = (GameObject)PrefabUtility.InstantiatePrefab(tp_orange);
                GameObject tpOrange = LocateOrCreateBlock("Teleporters", explorable: true);
                creation.transform.parent = tpOrange.transform; // Explorable (teleporter)
                break;
            // depart ghost
            case "C":
                creation = (GameObject)PrefabUtility.InstantiatePrefab(spawn_ghost);
                GameObject monsterSpawns = LocateOrCreateBlock("MonsterSpawns", explorable: true);
                creation.transform.parent = monsterSpawns.transform; // Explorable (monster spawn)
                break;
            // safe zone
            case "G":
                creation = (GameObject)PrefabUtility.InstantiatePrefab(safe_zone);
                GameObject safeZones = LocateOrCreateBlock("SafeZones", explorable: false);
                creation.transform.parent = safeZones.transform; // Non explorable (safe zone)
                break;
            // tp jaune
            case "Y":
                creation = (GameObject)PrefabUtility.InstantiatePrefab(tp_jaune);
                GameObject tpJaune = LocateOrCreateBlock("Teleporters", explorable: true);
                creation.transform.parent = tpJaune.transform; // Explorable (teleporter)
                break;
            // tp rouge
            case "R":
                creation = (GameObject)PrefabUtility.InstantiatePrefab(tp_rouge);
                GameObject tpRouge = LocateOrCreateBlock("Teleporters", explorable: true);
                creation.transform.parent = tpRouge.transform; // Explorable (teleporter)
                break;
            // tp violet
            case "P":
                creation = (GameObject)PrefabUtility.InstantiatePrefab(tp_violet);
                GameObject tpViolet = LocateOrCreateBlock("Teleporters", explorable: true);
                creation.transform.parent = tpViolet.transform; // Explorable (teleporter)
                break;
            default:
                print("Not exist");
                break;
        }
        if (creation)
        {
            creation.transform.position = position * size;
            creation.transform.localScale = Vector3.one * size;
        }
    }
}