using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(ScriptGameManager.SGM.GetPlayerHand());
        this.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
