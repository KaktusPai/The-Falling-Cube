using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Abilities: MonoBehaviour, IPointerDownHandler //IPointerUpHandler
{
    public PlayerMechanics pm;
    public GameUI gui;
    public int buttonIndex;
    public bool holdingPressOnButton = false;
    bool justPressed = false;
    // Cooldown abilities
    public float abilityDuration;
    public float cooldown;
    public bool recharging = false;

    void Update()
    {
        // Mouse up      
        if (Input.GetButtonUp("Fire1"))
        {
            if (holdingPressOnButton == true)
            {
                print("MOUSE UP");
                holdingPressOnButton = false;
            } 
        }
        ButtonPressAbilities();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        holdingPressOnButton = true;
    }
    void ButtonPressAbilities()
    { 
        // ABILITIES - WHILE PRESSING
        if (holdingPressOnButton == true)
        {
            justPressed = true;
            //Moveslower
            if (buttonIndex == 0)
            {
                pm.MultiplyMoveSpeed(pm.slowDown);
                print("slowing down....");
            }
            // When speed button clicked
            if (buttonIndex == 1)
            {
                if (pm.speedingUp == false)
                {
                    StartCoroutine(SpeedUp());
                }
                print("YEAH FAST WE GO FAST NOW YEAH");
            }
            //Spike ability
            if (buttonIndex == 2)
            {
                pm.PlaceSpike();
            }
            // ABILITIES - ON MOUSEUP
        } else if (holdingPressOnButton == false && justPressed == true)
        {
            // Move slower
            if (buttonIndex == 0)
            {
                if (gui.isFast == 0)
                {

                }
                pm.moveSpeed = pm.maxMoveSpeed;
            }
            // Spike ability
            if (buttonIndex == 2)
            {
                pm.placingScore = 0;
                print("reset spikeplacing " + buttonIndex);
            }
            justPressed = false;
        }
    }
    IEnumerator SpeedUp()
    {
        // In speedup coroutine
        pm.speedingUp = true;
        pm.MultiplyMoveSpeed(pm.speedUp);
        print("start go fast");
        gui.isFast = true;
        //pm.MultiplyMoveSpeed(pm.speedUp);
        yield return new WaitForSeconds(abilityDuration);
        pm.moveSpeed = pm.maxMoveSpeed;
        gui.isFast = false;
        print("speed normal and cooldown");
        yield return new WaitForSeconds(cooldown);
        pm.speedingUp = false;
        print("cooldown ended, you may reuse");
    }
}
