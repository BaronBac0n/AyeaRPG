using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItems : MonoBehaviour
{

    public List<InteractableObject> usableItemList;

    public List<InteractableObject> interactableObjects;
    public Dictionary<string, InteractableObject> interactableObjectsDictionary = new Dictionary<string, InteractableObject>();

    public Dictionary<string, string> examineDictionary = new Dictionary<string, string>();
    public Dictionary<string, string> takeDictionary = new Dictionary<string, string>();

    Dictionary<string, ActionResponse> useDictionary = new Dictionary<string, ActionResponse>();

    GameController controller;

    [HideInInspector]
    public List<string> nounsInRoom = new List<string>();

    PlayerStats playerStats;


    private void Awake()
    {
        playerStats = PlayerStats.instance;
        controller = GetComponent<GameController>();
        SetUpInteractableDictionary();
    }

    public void SetUpInteractableDictionary()
    {
        for (int i = 0; i < interactableObjects.Count; i++)
        {
            interactableObjectsDictionary.Add(interactableObjects[i].noun, interactableObjects[i]);
        }
    }

    public string GetObjectsNotInInventory(Room currentRoom, int i)
    {
        InteractableObject interactableInRoom = currentRoom.interactableObjectsInRoom[i];

        if (!playerStats.nounsInInventory.Contains(interactableInRoom.noun))
        {
            nounsInRoom.Add(interactableInRoom.noun);
            return interactableInRoom.description;
        }
        else
        {
            return null;
        }
    }

    public void ClearCollections()
    {
        examineDictionary.Clear();
        takeDictionary.Clear();
        nounsInRoom.Clear();
    }

    public Dictionary<string, string> Take(string[] seperatedInputWords)
    {
        string noun = seperatedInputWords[1];

        if (nounsInRoom.Contains(noun))
        {
            if (interactableObjectsDictionary[noun].canPickup)
            {
                playerStats.inventory.Add(interactableObjectsDictionary[noun]);
                playerStats.nounsInInventory.Add(noun);
                AddActionResponsesToUseDictionary();
                nounsInRoom.Remove(noun);
            }
            else
            {
                controller.LogStringWithReturn("You cannot take " + noun);
                return null;
            }
            return takeDictionary;
        }
        else
        {
            controller.LogStringWithReturn("There is no " + noun + " here to take.");
            return null;
        }
    }

    public void DisplayInventory()
    {
        controller.LogStringWithReturn("You look in your backpack, inside you have: ");
        for (int i = 0; i < playerStats.nounsInInventory.Count; i++)
        {
            controller.LogStringWithReturn(playerStats.nounsInInventory[i]);
        }
    }

    public void DisplayInventoryAfterGame()
    {
        controller.LogStringWithReturn("You even managed to collect: ");
        for (int i = 0; i < playerStats.nounsInInventory.Count; i++)
        {
            controller.LogStringWithReturn(playerStats.nounsInInventory[i]);
        }
    }

    public void AddActionResponsesToUseDictionary()
    {
        for (int i = 0; i < playerStats.nounsInInventory.Count; i++)
        {
            string noun = playerStats.nounsInInventory[i];

            InteractableObject interactableObjectInInventory = GetInteractableObjectFromUsableList(noun);
            if (interactableObjectInInventory == null)
            {
                continue;
            }
            else
            {
                for (int j = 0; j < interactableObjectInInventory.interactions.Length; j++)
                {
                    Interaction interaction = interactableObjectInInventory.interactions[j];

                    if (interaction.actionResponse == null)
                    {
                        continue;
                    }

                    if (!useDictionary.ContainsKey(noun))
                    {
                        useDictionary.Add(noun, interaction.actionResponse);
                    }
                }
            }
        }
    }

    InteractableObject GetInteractableObjectFromUsableList(string noun)
    {
        for (int i = 0; i < usableItemList.Count; i++)
        {
            if (usableItemList[i].noun == noun)
            {
                return usableItemList[i];
            }
        }
        return null;
    }

    public void UseItem(string[] seperatedInputWords)
    {
        string nounToUse = seperatedInputWords[1];

        if (playerStats.nounsInInventory.Contains(nounToUse))
        {
            if (useDictionary.ContainsKey(nounToUse))
            {
                bool actionResult = useDictionary[nounToUse].DoActionResponse(controller);
                if (!actionResult)
                {
                    controller.LogStringWithReturn("Hmm. Nothing happens.");
                }
            }
            else
            {
                controller.LogStringWithReturn("You can't use the " + nounToUse);
            }
        }
        else
        {
            controller.LogStringWithReturn("There is no " + nounToUse + " in your backpack to use.");
        }
    }
}
