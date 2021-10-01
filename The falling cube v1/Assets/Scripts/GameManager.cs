using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text finalText;
    public Text finalTextOT;
    public GameObject endUI;
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
        endUI.gameObject.SetActive(false);
        cubeHealth = cubeMaxHealth;
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
        endUI.gameObject.SetActive(true);
        finalText.text = "You lose";
        finalTextOT.text = "You lose";
    }

    void Win()
    {
        endUI.gameObject.SetActive(true);
        finalText.text = "You win!";
        finalTextOT.text = "You win!";
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }
}
