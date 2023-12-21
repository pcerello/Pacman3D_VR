using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MenuButton : MonoBehaviour
{

    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject firstMenu;
    [SerializeField] private GameObject secondMenu;

    private bool _pause = false;

    private void Start()
    {
        if(canvas != null)
        {
            canvas.SetActive(false);
        }
        if(secondMenu != null && firstMenu != null)
        {
            firstMenu.SetActive(true);
            secondMenu.SetActive(false);
        }

    }

    public void SelectLevel()
    {
        firstMenu.SetActive(false);
        secondMenu.SetActive(true);
    }

    public void UnSelectLevel()
    {
        secondMenu.SetActive(false);
        firstMenu.SetActive(true);
    }

    public void PlayLevel(int id)
    {
        SceneManager.LoadScene(id);
    }

    public void QuitApp()
    {
        Application.Quit();
    }

    public void OnPause()
    {
        if (canvas != null)
        {
            _pause = !_pause;
            canvas.SetActive(_pause);
        }
    }
}
