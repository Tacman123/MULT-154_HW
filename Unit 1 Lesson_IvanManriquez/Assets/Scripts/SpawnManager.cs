using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{    
    public GameObject[] lilyPads;

    public float respawnTime = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
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
            Instantiate(lilyPad);
        }        
    }
}
