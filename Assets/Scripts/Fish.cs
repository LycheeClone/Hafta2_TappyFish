using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private Rigidbody2D _rb;
    
    [SerializeField]
    private float _speed;

    private int angle;

    private int maxAngle = 20;

    private int minAngle = -60;
    private bool touchedGround;
    public GameManager gameManager;
    public Sprite fishDied;
    private SpriteRenderer sp;
    private Animator anim;

    public Score score;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        //_rb.gravityScale = 0;
        //_rb.velocity = new Vector2(_rb.velocity.x,9f);
    }

    // Update is called once per frame
    void Update()
    {
        FishSwim();
        //FishRotation();
    }

    private void FixedUpdate()
    {
        FishRotation();
    }

    void FishSwim()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.gameOver == false)
        {
            _rb.velocity = Vector2.zero;
            _rb.velocity = new Vector2(_rb.velocity.x,_speed);

        }
    }

    void FishRotation()
    {
        if (_rb.velocity.y > 0)
        {
            if (angle <= maxAngle)
            {
                angle = angle + 4;
            }
        }
        else if (_rb.velocity.y < -1.2)
        {
            if (angle > minAngle)
            {
                angle = angle - 2;
            }
        }

        if (touchedGround == false)
        {
            transform.rotation = Quaternion.Euler(0,0,angle);

        }
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Obstacle"))
        {
            //Debug.Log("Scored!");
            score.Scored();
        }else if (col.CompareTag("Column"))
        {
            //Game Over
            gameManager.GameOver();
            //GameOver();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            if (GameManager.gameOver == false)
            {
                //Game Over
                gameManager.GameOver();
                GameOver();
            }
            else
            {
                //Game Over(fish)
                GameOver();
            }
        }
      
    }

    void GameOver()
    {
        touchedGround = true;
        transform.rotation = Quaternion.Euler(0,0,-90);
        sp.sprite = fishDied;
        anim.enabled = false;
    }
}
