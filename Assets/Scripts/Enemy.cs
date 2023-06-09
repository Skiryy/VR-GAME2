using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public float moveSpeed = 5f;
    public float attackDistance = 10;
    public float attackSpeed = 10f;
    public GameObject ratAttackPrefab;
    public int ratAttackDamage = 10;
    public float attackDelay = 5f;

    private Rigidbody rb;
    private bool canShoot = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Vector3.Distance(rb.transform.position, player.transform.position) < attackDistance)
        {
            rb.velocity = Vector3.zero;
            Shoot();
        }
        else
        {
            Vector3 direction = player.transform.position - transform.position;
            direction.Normalize();

            rb.velocity = new Vector3(direction.x * moveSpeed, rb.velocity.y, direction.z * moveSpeed);
        }
    }

private void Shoot()
{
    if (canShoot)
    {
        GameObject ratAttack = Instantiate(ratAttackPrefab, transform.position, Quaternion.identity);
        ratAttack.transform.position += Vector3.up * 3;
        Rigidbody ratAttackRigidbody = ratAttack.GetComponent<Rigidbody>();

        Vector3 direction = (player.transform.position - transform.position).normalized;
        direction += Vector3.down * 0.3f;
        ratAttackRigidbody.AddForce(direction * attackSpeed, ForceMode.VelocityChange);
        Physics.IgnoreCollision(ratAttack.GetComponent<Collider>(), GetComponent<Collider>());

        canShoot = false;
        Invoke("ResetShoot", attackDelay);
    }
}

    private void ResetShoot()
    {
        canShoot = true;
    }
}