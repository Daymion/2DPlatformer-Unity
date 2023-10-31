using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int playerScore = 0;

    void Start()
    {
        
    }

    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // If the player dies or hits and enemy restart the stage
        if (other.gameObject.tag == "Enemy")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (other.gameObject.tag == "DeathPlane")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the player interacts with a coin, turn the coin off and add points
        if (other.gameObject.tag == "Coin")
        {
            playerScore += 100;
            other.gameObject.SetActive(false);
            scoreText.SetText("Score: " + playerScore);
        }
    }
}
