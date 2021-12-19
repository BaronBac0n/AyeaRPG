using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextGame/Enemy/Enemy")]
public class Enemy : ScriptableObject
{
    public string noun = "name";
    [TextArea]
    public string description = "Description in room";

    public Interaction[] interactions;

    public int maxHealth;
    public int currentHealth;

    public int damageToPlayer = 3;

    public bool isAboveHalfHealth = true;
    public bool isNamed = false;

    public bool changesRoomOnDeath;
    public Room roomToChangeTo;

    public void TakeDamage(int amount, GameController controller)
    {
        currentHealth -= amount;
        if (!isNamed)
            controller.LogStringWithReturn("The " + noun + " takes " + amount + " damage!");
        else
            controller.LogStringWithReturn(noun + " takes " + amount + " damage!");

        if (currentHealth <= (maxHealth / 2) && isAboveHalfHealth && currentHealth > 0)
        {
            isAboveHalfHealth = false;
            if (!isNamed)
                controller.LogStringWithReturn("The " + noun + " is looking rough!");
            else
                controller.LogStringWithReturn(noun + " is looking rough!");
        }

        if (DeathCheck())
        {
            if (!isNamed)
                controller.LogStringWithReturn("The " + noun + " falls to the ground, dead.");
            else
                controller.LogStringWithReturn(noun + " falls to the ground, dead.");

            if (changesRoomOnDeath)
            {
                controller.roomNavigation.currentRoom = roomToChangeTo;
                controller.DisplayRoomText();
            }
            controller.enemies.enemiesKilled.Add(this);
            controller.stateController.gameState = GameStateController.GameState.EXPLORING;
        }
        else
        {
            controller.playerStats.TakeDamage(damageToPlayer, controller, this);
        }
    }

    public bool DeathCheck()
    {
        if (currentHealth <= 0)
        {
            return true;
        }
        else return false;
    }
}
