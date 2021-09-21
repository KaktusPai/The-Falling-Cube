using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMechanics : MonoBehaviour
{

    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Transform player;

    Vector2 movement;

    public GameObject spike;
    public Text spikesLeft;
    public Slider spikesBar;

    public static int spikes = 0;
    public int maxSpikes = 3;
    public float placingScore = 0;
    public float maxPlaceTime;

    void Start()
    {
        spikes = 0;
        placingScore = 0;
    }
    void Update()
    {
        spikesLeft.text = "Spikes: " + spikes.ToString() + "/3";
        spikesBar.value = placingScore;
        spikesBar.maxValue = maxPlaceTime;

        if (placingScore == 0)
        {
            // Movement inputs
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        } else
        {
            // Can't move when placing
            movement = Vector3.zero;
            print("Placing 0 cant move");
        }

        if (Input.GetButton("Fire1") && spikes < maxSpikes)
        {
            placingScore += 1 * Time.deltaTime;
            if (placingScore >= maxPlaceTime)
            {
                Instantiate(spike, this.transform.position, this.transform.rotation);
                spikes += 1;
                placingScore = 0;
            }
        }
        else
        {
            placingScore = 0;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
