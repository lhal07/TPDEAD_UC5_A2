using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D m_Rb2d;
    private Animator m_Anim;
    private SpriteRenderer m_Sprite;
    private CanvasScript m_Canvas;
    private HealthBarScript m_HealthBar;
    private Vector3 m_RecPos;
    public int m_Life = 10;
    private int m_Score = 0;
    private bool m_toTheLeft;
    private bool m_toTheRight;
    private bool m_facingRight = true;
    private bool m_idle;
    public bool m_isGrounded = false;
    public LayerMask m_groundLayer;
    public float m_Speed = 0.5f;
    public float m_JumpForce = 5f;
    private bool m_Victory = false;

    // Start is called before the first frame update
    void Start()
    {
        m_Anim = GetComponent<Animator>();
        m_Rb2d = GetComponent<Rigidbody2D>();
        m_Sprite = GetComponent<SpriteRenderer>();
        m_RecPos = transform.position;
        m_Canvas = (CanvasScript)FindObjectOfType(typeof(CanvasScript));
        m_HealthBar = (HealthBarScript)FindObjectOfType(typeof(HealthBarScript));
    }

    void FixedUpdate()
    {
        Move();
    }

    float UpdateDirection()
    {
        float direction = Input.GetAxis("Horizontal");
        if (m_Victory) {
            direction = 0;
        }
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
        if (Input.GetKeyDown(KeyCode.Space) && m_isGrounded && !m_Victory) {
            Jump();
        }
        if (OutOfBounds()) {
            m_HealthBar.SetHealth(0);
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
        m_HealthBar.SetHealth(m_Life);
    }

    public void IncrementScore(int points)
    {
        m_Score += points;
        m_Canvas.SetScore(m_Score);
    }

    public int GetScore() {
        print("GetScore" + m_Score.ToString());
        return m_Score;
    }

    bool OutOfBounds()
    {
        float screenBottomOffset = -1;
        float screenBottomLimit = Camera.main.ScreenToWorldPoint(new Vector2(0,screenBottomOffset)).y;
        return (transform.position.y <= screenBottomLimit);
    }

    void TestDefeatCondition()
    {
        bool gameOver = (m_Life <= 0) || OutOfBounds();

        if (gameOver) {
            SceneManager.LoadScene("Scenes/Fase01");
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Goal")
        {
            m_Victory = true;
            m_Canvas.SetVictory();
        }
    }

}
