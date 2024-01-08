using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.AI.Navigation;
using UnityEditor;

public class CreateLab : MonoBehaviour
{
    [SerializeField] private float size = 1;
    [SerializeField] private int stage = 1;
    [SerializeField] private GameObject mur;
    [SerializeField] private GameObject sol;
    [SerializeField] private GameObject tp_orange;
    [SerializeField] private GameObject tp_jaune;
    [SerializeField] private GameObject tp_violet;
    [SerializeField] private GameObject tp_rouge;
    [SerializeField] private GameObject spawn_ghost;
    [SerializeField] private GameObject safe_zone;

    private GameObject stageParent;
    private GameObject walls;
    private GameObject grounds;
    private GameObject teleporters;
    private GameObject monsterspawns;
    private GameObject safezones;
#if UNITY_STANDALONE_WIN
    void Start()
    {
        stageParent = new GameObject();
        stageParent.name = "Stage"+stage;
        stageParent.transform.position = Vector3.zero;

        walls = CreateBlock("Walls", false);
        grounds = CreateBlock("Grounds", true);
        teleporters = CreateBlock("Teleporters", true);
        monsterspawns = CreateBlock("MonsterSpawns", true);
        safezones = CreateBlock("SafeZones", false);

        ReadCSVFile();

        NavMeshSurface surface = grounds.GetComponent<NavMeshSurface>();
        surface.layerMask = LayerMask.GetMask("Default");

        surface.AddData(); // Add the data of the environment
        surface.BuildNavMesh(); // Build the area explorable by AIs
    }

    void ReadCSVFile()
    {
        // Liste pour stocker les données lues à partir du fichier CSV
        List<string[]> csvData = new List<string[]>();

        // Lire le fichier CSV ligne par ligne
        using (StreamReader reader = new StreamReader("Assets/CSV/etage" + stage + ".csv"))
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
    private GameObject CreateBlock(string varName, bool explorable)
    {
        GameObject obj = new GameObject(varName);
        obj.transform.SetParent(stageParent.transform);
        obj.transform.position = Vector3.zero;
        if (!explorable)
        {
            NavMeshModifier modifierN = obj.AddComponent<NavMeshModifier>();
            modifierN.overrideArea = true;
            modifierN.area = 1;
        }
        else
        {
            obj.AddComponent<NavMeshSurface>();
        }

        return obj;
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
                creation.transform.parent = walls.transform; // Non explorable (wall)
                break;
            // sol
            case "W":
                creation = (GameObject)PrefabUtility.InstantiatePrefab(sol);
                creation.transform.parent = grounds.transform; // Explorable (ground)
                break;
            // tp orange
            case "O":
                creation = (GameObject)PrefabUtility.InstantiatePrefab(tp_orange);
                creation.transform.parent = teleporters.transform; // Explorable (teleporter)
                break;
            // depart ghost
            case "C":
                creation = (GameObject)PrefabUtility.InstantiatePrefab(spawn_ghost);
                creation.transform.parent = monsterspawns.transform; // Explorable (monster spawn)
                break;
            // safe zone
            case "G":
                creation = (GameObject)PrefabUtility.InstantiatePrefab(safe_zone);
                creation.transform.parent = safezones.transform; // Non explorable (safe zone)
                break;
            // tp jaune
            case "Y":
                creation = (GameObject)PrefabUtility.InstantiatePrefab(tp_jaune);
                creation.transform.parent = teleporters.transform; // Explorable (teleporter)
                break;
            // tp rouge
            case "R":
                creation = (GameObject)PrefabUtility.InstantiatePrefab(tp_rouge);
                creation.transform.parent = teleporters.transform; // Explorable (teleporter)
                break;
            // tp violet
            case "P":
                creation = (GameObject)PrefabUtility.InstantiatePrefab(tp_violet);
                creation.transform.parent = teleporters.transform; // Explorable (teleporter)
                break;
            // case anti spawn
            case "A":
                creation = (GameObject)PrefabUtility.InstantiatePrefab(sol);
                creation.transform.parent = safezones.transform; // Explorable (Anti Spawn de coins)
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
#endif
}