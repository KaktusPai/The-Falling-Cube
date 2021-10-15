using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMechanics : MonoBehaviour
{
    public GameManager gm;
    // Player movement vars
    public float moveSpeed = 5f;
    public float maxMoveSpeed = 12f;
    public Rigidbody2D rb;
    public Transform player;
    Vector2 movement;
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
                Instantiate(spike, this.transform.position, this.transform.rotation);
                spikes += 1;
                placingScore = 0;
            }
        }
    }
}
