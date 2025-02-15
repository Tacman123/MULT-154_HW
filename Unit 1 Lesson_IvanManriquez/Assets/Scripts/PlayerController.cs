using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rbPlayer;

    private Vector3 direction = Vector3.zero;
    [SerializeField]

    private float forceMultiplier = 1.0f;
    [SerializeField]

    private ForceMode forceMode;

    public GameObject spawnPoint;
    private Dictionary<Item.VegetebleType, int> inventory = new Dictionary<Item.VegetebleType, int>();    
    
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();

        // populate inventory dictionary with vegetable types and their counts (0 by default)
        foreach (Item.VegetebleType type in System.Enum.GetValues(typeof(Item.VegetebleType)))
        {
            inventory.Add(type, 0);
        }
    }

    void Update()
    {
        // local variables - they're local to Upddate, but not to other methods
        float horizontalVelocity = Input.GetAxis("Horizontal");
        float verticalVelocity = Input.GetAxis("Vertical");

        direction = new Vector3(horizontalVelocity, 0, verticalVelocity);
    }

    // FixedUpdate is called once per frame, along with Unity's physics engine
    void FixedUpdate()
    {                
        rbPlayer.AddForce(direction * forceMultiplier, forceMode);

        if (transform.position.z > 38)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 38);
        }
        else if (transform.position.z < -38)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -38);
        }
    }

    private void Respawn()
    {
        rbPlayer.MovePosition(spawnPoint.transform.position);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Item"))
        {
            Item item = collider.gameObject.GetComponent<Item>();
            AddItemToInventory(item);
            PrintInventory();
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

    private void OnTriggerExit(Collider collider)
    {
        if(collider.CompareTag("Hazard"))
        {
            Respawn();
        }
    }
}
