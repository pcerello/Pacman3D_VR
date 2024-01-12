using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using static Unity.VisualScripting.Member;

public class ScriptGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private int points;
    [SerializeField] private ScriptSceneManager ssm;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerHand;
    [SerializeField] private GameObject mapPrefab;
    [SerializeField] public TMP_Text scoreText;
    private ScripTableMap tableMap;
    [SerializeField] private AudioClip sfxWin;
    [SerializeField] private AudioClip sfxLose;
    [SerializeField] private AudioClip sfxElevator;
    [SerializeField] private AudioClip sfxNegative;
    private AudioSource source;

    public static ScriptGameManager SGM;
    private GameObject[] maps;
    private int totalCoins;
    private Boolean inGame;

    private void Awake()
    {
        if(SGM == null)
        {
            SGM = this;
        }
        else
        {
            Destroy(this );
        }
        inGame = true;
        tableMap = player.GetComponent<ChangeHand>().GetTableMap();
        source = GetComponent<AudioSource>();
    }

    void Start()
    {
        points = 0;
        totalCoins = 0;

        for (int i = 0; i < ssm.GetListParentStage().Length; i++)
        {
            int coins = ssm.GetNbrCoinsInStage(i);
            totalCoins += coins;
            tableMap.CreateLine(i, coins);
        }
        //scoreText.text = "Score : " + points.ToString() + " / " + totalCoins.ToString();


        playerHand = playerHand.gameObject.GetComponentInChildren<Transform>(true);
    }

    public void initMaps()
    {
        ParentStageScript[] list = ssm.GetListParentStage();
        for (int i = 0; i < 12; i++)
        {
            GameObject map = Instantiate(mapPrefab);
            map.GetComponent<MapCalculator>().stageNumber = i + 1;
            map.transform.SetParent(playerHand);
            map.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, (float)2.5);
        }
    }

    public void PlaySfx(AudioClip sfx, float pitch, float volume)
    {
        source.pitch = pitch;
        source.PlayOneShot(sfx, volume);
    }

    public void CollectCoin(GameObject coin)
    {
        points += 1;
        ssm.GetParentStage().RemoveCoin(coin);

        coin.GetComponent<CoinBehavior>().DestroyCoin();
        scoreText.text = "Score : " + points.ToString() + " / " + totalCoins.ToString();
        
        tableMap.SetValue(ssm.GetCurrentStage(), ssm.GetParentStage().GetNbrCoins());

        if(points == totalCoins)
        {
            Win();
        }
    }

    public void Win()
    {
        ssm.GoToWinArea(player);
        inGame = false;
        PlaySfx(sfxWin, 1f, 1f);
    }

    public void GameOver()
    {
        if (inGame)
        {
            ssm.GoToLoseArea(player);
            PlaySfx(sfxLose, 1f, 1f);
        }
        inGame = false;
    }

    public ScriptSceneManager GetScriptSceneManager { get { return ssm; } }

    public void AddPoints(int value)
    {
        points += value;
    }

    public void UpStage(Elevator tp)
    {
        
        if(ssm.UpStage(player, tp))
        {
            PlaySfx(sfxElevator, 1f, 1f);
        }
        else
        {
            PlaySfx(sfxNegative, 1f, 1f);
        }
    }

    public void DownStage(Elevator tp)
    {
        if (ssm.DownStage(player, tp))
        {
            PlaySfx(sfxElevator, 1f, 1f);
        }
        else
        {
            PlaySfx(sfxNegative, 1f, 1f);
        }
    }

    public List<GameObject> GetListCoins()
    {
        return ssm.GetListCoins();
    }

    public List<GameObject> GetListCoins(int id)
    {
        return ssm.GetListCoins(id);
    }

    public int GetNumberParentStage()
    {
        return ssm.GetListParentStage().Length;
    }

    public Transform GetTransformPlayer()
    {
        return player.transform;
    }

    public List<GameObject> GetListIAsStage()
    {
        return ssm.GetListIAs();
    }

    public List<GameObject> GetListIAsStage(int id)
    {
        return ssm.GetListIAs(id);
    }

    public int GetNbrIAsInStage(int id)
    {
        return ssm.GetNbrIAsInStage(id);
    }

    public Transform GetPlayerHand()
    {
        return playerHand;
    }

    public GameObject GetCurrentMap()
    {
        return ssm.GetParentStage().GetMiniMap();
    }
}
