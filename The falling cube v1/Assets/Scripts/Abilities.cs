using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
    public PlayerMechanics pm;
    public GameManager gm;
    // Slowdown
    public Button slowdownButton;
    // Speedup
    public Button speedUpButton;
    public Slider speedUpSlider;
    float time = 0f;
    float cooldownTime = 0f;

    public void OnSpeedUpButtonClick()
    {
        speedUpButton.interactable = false;
        //Speed up time
        pm.speedingUp = true;
        speedUpSlider.value = 7f;
    }

    public void OnSlowDownButtonClick()
    {

    }

    void Update()
    {
        if (pm.speedingUp)
        {
            time += Time.deltaTime;
            Debug.Log(time);
            if(time >= 3)
            {
                pm.speedingUp = false;
                time = 0f;
            }
        }
    }
}