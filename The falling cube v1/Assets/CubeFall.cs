using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeFall : MonoBehaviour
{
    public AnimationCurve curve;
    public GameObject careful;
    float cellSize = 4f;
    public float duration = 4f;
    public float delay = 3f;
    public float minDelay = 2f;
    public float delayDecay = 0.05f;
    public bool falling;
    SpriteRenderer sr;
    public ParticleSystem ps;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.material.color = Color.white;
        StartCoroutine(FallInRandomDirection());
        ps.gameObject.SetActive(false);
        //careful.SetActive(false);
        PlayerMechanics.spikes = 0;
    }

    private void Update()
    {
        sr = GetComponent<SpriteRenderer>();
        if (GameManager.cubeHealth <= 0)
        {
            ps.gameObject.SetActive(true);
            Destroy(this);
            StartCoroutine(StopEmission());
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var main = ps.main;
        if (falling == true)
        {
            if (collision.gameObject.tag == "Spike")
            {
                Destroy(collision.gameObject);
                GameManager.cubeHealth -= 1;
                PlayerMechanics.spikes -= 1;

                main.startColor = Color.red;
                ps.gameObject.SetActive(true);
                StartCoroutine(StopEmission());
            }

            if (collision.gameObject.tag == "House")
            {
                Destroy(collision.gameObject);
                GameManager.houses -= 1;

                main.startColor = Color.yellow;
                ps.gameObject.SetActive(true);
                StartCoroutine(StopEmission());
            }

            if (collision.gameObject.tag == "Player")
            {
                Destroy(collision.gameObject);
                GameManager.dead = true;

                main.startColor = Color.magenta;
                ps.gameObject.SetActive(true);
                StartCoroutine(StopEmission());
            }
        }
    }

    IEnumerator StopEmission()
    {
        yield return new WaitForSeconds(0.9f);
        ps.gameObject.SetActive(false);
    }

    IEnumerator FallInRandomDirection()
    {
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

        yield return new WaitForSeconds(delay);

        falling = true;

        careful.SetActive(false);

        float t = 0;
        while (t < 1)
        {
            yield return null;
            t += Time.deltaTime * 1f / duration;
            transform.position = Vector3.Lerp(startPos, endPos, curve.Evaluate(t));
        }
        transform.position = endPos;

        delay -= delayDecay;
        delay = Mathf.Max(delay, minDelay);

        if (GameManager.cubeHealth <= 6)
        {
            duration -= 0.25f;
            duration = Mathf.Max(duration, 1f);
            minDelay = 1;
            delayDecay = 0.25f;
            sr.material.color = Color.red;
        }
        StartCoroutine(FallInRandomDirection());
    }
}
