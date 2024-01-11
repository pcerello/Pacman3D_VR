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
    [SerializeField] private Vector3 posCarte;
    [SerializeField] private Vector3 rotCarte;

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
        rt.rotation = Quaternion.Euler(rotCarte.x, rotCarte.y, rotCarte.z);
        wh = rt.sizeDelta;
        rate = (wh.x / 36);
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(ScriptGameManager.SGM.GetPlayerHand());
        this.GetComponent<RectTransform>().localPosition = posCarte;
        player = SpawnSpriteGame(spritePlayer, ScriptGameManager.SGM.GetTransformPlayer().localPosition);
        SpawnCoins();
    }

    private void FixedUpdate()
    {
        
        updateTile(player, (ScriptGameManager.SGM.GetTransformPlayer().localPosition - stageObject.transform.position), stageObject.position);
        /*
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

    
    private void updateTile(GameObject tile, Vector3 entityPosition, Vector3 mapPosition)
    {

        Vector3 localPosition = entityPosition;
        localPosition.y = localPosition.z;
        localPosition.z = 0;

        tile.GetComponent<RectTransform>().localPosition = (localPosition/1.5f * rate - new Vector3((wh.x- rate )/ 2, (wh.y-rate)/2,0));
    }

    private void SpawnCoins()
    {
        foreach (var coin in ScriptGameManager.SGM.GetListCoins(stageNum))
        {
            GameObject o = SpawnSpriteGame(spriteCoin, coin.transform.parent.localPosition);
            coin.GetComponent<CoinBehavior>().setMinimapCoin(o);
        }
    }

    public void SetId(int id)
    {
        stageNum = id;
    }

    private GameObject SpawnSpriteGame(Sprite game, Vector3 pos)
    {
        GameObject tile = Instantiate(imageObject, Vector3.zero, Quaternion.identity, this.transform);
        RectTransform rtransform = tile.GetComponent<RectTransform>();
        rtransform.position = pos;
        rtransform.localEulerAngles = Vector3.zero;
        tile.GetComponent<Image>().sprite = game;

        updateTile(tile, pos, stageObject.position);
       
        return tile;
    }

}
