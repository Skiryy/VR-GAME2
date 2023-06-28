using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject player;
    public TextMeshProUGUI playerHealthText;
    public int playerHealth = 100;
    // Start is called before the first frame update
    void Start()
    {
        player.transform.position = spawnPoint.transform.position;
        player.transform.rotation = spawnPoint.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        playerHealthText.text = "Health:" + playerHealth;
        if (playerHealth <= 0){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
       }
    }
    public void TakeDamage(int damage){
        playerHealth -= damage;
        Debug.Log(playerHealth);
    }
}
