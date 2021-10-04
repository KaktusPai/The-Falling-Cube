using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeColliding : MonoBehaviour
{
    public GameManager gm;
    public PlayerMechanics pm;
    public int spikeDamage = 1; 
    void Start()
    {

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        print("collided with " + collision.tag);
        if (CubeFall.falling == true)
        {

            if (collision.gameObject.tag == "Spike")
            {
                Destroy(collision.gameObject);
                pm.spikes -= 1;
                gm.cubeHealth -= spikeDamage;
                print("Collided with spike");
            }

            if (collision.gameObject.tag == "House")
            {
                Destroy(collision.gameObject);
                gm.houses -= 1;

                print("Collided with house");
            }

            if (collision.gameObject.tag == "Player")
            {
                Destroy(collision.gameObject);
                gm.dead = true;
                print("Collided with player");
            }
        }
    }
}