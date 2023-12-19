using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    public Transform player, destination;
    public GameObject playerg;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerg.SetActive(false);
            player.position = destination.position;
            playerg.SetActive(true);
        }
    }
}
