using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeFall : MonoBehaviour
{
    // References
    public AnimationCurve curve;
    public GameObject careful;
    SpriteRenderer spriteRenderer;
    // Falling variables
    float cellSize = 4f;
    public float fallDuration = 4f;
    public float timeBeforeFall = 3f;
    public float minTBF = 2f;
    public float timeDecayAmount = 0.05f;
    public static bool falling;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.material.color = Color.white;
        StartCoroutine(FallInRandomDirection());
        PlayerMechanics.spikes = 0;
    }

    private void Update()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (GameManager.cubeHealth <= 0)
        {
            Destroy(this);
        }
    }

    IEnumerator FallInRandomDirection()
    {
        // BEFPRE FALLING WAIT TIME
        falling = false;
        Vector3 direction = Utility.Choose(transform.up, transform.right, -transform.up, -transform.right);
        Vector3 startPos;
        Vector3 endPos;
        startPos = transform.position;
        endPos = startPos + direction * cellSize;

        if (endPos.x < -6 || endPos.x > 8 || endPos.y < -5 || endPos.y > 4)
        {
            StartCoroutine(FallInRandomDirection());
            yield break;
        }

        careful.SetActive(true);
        careful.transform.position = endPos;

        // AFTER FALLING WAIT TIME
        yield return new WaitForSeconds(timeBeforeFall); 

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
        StartCoroutine(FallInRandomDirection());
    }
}
