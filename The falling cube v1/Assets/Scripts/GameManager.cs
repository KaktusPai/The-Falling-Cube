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
    //Game vars
    public int houses = 8;
    public int cubeMaxHealth = 9;
    public int cubeHealth;
    // "State" bools
    public bool dead = false;

    public bool gameWon = false;
    public bool gameLost = false;

    void Start()
    {
        // Set all to default values
        dead = false;
        gameWon = false;
        cubeHealth = cubeMaxHealth;
        houses = 8;
        cubeHealth = cubeMaxHealth;
    }

    void Update()
    {
        if (houses <= 0 || dead == true)
        {
            Lose();
        } 

        if (cubeHealth <= 0)
        {
            Win();
        }
    }

    void Lose()
    {
        gameLost = true;
    }

    void Win()
    {
        gameWon = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }
}
