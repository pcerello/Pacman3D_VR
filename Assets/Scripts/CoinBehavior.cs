using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    private GameObject minimapLink;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setMinimapCoin(GameObject minimapCoin)
    {
        minimapLink = minimapCoin;
    }

    private void OnDestroy()
    {
        Destroy(minimapLink);
    }
}
