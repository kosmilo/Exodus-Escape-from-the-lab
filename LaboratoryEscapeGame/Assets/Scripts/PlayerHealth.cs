using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] GameOverScreenManager gameOverScreenManager;
    [SerializeField] GameObject damageFlashPanel;
    [SerializeField] int playerMaxHealth = 100;
    [SerializeField] int playerHealth;

    void Start()
    {
        playerHealth = playerMaxHealth;
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        playerHealth = Mathf.Clamp(playerHealth, 0, playerMaxHealth);

        if (playerHealth <= 0) {
            gameOverScreenManager.GameOver();
        }
        else {
            // Play an animation that flashes a red screen when taking damage
            damageFlashPanel.GetComponent<Animator>().Play("DamageFlash");
        }
    }

    public void HealPlayer(int heal)
    {
        playerHealth += heal;
        playerHealth = Mathf.Clamp(playerHealth, 0, playerMaxHealth);
    }
}
