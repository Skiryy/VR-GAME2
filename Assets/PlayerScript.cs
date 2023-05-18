using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player.transform.position = spawnPoint.transform.position;
        player.transform.rotation = spawnPoint.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
