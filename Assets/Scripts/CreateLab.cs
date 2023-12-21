using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.AI.Navigation;
using UnityEditor;

public class CreateLab : MonoBehaviour
{
    public string csvFilePath = "Assets/CSV/etage3.csv";

    [SerializeField] private int size = 1;
    [SerializeField] private GameObject mur;
    [SerializeField] private GameObject sol;
    [SerializeField] private GameObject tp_orange;
    [SerializeField] private GameObject tp_jaune;
    [SerializeField] private GameObject tp_violet;
    [SerializeField] private GameObject tp_rouge;
    [SerializeField] private GameObject spawn_ghost;
    [SerializeField] private GameObject safe_zone;

    void Start()
    {
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
            } else
            {
                explorableBlock.AddComponent<NavMeshSurface>();
            }
        }

        return explorableBlock;
    }

    private void SpawnObjLab(string[] columns, int lineNumber, int col)
    {
        Vector3 position = new Vector3(col, 0, lineNumber);

        switch (columns[col])
        {
            // mur
            case "N":
                GameObject n = (GameObject) PrefabUtility.InstantiatePrefab(mur);
                n.transform.position = position * size;
                GameObject walls = LocateOrCreateBlock("Walls", explorable:false);
                n.transform.parent = walls.transform; // Non explorable (wall)
                break;
            // sol
            case "W":
                GameObject w = (GameObject)PrefabUtility.InstantiatePrefab(sol);
                w.transform.position = position * size;
                GameObject ground = LocateOrCreateBlock("Grounds", explorable: true);
                w.transform.parent = ground.transform; // Explorable (ground)
                break;
            // tp orange
            case "O":
                GameObject o = (GameObject)PrefabUtility.InstantiatePrefab(tp_orange);
                o.transform.position = position * size;
                GameObject tpOrange = LocateOrCreateBlock("Teleporters", explorable: true);
                o.transform.parent = tpOrange.transform; // Explorable (teleporter)
                break;
            // depart ghost
            case "C":
                GameObject c = (GameObject)PrefabUtility.InstantiatePrefab(spawn_ghost);
                c.transform.position = position * size;
                GameObject monsterSpawns = LocateOrCreateBlock("MonsterSpawns", explorable: true);
                c.transform.parent = monsterSpawns.transform; // Explorable (monster spawn)
                break;
            // safe zone
            case "G":
                GameObject g = (GameObject)PrefabUtility.InstantiatePrefab(safe_zone);
                g.transform.position = position * size;
                GameObject safeZones = LocateOrCreateBlock("SafeZones", explorable: false);
                g.transform.parent = safeZones.transform; // Non explorable (safe zone)
                break;
            // tp jaune
            case "Y":
                GameObject y = (GameObject)PrefabUtility.InstantiatePrefab(tp_jaune);
                y.transform.position = position * size;
                GameObject tpJaune = LocateOrCreateBlock("Teleporters", explorable: true);
                y.transform.parent = tpJaune.transform; // Explorable (teleporter)
                break;
            // tp rouge
            case "R":
                GameObject r = (GameObject)PrefabUtility.InstantiatePrefab(tp_rouge);
                r.transform.position = position * size;
                GameObject tpRouge = LocateOrCreateBlock("Teleporters", explorable: true);
                r.transform.parent = tpRouge.transform; // Explorable (teleporter)
                break;
            // tp violet
            case "P":
                GameObject p = (GameObject)PrefabUtility.InstantiatePrefab(tp_violet);
                p.transform.position = position * size;
                GameObject tpViolet = LocateOrCreateBlock("Teleporters", explorable: true);
                p.transform.parent = tpViolet.transform; // Explorable (teleporter)
                break;
            default:
                print("Not exist");
                break;
        }
    }
}