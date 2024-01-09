using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundHazard : MonoBehaviour
{
    [SerializeField] private AudioClip[] sfx;
    [SerializeField] private Vector2 randPitch = Vector2.one;
    [SerializeField] private bool playOnAwake = true;

    private AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        RandomSound();
        source.playOnAwake = false;
    }

    private void Start()
    {
        if(playOnAwake)
        {
            LaunchSoud();
        }
    }

    private void RandomSound()
    {
        source.clip = sfx[Random.Range(0, sfx.Length)];
    }

    public void LaunchSoud()
    {
        source.pitch = Random.Range(randPitch.x, randPitch.y);
        source.Play();
    }
}
