using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //References 
    public GameObject level;
    public CubeFall cf;
    public GameUI gui;
    //Game vars
    public int houses = 8;
    public int maxHouses = 8;
    public int cubeMaxHealth = 9;
    public int cubeHealth;
    // "State" bools
    public bool gameStart = false;
    public static bool playAgain;
    public bool dead = false;
    public bool gameWon = false;
    public bool gameLost = false;

    void Start()
    {
        // Set all to default values
        dead = false;
        gameWon = false;
        cubeHealth = cubeMaxHealth;
        houses = maxHouses;
        cubeHealth = cubeMaxHealth;
        if (playAgain == true)
        {
            gameStart = true;
        }
    }

    void Update()
    {
        if (gameStart == true)
        {
            if (houses <= 0 || dead == true)
            {
                Lose();
                gameLost = true;
            }

            if (cubeHealth <= 0)
            {
                Win();
                gameWon = true;
            }
        }
    }

    void Lose()
    {
        
    }

    void Win()
    {
        
    }
}
