using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Rigidbody2D m_Rb2d;
    private Animator m_Anim;
    public PlayerController m_Player;
    private float m_ScreenTopLimit;
    private bool m_Destroyed = false;
    private float m_Speed = 50.0f;
    private int m_Damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        m_Rb2d = gameObject.GetComponent<Rigidbody2D>();
        m_Speed = this.transform.parent.transform.localScale.x*m_Speed*(-1);
        m_Rb2d.AddForce(new Vector2(m_Speed,0.0f));
        m_Anim = gameObject.GetComponent<Animator>();
        m_Player = (PlayerController)FindObjectOfType(typeof(PlayerController));
    }

    void FixedUpdate()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "Fruit") {
            m_Anim.SetTrigger("explode");
            m_Rb2d.gravityScale = 1;
            if (col.gameObject.tag == "Player") {
                m_Player.DecrementLife(m_Damage);
            }
        }
    }

    void Hit()
    {
        if(!m_Destroyed && m_Rb2d && m_Anim) {
            m_Rb2d.velocity = new Vector2(0.0f, 0.0f);
            DestroyProjectile(0.5f);
        }
    }

    void DestroyProjectile(float delay=0.0f) {
        if (!m_Destroyed) {
            m_Destroyed = true;
            Destroy(this.gameObject,delay);
        }
    }

}
