using UnityEngine;

public class ratAttack : MonoBehaviour
{
    public int damageAmount = 5;
    public string playerTag = "Player"; 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != 6){ 
            if (collision.gameObject.tag == "playerTag")
            {
                PlayerScript playerHealth = collision.gameObject.GetComponent<PlayerScript>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damageAmount);
                }
                Destroy(gameObject);
            }
            else{
                Destroy(gameObject);
            }

        }
        else{
            Debug.Log("ground touched");
        }
    }
}