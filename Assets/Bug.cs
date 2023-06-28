using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bug : MonoBehaviour
{
    public NavMeshAgent Agent;
    Vector3 Destination;
    // Start is called before the first frame update
    void Start()
    {
        Destination = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(Destination, transform.position) <= 0.5)
        {
            SetDestination();
        }
        
    }

    void SetDestination()
    {
        float RandomX = Random.Range(-8, 111);
        float RandomY = Random.Range(-70, 4);
        Destination = new Vector3(RandomX, 21.8088f, RandomY);
        Agent.SetDestination(Destination);
    }
}
