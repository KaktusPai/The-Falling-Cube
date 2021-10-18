using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMechanics : MonoBehaviour
{
    // References
    public Animator animator;
    public SpriteRenderer sr;
    public GameManager gm;
    // Player movement vars
    public float moveSpeed = 5f;
    public float maxMoveSpeed = 12f;
    public Rigidbody2D rb;
    public Transform player;
    Vector2 movement;
    public bool playerIsFlipped;
    // Slowdown and speedup
    public float slowDown = 0.75f;
    public float speedUp = 1.25f;
    public bool speedingUp = false;
    // Spike mechanic
    public GameObject spike;
    public int spikes = 0;
    public int maxSpikes = 3;
    public float placingScore = 0;
    public float maxPlaceTime;


    void Start()
    {
        // Set default values
        spikes = 0;
        placingScore = 0;
        moveSpeed = maxMoveSpeed;
    }
    void Update()
    {
        if (gm.gameStart == true)
        {
            if (placingScore == 0)
            {
                // Movement inputs
                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");
            }
            else
            {
                // Can't move when placing
                movement = Vector3.zero;
                print("Placing 0 cant move");
            }
        }
        // Set animator variables
        animator.SetFloat("Speed", movement.sqrMagnitude);
        //print(movement.x + " , " + movement.y);
        // When moving left, flip - When moving right, unflip
        Quaternion zero = new Quaternion(0, 0, 0, 0);
        Quaternion flippedY = new Quaternion(0, 180, 0, 0);
        if (movement.x == -1) 
        {
            transform.rotation = flippedY;
            playerIsFlipped = true;
        } else if (movement.x == 1) 
        {
            transform.rotation = zero;
            playerIsFlipped = false;
        }
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void MultiplyMoveSpeed(float amount)
    {
        moveSpeed = maxMoveSpeed * amount;
    }
    public void PlaceSpike()
    {
        if (spikes < maxSpikes)
        {
            placingScore += 1 * Time.deltaTime;
            if (placingScore >= maxPlaceTime)
            {
                Vector3 spawnOffset = new Vector3(0.5f, 0, 0);
                if (playerIsFlipped == true) // Place on the left side of the player
                {
                    Instantiate(spike, this.transform.position + -spawnOffset, this.transform.rotation);
                } 
                if (playerIsFlipped == false) // Place on the right side
                {
                    print("PLACING RIGHT");
                    Instantiate(spike, this.transform.position + spawnOffset, this.transform.rotation);
                }
                spikes += 1;
                placingScore = 0;
            }
        }
    }
}
