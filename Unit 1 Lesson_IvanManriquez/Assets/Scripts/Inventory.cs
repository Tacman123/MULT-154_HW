using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Inventory : NetworkBehaviour
{
    public GameObject[] items;

    // Start is called before the first frame update
    void Start()
    {
        ItemCollect.ItemCollected += IncrementItem;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IncrementItem(Item.VegetebleType vegetebleType)
    {
        CountGUI count = items[(int)vegetebleType].GetComponent<CountGUI>();
        count.UpdateCountBroadcast();
    }
}
