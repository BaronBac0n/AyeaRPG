using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public enum GameState { COMBAT, EXPLORING, DEAD };
    public GameState gameState;
}
