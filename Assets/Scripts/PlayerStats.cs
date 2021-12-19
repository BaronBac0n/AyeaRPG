using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    #region Singleton
    public static PlayerStats instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of PlayerStats found");
            return;
        }
        instance = this;
    }
    #endregion

    public int currentHealth, maxHealth;

    public int damage = 3;

    public List<InteractableObject> inventory;
    [HideInInspector]
    public List<string> nounsInInventory = new List<string>();

    GameController gController;

    private void Start()
    {
        gController = GameController.instance;
    }

    public void TakeDamage(int amount, GameController controller, Enemy attackingEnemy)
    {
        gController = controller;
        currentHealth -= amount;
        if (!attackingEnemy.isNamed)
            controller.LogStringWithReturn("The " + attackingEnemy.noun + " attacks you for " + amount + " damage!");
        else
            controller.LogStringWithReturn(attackingEnemy.noun + " attacks you for " + amount + " damage!");

        UpdateHealthDisplay();

        if (DeathCheck())
        {
            controller.LogStringWithReturn("YOU DIED");
            controller.stateController.gameState = GameStateController.GameState.DEAD;
            controller.buttonsPanel.SetActive(true);
        }
    }

    public bool DeathCheck()
    {
        return (currentHealth <= 0) ? true : false;
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthDisplay();
    }

    public void UpdateHealthDisplay()
    {
        if (currentHealth <= 0) currentHealth = 0;
        gController.playerHealthDisplay.text = "Health: " + currentHealth + "/" + maxHealth;
    }

    public void DisplayInventory()
    {
        gController.LogStringWithReturn("You look in your backpack, inside you have: ");
        for (int i = 0; i < nounsInInventory.Count; i++)
        {
            gController.LogStringWithReturn(nounsInInventory[i]);
        }
    }
}
