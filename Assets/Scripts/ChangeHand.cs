using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeHand : MonoBehaviour
{

    [SerializeField] private GameObject ui_hand;
    [SerializeField] private GameObject game_hand;

    [SerializeField] private bool ui_active = true;

    private void Start()
    {
        ui_hand.SetActive(ui_active);
        game_hand.SetActive(!ui_active);
    }

    public void OnChange()
    {
        ui_active = !ui_active;
        ui_hand.SetActive(ui_active);
        game_hand.SetActive(!ui_active);
    }

}
