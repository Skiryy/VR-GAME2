using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bug : MonoBehaviour
{
    Vector3 Destination;
    public float Step;
    public float Lerp;
    public float MinimalDistance;
    // Start is called before the first frame update
    void Start()
    {
        Destination = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(Destination, transform.position) <= MinimalDistance)
        {
            SetDestination();
        }
        else
        {
            transform.transform.position = Vector3.MoveTowards(transform.position, Destination, Mathf.Clamp(Mathf.Lerp(Vector3.Distance(transform.position, Destination),0,Lerp),0, Step) * Time.deltaTime);
        }
        transform.LookAt(Destination, Vector3.forward);
    }

    void SetDestination()
    {
        float RandomX = Random.Range(-8, 111);
        float RandomY = Random.Range(-70, 4);
        Destination = new Vector3(RandomX, 21.8088f, RandomY);
    }
}
