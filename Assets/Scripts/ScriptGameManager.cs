using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class ScriptGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private int points;
    [SerializeField] private ScriptSceneManager ssm;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject mapPrefab;
    [SerializeField] public TMP_Text scoreText;
    [SerializeField] private ScripTableMap tableMap;
    [SerializeField] private AudioClip sfxHit;
    [SerializeField] private AudioClip sfxPowerUp;
    [SerializeField] private AudioClip sfxExplosion;
    [SerializeField] private AudioClip sfxPickup;
    [SerializeField] private AudioClip sfxGhost;
    [SerializeField] private AudioClip sfxWin;
    [SerializeField] private AudioClip sfxLose;
    private AudioSource sfxHitSource;

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
        sfxHitSource = GetComponent<AudioSource>();
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
        scoreText.text = "Score : " + points.ToString() + " / " + totalCoins.ToString();

        initMaps();
    }

    public void initMaps()
    {
        ParentStageScript[] list = ssm.GetListParentStage();
        for (int i = 0; i < list.Length; i++)
        {
            GameObject map = (GameObject)PrefabUtility.InstantiatePrefab(mapPrefab);
            map.GetComponent<MapCalculator>().stageNumber = i + 1;
            map.GetComponent<MapCalculator>().stageObject = list[i].gameObject;
            map.transform.parent = player.transform;
        }
    }

    public void PlaySfx(AudioClip sfx, float pitch, float volume)
    {
        sfxHitSource.pitch = pitch;
        sfxHitSource.PlayOneShot(sfx, volume);
    }

    public void CollectCoin(GameObject coin)
    {
        points += 1;
        ssm.GetParentStage().RemoveCoin(coin);
        Destroy(coin);
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
        ssm.UpStage(player, tp);
    }

    public void DownStage(Elevator tp)
    {
        ssm.DownStage(player, tp);
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
}
