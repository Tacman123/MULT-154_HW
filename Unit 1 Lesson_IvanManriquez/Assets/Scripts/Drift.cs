using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drift : MonoBehaviour
{
    public float speed = 5.0f;
    public enum DriftDirection
    {
        LEFT = -1,
        RIGHT = 1
    }

    public DriftDirection driftDirection;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime * (int)driftDirection);

        if (transform.position.x < -90 || transform.position.x > 90)
        {
            Destroy(gameObject);
        }       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject player = collision.gameObject;
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject player = collision.gameObject;
            collision.gameObject.transform.SetParent(null);
        }
    }
}
