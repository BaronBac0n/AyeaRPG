using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextGame/InputActions/Help")]
public class Help : InputAction
{
    public override void RespondToInput(GameController controller, string[] seperatedInputWords)
    {
        controller.LogStringWithReturn(
            "Use 'go <direction>' to move that way. EX: 'go north'. \n" +
            "Use 'examine <item>' to examine an item in a room. EX: 'examine skull'. \n" +
            "Use 'take <item>' to take an item from a room. EX: 'take skull'. \n" +
            "Use 'use <item>' to use an item. EX: 'use skull'. \n" +
            "Use 'inventory' to see what is in your inventory. \n" +
            "Use 'attack' to attack an enemy in the room with you. \n" +
            "Use 'examine room' to see the room's description again.");
    }
}
