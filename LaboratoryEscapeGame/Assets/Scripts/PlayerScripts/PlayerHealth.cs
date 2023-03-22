using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] GameOverScreenManager gameOverScreenManager;
    [SerializeField] GameObject damageFlashPanel;
    [SerializeField] HealthBar healthBar;
    Rigidbody rb;
    [SerializeField] int playerMaxHealth = 100;
    [SerializeField] int playerHealth;

    void Start()
    {
        playerHealth = playerMaxHealth;
        rb = GetComponent<Rigidbody>();
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        playerHealth = Mathf.Clamp(playerHealth, 0, playerMaxHealth);

        // Update health bar
        float barWidth = ((float) playerHealth) / ((float) playerMaxHealth);
        healthBar.UpdateHealth(barWidth);

        if (playerHealth <= 0) {
            GetComponent<PlayerSoundEffects>().StopMovementSounds();
            rb.velocity = new Vector3(0, 0, 0);
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

        // Update health bar
        float barWidth = ((float) playerHealth) / ((float) playerMaxHealth);
        healthBar.UpdateHealth(barWidth);
    }
}
