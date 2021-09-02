using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D m_Rb2d;
    private Animator m_Anim;
    private SpriteRenderer m_Sprite;
    private Vector3 m_RecPos;
    private int m_Life = 10;
    private int m_Score = 0;
    private bool m_toTheLeft;
    private bool m_toTheRight;
    private bool m_facingRight = true;
    private bool m_idle;
    public bool m_isGrounded = false;
    public LayerMask m_groundLayer;
    public float m_Speed = 0.5f;
    public float m_JumpForce = 5f;

    // Start is called before the first frame update
    void Start()
    {
        m_Anim = GetComponent<Animator>();
        m_Rb2d = GetComponent<Rigidbody2D>();
        m_Sprite = GetComponent<SpriteRenderer>();
        m_RecPos = transform.position;
    }

    void FixedUpdate()
    {
        Move();
    }

    float UpdateDirection()
    {
        float direction = Input.GetAxis("Horizontal");
        m_toTheLeft = direction < 0;
        m_toTheRight = direction > 0;
        m_facingRight = transform.localScale.x > 0;
        m_idle = direction == 0;
        return direction;
    }

    void Move()
    {
        float direction = UpdateDirection();
        if (m_idle) {
            m_Anim.SetTrigger("idle");
        }
        else {
            m_Rb2d.velocity = new Vector2(direction * m_Speed, m_Rb2d.velocity.y);
            if (m_toTheLeft  && m_facingRight || m_toTheRight && !m_facingRight) {
                Flip();
            }
        }
    }

    void Flip()
    {
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }

    // Update is called once per frame
    void Update()
    {
        m_isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.2f, m_groundLayer);
        m_Anim.SetFloat("vertical",m_Rb2d.velocity.y);
        m_Anim.SetFloat("horizontal",Mathf.Abs(m_Rb2d.velocity.x));
        if (Input.GetKeyDown(KeyCode.Space) && m_isGrounded) {
            Jump();
        }
        TestDefeatCondition();
    }

    void Jump()
    {
        m_Rb2d.velocity = new Vector2(m_Rb2d.velocity.x,0);
        m_Rb2d.AddForce(Vector2.up*m_JumpForce, ForceMode2D.Impulse);
        m_Anim.SetTrigger("jump");
    }

    public void DecrementLife(int damage)
    {
        m_Anim.SetTrigger("hit");
        m_Life -= damage;
    }

    public void IncrementScore(int points)
    {
        m_Score += points;
    }

    bool OutOfBounds()
    {
        float screenBottomLimit = Camera.main.ScreenToWorldPoint(new Vector2(0,0)).y;
        return (transform.position.y <= screenBottomLimit);
    }

    void TestDefeatCondition()
    {
        bool gameOver = (m_Life <= 0) || OutOfBounds();

        if (gameOver) {
            print("Game Over");
        }
    }
}
