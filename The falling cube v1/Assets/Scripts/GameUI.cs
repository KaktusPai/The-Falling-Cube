using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    // References
    public GameManager gm;
    public PlayerMechanics pm;
    public CubeFall cf;
    public Abilities a;
    // Level select / pregame UI
    public GameObject preGameUI;
    public Animator pregameUIAnimator;
    // Game UI
    public Slider healthSlider;
    public Text housesText;
    public Text spikesLeft;
    public Slider spikesBar;
    // Game cooldown abilites UI
    public Slider speedBar;
    public bool isFast = false;

    // Game End UI
    public Button retryButton;
    public GameObject endUI;
    public Text finalText;
    public Text finalTextOT;

    public void Start()
    {
        speedBar.maxValue = a.cooldown;
        speedBar.value = 0;
        endUI.gameObject.SetActive(false);
        if (GameManager.playAgain == false)
        {
            PreGameUI();
        }
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
    void FixedUpdate()
    {
        // Speeding up
        if (pm.speedingUp == false && isFast == false)
        {
            speedBar.value = 0;
        }
        else if (isFast == true)
        {
            speedBar.value = a.cooldown;
        }
        else if (pm.speedingUp == true && isFast == false)
        {
            speedBar.value -= 1 * Time.deltaTime;
        }
    }
    public void PreGameUI()
    {
        print("Pregame UI active");
        endUI.gameObject.SetActive(false);
        preGameUI.gameObject.SetActive(true);
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
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadDifficultySelect()
    {
        GameManager.playAgain = false;
        SceneManager.LoadScene(1);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void PlayAgain()
    {
        GameManager.playAgain = true;
        LoadGame();
    }
    public void StartGame()
    {
        gm.gameStart = true;
        preGameUI.SetActive(false);
        cf.StartFalling();
    }
    // Pregame UI up down
    void pauseAnimationEvent()
    {
        pregameUIAnimator.enabled = false;
    }
}
