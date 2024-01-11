using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapUpdate : MonoBehaviour
{

    [SerializeField] private GameObject imageObject;
    [SerializeField] private float tilesSize = 1.5f;

    [SerializeField] private Sprite spritePlayer;
    [SerializeField] private Sprite spriteGhost;
    [SerializeField] private Sprite spriteCoin;

    private Transform stageObject;

    private GameObject player;
    private GameObject[] list_ias;
    private Vector2 wh;
    private float rate;
    private int stageNum;

    private void Awake()
    {
        stageObject = this.transform.parent;

        RectTransform rt = GetComponent<RectTransform>();
        wh = rt.sizeDelta;
        rate = (wh.x / 36) * rt.localScale.x;
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(ScriptGameManager.SGM.GetPlayerHand());
        this.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 3);

        SpawnCoins();
    }

    private void FixedUpdate()
    {
        /**
        updateTile(ImageObject, ScriptGameManager.SGM.GetTransformPlayer().position, stageObject);
        
        foreach (KeyValuePair<GameObject, GameObject> pair in MapAIs) 
        {
            GameObject AI = pair.Key;
            GameObject mapAI = pair.Value;
            if (AI != null)
            {
                updateTile(mapAI, AI.transform.position, stageObject);
            } else
            {
                removeTile(mapAI);
            }
        }*/
        
    }

    
    private void updateTile(GameObject tile, Vector3 playerPosition, Vector3 mapPosition)
    {

        Vector3 localPosition = playerPosition - mapPosition;
        localPosition.y = localPosition.z;
        localPosition.z = 0;

        Vector3 canvasPos = getCanvasPos();

        tile.transform.position = canvasPos + rate * (localPosition / (float)tilesSize);
    }

    void removeTile(GameObject mapAI)
    {
        if (mapAI != null)
        {
            Destroy(mapAI);
        }
    }

    private void SpawnCoins()
    {
        foreach (var coin in ScriptGameManager.SGM.GetListCoins(stageNum))
        {
            SpawnSpriteGame(spriteCoin, coin.transform.position);
        }
    }

    public void SetId(int id)
    {
        stageNum = id;
    }

    private Vector3 getCanvasPos()
    {
        return transform.position - new Vector3((transform.localScale.x * wh.x / 2) - (rate / 2), (transform.localScale.y * wh.y / 2) - (rate / 2));
    }

    private void SpawnSpriteGame(Sprite game, Vector3 pos)
    {
        GameObject tile = Instantiate(imageObject, Vector3.zero, Quaternion.identity, this.transform);
        tile.GetComponent<RectTransform>().position = pos;
        tile.GetComponent<Image>().sprite = game;

        updateTile(tile, pos, this.transform.position);
    }

}
