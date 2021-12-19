using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour
{

    GameController controller;

    public InputField inputField;

    private void Awake()
    {
        controller = GetComponent<GameController>();
        inputField.onEndEdit.AddListener(AcceptStringInput);
    }

    void AcceptStringInput(string userInput)
    {
        if (controller.stateController.gameState != GameStateController.GameState.DEAD)
        {
            userInput = userInput.ToLower();
            controller.LogStringWithReturn(userInput);

            char[] delimiterCharacters = { ' ' };
            string[] seperatedInputWords = userInput.Split(delimiterCharacters);

            for (int i = 0; i < controller.inputActions.Length; i++)
            {
                InputAction inputAction = controller.inputActions[i];
                if (inputAction.keyWord == seperatedInputWords[0])
                {
                    inputAction.RespondToInput(controller, seperatedInputWords);
                }
                //else
                //{
                //    controller.LogStringWithReturn("Not recognised");
                //    break;
                //}
            }

            InputComplete();
        }
    }

    void InputComplete()
    {
        controller.DisplayLoggedText();
        inputField.ActivateInputField();
        inputField.text = null;
    }
}
