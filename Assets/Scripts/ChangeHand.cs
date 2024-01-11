using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeHand : MonoBehaviour
{

    [SerializeField] private GameObject ui_hand;
    [SerializeField] private GameObject game_hand;
    [Space(4)]
    [SerializeField] private GameObject tableMap;

    [SerializeField] private bool ui_active = true;

    private bool save_ui_active;
    private int state_map = 0;

    private void Start()
    {
        save_ui_active = ui_active;
        ui_hand.SetActive(ui_active);
        game_hand.SetActive(!ui_active);
    }

    public void OnChange()
    {
        ui_active = !ui_active;
        ui_hand.SetActive(ui_active);
        game_hand.SetActive(!ui_active);
    }

    public void OnChangeMap()
    {
        if (!save_ui_active)
        {
            ChangeMap();
        }
    }

    public ScripTableMap GetTableMap()
    {
        return tableMap.GetComponent<ScripTableMap>();
    }

    private void ChangeMap()
    {
        switch (state_map)
        {
            case 0:
                ScriptGameManager.SGM.GetCurrentMap().SetActive(true);
                tableMap.SetActive(false);
                break;
            case 1:
                ScriptGameManager.SGM.GetCurrentMap().SetActive(false);
                tableMap.SetActive(true);
                break;
            case 2:
                ScriptGameManager.SGM.GetCurrentMap().SetActive(false);
                tableMap.SetActive(false);
                break;
            default:
                ScriptGameManager.SGM.GetCurrentMap().SetActive(false);
                tableMap.SetActive(false);
                break;
        }
        if(state_map < 2)
        {
            state_map++;
        }
        else
        {
            state_map = 0;
        }
    }
} 
