using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MenuButton : MonoBehaviour
{

    [SerializeField] private GameObject canvas;

    private bool _pause = false;

    private void Start()
    {
        if(canvas != null)
        {
            canvas.SetActive(false);
        }
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
