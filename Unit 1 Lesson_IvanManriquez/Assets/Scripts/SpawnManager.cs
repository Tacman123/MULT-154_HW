using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class SpawnManager : NetworkBehaviour
{    
    public GameObject[] lilyPads;

    public float respawnTime = 5.0f;

    // Start is called before the first frame update
    public override void OnNetworkSpawn()
    {
        if (!IsServer)
        {
            return;
        }
        InvokeRepeating("SpawnLilyPad", 2.0f, respawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnLilyPad()
    {
        foreach (GameObject lilyPad in lilyPads)
        {
            NetworkObject lilyPadObject = Instantiate(lilyPad).GetComponent<NetworkObject>();
            lilyPadObject.Spawn();
        }        
    }
}
