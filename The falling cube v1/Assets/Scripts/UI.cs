using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    // References
    public GameManager gm;
    public PlayerMechanics pm;
    // Level select / pregame UI
    public GameObject LevelSelectUI;
    public GameObject LevelSettingsUI;
    // Game UI
    public Slider healthSlider;
    public Text housesText;
    public Text spikesLeft;
    public Slider spikesBar;
    // Game End UI
    public Button retryButton;
    public GameObject endUI;
    public Text finalText;
    public Text finalTextOT;

    public void Start()
    {
        endUI.gameObject.SetActive(false);
    }
    public void Update()
    {
        // Cube health UI
        healthSlider.value = gm.cubeHealth;
        healthSlider.maxValue = gm.cubeMaxHealth;
        // Houses UI
        housesText.text = "Houses: " + gm.houses.ToString();
        // Spikes UI
        spikesLeft.text = "Spikes: " + pm.spikes.ToString() + "/3";
        spikesBar.value = pm.placingScore;
        spikesBar.maxValue = pm.maxPlaceTime;
        // Update end UI
        if (gm.dead == true)
        {
            LoseUI();
        }
        if (gm.gameLost == true)
        {
            LoseUI();
        }
        if (gm.gameWon == true)
        {
            WinUI();
        }
    }

    public void LoseUI() 
    {
        print("Lose UI active");
        endUI.gameObject.SetActive(true);
        finalText.text = "You lose";
        finalTextOT.text = "You lose";
    }    
    public void WinUI() 
    {
        print("Win UI active");
        endUI.gameObject.SetActive(true);
        finalText.text = "You win!";
        finalTextOT.text = "You win!";
    } 
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
