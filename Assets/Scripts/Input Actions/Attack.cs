using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextGame/InputActions/Attack")]
public class Attack : InputAction
{

    public Enemy enemyToAttack;
    public PlayerStats playerStats;


    public override void RespondToInput(GameController controller, string[] seperatedInputWords)
    {
        playerStats = PlayerStats.instance;
        if (controller.roomNavigation.currentRoom.enemies.Count > 0)
        {
            enemyToAttack = controller.roomNavigation.currentRoom.enemies[0];

            if (!controller.enemies.enemiesKilled.Contains(enemyToAttack))
            {
                if (!enemyToAttack.isNamed)
                    controller.LogStringWithReturn("You attack the " + enemyToAttack.noun + "!");
                else
                    controller.LogStringWithReturn("You attack the " + enemyToAttack.noun + "!");
                enemyToAttack.TakeDamage(playerStats.damage, controller);
            }
            else
            {
                controller.LogStringWithReturn("There are no enemies here to attack.");
            }

        }
        else
        {
            controller.LogStringWithReturn("There are no enemies here to attack.");
        }
    }
}
