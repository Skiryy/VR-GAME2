using System;
using UnityEngine;
using UnityEngine.AI;

public class Rat_Enemy : MonoBehaviour
{
    private bool canShoot = true;
    public NavMeshAgent RatAI;
    public float attackSpeed = 10f;
    public GameObject ratAttackPrefab;
    public int ratAttackDamage = 10;
    public float attackDelay = 5f;
    public float startingHealth = 300f;
    private float currentHealth;
    private GameObject Player;
    AudioSource m_damageSound;


    public event Action OnEnemyDeath; 

    void Start()
    {
        Player = GameObject.Find("Player");
        m_damageSound = GetComponent<AudioSource>();
        currentHealth = startingHealth;
    }


    void Update()
    {
        RatAI.SetDestination(Player.transform.position);

        if (Vector3.Distance(transform.position, Player.transform.position) <= 8.5f)
        {
            RatAI.speed = 0.1f;
            RatAI.angularSpeed = 270;
            Shoot();
        }
        else
        {
            RatAI.speed = 3.5f;
            RatAI.angularSpeed = 340;
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
        m_damageSound.Play();
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }


    private void Shoot()
    {
        if (canShoot)
        {
            GameObject ratAttack = Instantiate(ratAttackPrefab, transform.position, Quaternion.identity);
            ratAttack.transform.position += Vector3.up * 5;
            Rigidbody ratAttackRigidbody = ratAttack.GetComponent<Rigidbody>();

            Vector3 direction = (Player.transform.position - transform.position).normalized;
            direction += Vector3.down * 0.4f;
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

    //private void OnCollisionEnter(Collision collision)
    //{
        //if (collision.gameObject.CompareTag("playerAttack"))
       // {
          //  Destroy(collision.gameObject);
          //  OnEnemyDeath?.Invoke();
      //  }
  //  }
    private void Die()
    {
        Destroy(gameObject);
        OnEnemyDeath?.Invoke();
    }
}
