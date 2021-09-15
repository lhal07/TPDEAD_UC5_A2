using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantController : MonoBehaviour
{
    public GameObject m_Seed;
    private Animator m_Anim;
    public PlayerController m_Player;
    private float m_ShootInterval = 2.0f;
    public float m_Timer = 0.0f;
    private int m_Damage = 1;
    public bool m_PlayerVisible = false;
    public LayerMask m_PlayerMask;
    private Vector2 m_Direction;

    // Start is called before the first frame update
    void Start()
    {
        m_Anim = gameObject.GetComponent<Animator>();
        m_Player = (PlayerController)FindObjectOfType(typeof(PlayerController));
        m_Direction = new Vector2(-1*transform.localScale.x,0);
    }

    // Update is called once per frame
    void Update()
    {
        m_Timer += Time.deltaTime;
        m_PlayerVisible = false;
        float distance = 3f;
        RaycastHit2D sight = Physics2D.Raycast(transform.position, m_Direction, distance, m_PlayerMask);
        if (sight.collider != null) {
            m_PlayerVisible = sight.collider.CompareTag("Player");
        }

        if (m_PlayerVisible) {
            Attack();
        }
    }

    void Attack() {
        if (m_Timer > m_ShootInterval) {
            m_Timer = 0.0f;
            m_Anim.SetTrigger("onAttack");
        }
    }

    void Spawn()
    {
        Instantiate(m_Seed, this.transform.GetChild(0).position, Quaternion.identity, this.transform);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player") {
            m_Player.DecrementLife(m_Damage);
        }
    }

}
