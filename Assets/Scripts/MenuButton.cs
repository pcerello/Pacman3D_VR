using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Audio;
using static Unity.VisualScripting.Member;

public class MenuButton : MonoBehaviour
{

    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject firstMenu;
    [SerializeField] private GameObject secondMenu;
    [SerializeField] private GameObject optionMenu;

    private bool _pause = false;
    public AudioMixer audioMixerMusic;
    private AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

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
            optionMenu.SetActive(false);
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

    public void SelectOptions()
    {
        firstMenu.SetActive(false);
        optionMenu.SetActive(true);
    }

    public void UnSelectOptions()
    {
        optionMenu.SetActive(false);
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

    public void SetVolumeMusic(float volume)
    {
        audioMixerMusic.SetFloat("volumeMusic",volume);
    }

    public void SetVolumeSfx(float volume)
    {
        audioMixerMusic.SetFloat("volumeSfx", volume);
        source.Play();
    }
}
