using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Generate_Level : MonoBehaviour
{
    [SerializeField] private int size = 1;
    [SerializeField] private GameObject mur;
    [SerializeField] private GameObject sol;
    [SerializeField] private GameObject tp_orange;
    [SerializeField] private GameObject tp_jaune;
    [SerializeField] private GameObject tp_violet;
    [SerializeField] private GameObject tp_rouge;
    [SerializeField] private GameObject spawn_ghost;
    [SerializeField] private GameObject safe_zone;
    // Start is called before the first frame update
    void Start()
    {
         string filePath = "C:\\Users\\apelet\\Documents\\Pacman3D_VR\\Assets\\Scripts\\laby\\etape1.csv";

        if (!File.Exists(filePath))
        {
            Debug.LogError("CSV file not found: " + filePath);
            return;
        }

        StreamReader csvFilePath = new StreamReader(filePath);
        int lineNumber = 0;

        while (!csvFilePath.EndOfStream)
        {
            string line = csvFilePath.ReadLine();
            string[] columns = line.Split(',');

            for (int col = 0; col < columns.Length; col++)
            {
                Vector3 position = new Vector3(col, 0, lineNumber);

                switch (columns[col])
                {
                    //mur
                    case "N":
                        GameObject n = Instantiate(mur, position * size, Quaternion.identity);
                        n.transform.localScale = Vector3.one * size;
                        break;
                    //sol
                    case "W":
                        GameObject w = Instantiate(sol, position * size, Quaternion.identity);
                        w.transform.localScale = Vector3.one * size;
                        break;
                    //tp orange
                    case "O":
                        GameObject o = Instantiate(tp_orange, position * size, Quaternion.identity);
                        o.transform.localScale = Vector3.one * size;
                        break;
                    //depart ghost   
                    case "C":
                        GameObject c = Instantiate(spawn_ghost, position * size, Quaternion.identity);
                        c.transform.localScale = Vector3.one * size;
                        break;
                    //safe zone
                    case "G":
                        GameObject g = Instantiate(safe_zone, position * size, Quaternion.identity);
                        g.transform.localScale = Vector3.one * size;
                        break;
                    //tp jaune
                    case "Y":
                        GameObject y = Instantiate(tp_jaune, position * size, Quaternion.identity);
                        y.transform.localScale = Vector3.one * size;
                        break;
                    //tp rouge
                    case "R":
                        GameObject r = Instantiate(tp_rouge, position * size, Quaternion.identity);
                        r.transform.localScale = Vector3.one * size;
                        break;
                    //tp violet
                    case "P":
                        GameObject p = Instantiate(tp_violet, position * size, Quaternion.identity);
                        p.transform.localScale = Vector3.one * size;
                        break;
                        // Add more cases for other letters if needed
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
