using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeColliding : MonoBehaviour
{
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

                print("Collided with spike");
            }

            if (collision.gameObject.tag == "House")
            {
                Destroy(collision.gameObject);
                GameManager.houses -= 1;

                print("Collided with house");
            }

            if (collision.gameObject.tag == "Player")
            {
                Destroy(collision.gameObject);
                GameManager.dead = true;
                print("Collided with player");
            }
        }
    }
}