using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeLooking : MonoBehaviour
{
    public Animation moveEyeFromCenter;
    public CubeFall cf;

    void Update()
    {
        //If 
        if (cf.falling == false)
        {
            moveEyeFromCenter.Play();
        } else if (cf.falling == true)
        {
            moveEyeFromCenter.Stop();
        }
    }
    public void EyeInPlace()
    {
        
    }

    public void MoveEyeBack()
    {
        
    }
}
