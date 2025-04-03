using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Swarm : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> waypoints;
    [SerializeField]
    private float WAYPOINT_THRESHOLD = 1.0f;

    private int waypointIndex;    
    private NavMeshAgent agent;
    private Bot bot;
    private bool hiveNotPickedUp = true;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        bot = GetComponent<Bot>();

        // register for the hive picked up event
        HivePickUp.HivePickedUp += OnHivePickedUp;

        agent.SetDestination(waypoints[waypointIndex].transform.position);
    }

    private void OnHivePickedUp()
    {
        hiveNotPickedUp = false;
    }

    // Update is called once per frame
    public void Patrol()
    {
        //if close enough to waypoint, then advance index
        if(Vector3.Distance(transform.position, waypoints[waypointIndex].transform.position) < WAYPOINT_THRESHOLD)
        {
            waypointIndex++;

            //wrap index around to 0 if we reach the end
            if(waypointIndex == waypoints.Count)
            {
                waypointIndex = 0;
            }

            agent.SetDestination(waypoints[waypointIndex].transform.position);
        }
    }

    private void Update()
    {
        if(hiveNotPickedUp)
        {
            Patrol();
        }
        else
        {
            bot.Pursue();
        }
    }
}
