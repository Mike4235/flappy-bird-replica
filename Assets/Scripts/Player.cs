using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Player : MonoBehaviour
{

    SpriteRenderer sr;
    [SerializeField] Sprite[] BirdArray;
    Rigidbody2D body;
    [SerializeField] Text PointCount;
    int CurrentScore = 0;

    private void Awake()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        body = gameObject.GetComponent<Rigidbody2D>();
        PointCount = GameObject.FindWithTag("PointCounter").GetComponent<Text>();
        Debug.Log("Game Restarted");
    }

    // Start is called before the first frame update
    void Start()
    {
        // body.velocity = new Vector2(3f, 0f);
    }

    // Update is called once per frame
    float RotationAngle = 0.2f, cur = 0f;
    void Update()
    {
        playerJump();
        {
            float VeloY = body.velocity.y;
            if (VeloY >= -1e-6 && VeloY <= 1e-6) changeSprite(2);
            else if (VeloY >= 0)
            {
                cur += RotationAngle * VeloY; cur = Math.Min(cur, 45.0f);
                changeSprite(0);
                transform.rotation = Quaternion.Euler(0, 0, cur);
            }
            else
            {
                cur += RotationAngle * VeloY; cur = Math.Max(cur, -45.0f);
                changeSprite(1);
                transform.rotation = Quaternion.Euler(0, 0, cur);
            }
        }
    }

    public void changeSprite(int x)
    {
        sr.sprite = BirdArray[x];
    }

    float jumpForce = 5f;
    void playerJump()
    {
        if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
        {
            body.velocity = new Vector2(body.velocity.x, jumpForce);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Point"))
        {
            Debug.Log("Encountered a Point Object");
            CurrentScore++;
            PointCount.text = "Current Score: " + CurrentScore.ToString();
        }
        else if (collision.CompareTag("Pipe") || collision.CompareTag("Ground"))
        {
            PlayerPrefs.SetInt("MaxScore", Math.Max(PlayerPrefs.GetInt("MaxScore"), CurrentScore));
            Destroy(gameObject);
        }
    }
}
