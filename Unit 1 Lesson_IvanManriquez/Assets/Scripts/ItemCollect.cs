using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ItemCollect : NetworkBehaviour
{
    public delegate void CollectItem(Item.VegetebleType item);

    public static event CollectItem ItemCollected;

    private Dictionary<Item.VegetebleType, int> inventory = new Dictionary<Item.VegetebleType, int>();
    private Collider collidingItem;

    private void Start()
    {
        // populate inventory dictionary with vegetable types and their counts (0 by default)
        foreach (Item.VegetebleType type in System.Enum.GetValues(typeof(Item.VegetebleType)))
        {
            inventory.Add(type, 0);
        }
    }

    void Update()
    {
        if (collidingItem != null && Input.GetKeyDown(KeyCode.Space))
        {
            Item item = collidingItem.gameObject.GetComponent<Item>();
            AddItemToInventory(item);
            ItemCollected?.Invoke(item.typeOfVeggie);
            PrintInventory();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!IsLocalPlayer)
        {
            return;
        }

        if (collider.CompareTag("Item"))
        {
            collidingItem = collider;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (!IsLocalPlayer)
        {
            return;
        }

        if (collider.CompareTag("Item"))
        {
            collidingItem = null;
        }        
    }

    private void AddItemToInventory(Item item)
    {
        inventory[item.typeOfVeggie]++;
    }

    private void PrintInventory()
    {
        string output = "";

        foreach (KeyValuePair<Item.VegetebleType, int> pair in inventory)
        {
            output += string.Format("{0}: {1}; ", pair.Key, pair.Value);
        }

        Debug.Log(output);
    }
}
