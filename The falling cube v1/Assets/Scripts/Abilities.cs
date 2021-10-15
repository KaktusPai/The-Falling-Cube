using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Abilities: MonoBehaviour, IPointerDownHandler //IPointerUpHandler
{
    public PlayerMechanics pm;
    public int buttonIndex;
    public bool holdingPressOnButton = false;
    bool justPressed = false;
    // Cooldown abilities
    public float cooldown;
    public float maxCooldown;
    public bool recharging = false;

    void Start()
    {
        cooldown = maxCooldown;
    }
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

        // ABILITIES - COOLDOWN
        // Speeding up
        if (pm.speedingUp == true)
        {
            if (cooldown > 0 && recharging == false)
            {
                pm.MultiplyMoveSpeed(pm.speedUp);
                cooldown -= Time.deltaTime;
                print("Beginning speedup duration");
            }
            else if (cooldown < 0 && recharging == false)
            {
                recharging = true;
                print("Start recharging");
            }
            if (recharging == true && cooldown < maxCooldown)
            {
                cooldown += 0.5f * Time.deltaTime;
                pm.moveSpeed = pm.maxMoveSpeed;
                print("Recharge at half speed");
            }
            else if (recharging == true && cooldown >= maxCooldown)
            {
                recharging = false;
                cooldown = maxCooldown;
                pm.speedingUp = false;
                pm.moveSpeed = pm.maxMoveSpeed;
                print("Cooldown done");
            }
        }
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
                pm.MultiplyMoveSpeed(pm.speedUp);
                pm.speedingUp = true;
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
            if (buttonIndex == 0)
            {
                pm.moveSpeed = pm.maxMoveSpeed;
            }
            if (buttonIndex == 2)
            {
                pm.placingScore = 0;
                print("reset spikeplacing " + buttonIndex);
            }
            justPressed = false;
        }
    }
}
