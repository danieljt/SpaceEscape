using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the UI flow of the game
/// </summary>
public class UIManager : MonoBehaviour
{
    [Tooltip("The game manager this script communicates with")]
    public GameManager gameManager;

    [Tooltip("The level manager this script communicates with")]
    public LevelManager levelManager;
}
