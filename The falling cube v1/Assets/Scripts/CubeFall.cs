using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeFall : MonoBehaviour
{
    // References
    public GameManager gm;
    public PlayerMechanics pm;
    public AnimationCurve curve;
    public GameObject careful;
    public GameObject eye;
    SpriteRenderer spriteRenderer;
    // Falling variables
    float cellSize = 4f;
    public float fallDuration = 4f;
    public float timeBeforeFall = 3f;
    public float minTBF = 2f;
    public float timeDecayAmount = 0.05f;
    public static bool falling;
    public Vector3 direction;
    // Eye looking
    public float maxEyeDistance = 0.6f;
    public float lookSpeed = 0.0025f;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.material.color = Color.white;
        StartCoroutine(FallInRandomDirection());
        pm.spikes = 0;
    }

    private void FixedUpdate()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (gm.cubeHealth <= 0)
        {
            Destroy(this);
        }
        // Turning the eye
        if (falling == false)
        {
            EyeLook(direction, false);
        } else if (falling == true)
        {
            EyeLook(direction, true);
        }
    }

    IEnumerator FallInRandomDirection()
    {
        // BEFORE FALLING WAIT TIME
        falling = false;
        print("THE CUBE RESTS, FOR NOW");
        // CHOOSE RANDOM DIRECTION between up, -up (down), right and -right(left)
        direction = Utility.Choose(transform.up, transform.right, -transform.up, -transform.right);
        //Variables for begin and end positon
        Vector3 startPos;
        Vector3 endPos;
        startPos = transform.position; // startPos is the current position
        endPos = startPos + direction * cellSize; //endPos is the next cell in the new random direction
        if (endPos.x < -6 || endPos.x > 8 || endPos.y < -5 || endPos.y > 4) //If endPos is near the edges -  
        {
            StartCoroutine(FallInRandomDirection()); // - restart the process
             yield break;
        }
        // TURN THE EYE TO NEW POSITION
        careful.SetActive(true);
        careful.transform.position = endPos;

        // AFTER FALLING WAIT TIME
        yield return new WaitForSeconds(timeBeforeFall);

        falling = true;
        print("THE CUBE FALLS");
        careful.SetActive(false);
        float t = 0;

        while (t < 1)
        {
            yield return null;
            t += Time.deltaTime * 1f / fallDuration;
            transform.position = Vector3.Lerp(startPos, endPos, curve.Evaluate(t));
        }
        transform.position = endPos;

        timeBeforeFall -= timeDecayAmount;
        timeBeforeFall = Mathf.Max(timeBeforeFall, minTBF);

        // Repeat the couroutine instead of stopping it
        if (gm.gameLost == false)
        {
            StartCoroutine(FallInRandomDirection());
        }
    }

    void EyeLook(Vector3 lookWhere, bool returning)
    {
        float xDestination = lookWhere.x * maxEyeDistance;
        float yDestination = lookWhere.x * maxEyeDistance;
        if (returning == false && eye.transform.position.x <= xDestination || eye.transform.position.y <= yDestination) // While not in end position
        {
             eye.transform.position += lookWhere * Time.deltaTime;
        } 
        if (returning == true && eye.transform.position != Vector3.zero) // Return to 0,0
        {
             eye.transform.position -= lookWhere * Time.deltaTime;
        } 
    }
}
