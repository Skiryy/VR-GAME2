using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public float moveSpeed = 5f;
    public float attackDistance = 10;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (Vector3.Distance(rb.transform.position, player.transform.position) < attackDistance){
            rb.velocity=Vector3.zero;
        }
        else{

        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();

        rb.velocity = new Vector3(direction.x * moveSpeed, rb.velocity.y, direction.z * moveSpeed);
    }
    }
}