using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Rat_Enemy : MonoBehaviour
{
    public GameObject Player;
    public NavMeshAgent RatAI;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        RatAI.SetDestination(Player.transform.position);
        if(Vector3.Distance(transform.position, Player.transform.position) <= 8.5f)
        {
            RatAI.speed = 0.1f;
            RatAI.angularSpeed = 270;
        }
        else
        {
            RatAI.speed = 3.5f;
            RatAI.angularSpeed = 340;
        }

    }
}
