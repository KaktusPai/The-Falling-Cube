using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text finalText;
    public Text housesText;
    public Slider healthSlider;
    public Button retryButton;
    public static int houses = 8;
    public int cubeMaxHealth = 9;
    public static int cubeHealth;
    public static bool dead = false;

    public GameObject level;

    void Start()
    {
        dead = false;
        finalText.gameObject.SetActive(false);
        cubeHealth = cubeMaxHealth;
        retryButton.gameObject.SetActive(false);
        houses = 8;
        cubeHealth = cubeMaxHealth;
        PlayerMechanics.spikes = 2;
    }

    void Update()
    {
        healthSlider.value = cubeHealth;
        healthSlider.maxValue = cubeMaxHealth;
        housesText.text = "Houses: " + houses.ToString();
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
        finalText.gameObject.SetActive(true);
        finalText.text = "You lose";
        retryButton.gameObject.SetActive(true);
    }

    void Win()
    {
        finalText.gameObject.SetActive(true);
        finalText.text = "You win!";
        retryButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }
}
