using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class Bug_Enemy : MonoBehaviour
{
    Vector3 Destination;
    public float Step;
    public float LockStep;
    public float Lerp;
    public float MinimalDistance;
    private GameObject Player;
    public event Action OnEnemyDeath; 
    public bool LockOn;
    public BugSystem System;
    public float LockHeight;
    public float startingHealth = 50f;
    private float currentHealth;
    AudioSource m_damageSound;
    // Start is called before the first frame update
    void Start()
    {
        m_damageSound = GetComponent<AudioSource>();
        Destination = transform.position;
        Player = GameObject.Find("Player");
        LockOn = false;
        System = GameObject.Find("Bug controller").GetComponent<BugSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!LockOn){
            if (Vector3.Distance(Destination, transform.position) <= MinimalDistance)
            {
                SetDestination();
            }
            else
            {
                transform.transform.position = Vector3.MoveTowards(transform.position, Destination, Mathf.Clamp(Mathf.Lerp(Vector3.Distance(transform.position, Destination),0,Lerp),0, Step) * Time.deltaTime);
            }
        }
        else{
            Destination = Player.transform.position;
            transform.transform.position = Vector3.MoveTowards(transform.position, Destination, LockStep * Time.deltaTime);
        }
        transform.LookAt(Destination, Vector3.forward);
        if(Player.transform.position.y >= System.LockHeight && System.BugsLocked <= (System.MaxLocked - 1)){
            LockOn = true;
            System.BugsLocked += 1;
        }
    }

    void SetDestination()
    {
        float RandomX = UnityEngine.Random.Range(-8, 111);
        float RandomY = UnityEngine.Random.Range(-70, 4);
        Destination = new Vector3(RandomX, 21.8088f, RandomY);
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

    private void OnDestroy()
    {
        System.BugsLocked -= 1;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("playerTag"))
        {
            Debug.Log("Hit");
            collision.gameObject.GetComponent<PlayerScript>().TakeDamage(5f);
            GetComponent<BoxCollider>().enabled = false;
            Destroy(this.gameObject);
            OnEnemyDeath?.Invoke(); // Invoke the OnEnemyDeath event


        }
    }
    private void Die()
    {
        Destroy(gameObject);
        OnEnemyDeath?.Invoke();
    }
}
