using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    private GameObject minimapLink;
    [SerializeField] private GameObject particleDestroyed;
    [SerializeField] private Transform transformSpawn;

    public void setMinimapCoin(GameObject minimapCoin)
    {
        minimapLink = minimapCoin;
    }

    public void DestroyCoin()
    {
        Destroy(minimapLink);
        Instantiate(particleDestroyed, transformSpawn.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
