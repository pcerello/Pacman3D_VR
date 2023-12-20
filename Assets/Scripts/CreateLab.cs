using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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
        // Afficher les données lues pour vérification
        foreach (string[] row in csvData)
        {
            for(int i = 0; i<row.Length; i++)
            {
                SpawnObjLab(row, row_x, i);
            }
            row_x++;
        }
    }

    private void SpawnObjLab(string[] columns, int lineNumber, int col)
    {
        Vector3 position = new Vector3(col, 0, lineNumber);

        switch (columns[col])
        {
            //mur
            case "N":
                GameObject n = Instantiate(mur, position * size, Quaternion.identity);
                break;
            //sol
            case "W":
                GameObject w = Instantiate(sol, position * size, Quaternion.identity);
                break;
            //tp orange
            case "O":
                GameObject o = Instantiate(tp_orange, position * size, Quaternion.identity);
                break;
            //depart ghost   
            case "C":
                GameObject c = Instantiate(spawn_ghost, position * size, Quaternion.identity);
                break;
            //safe zone
            case "G":
                GameObject g = Instantiate(safe_zone, position * size, Quaternion.identity);
                break;
            //tp jaune
            case "Y":
                GameObject y = Instantiate(tp_jaune, position * size, Quaternion.identity);
                break;
            //tp rouge
            case "R":
                GameObject r = Instantiate(tp_rouge, position * size, Quaternion.identity);
                break;
            //tp violet
            case "P":
                GameObject p = Instantiate(tp_violet, position * size, Quaternion.identity);
                break;
            // Add more cases for other letters if needed
            default:
                print("Not exist");
                break;
        }
    }
}
