using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{

    public float m_Timer;
    public float m_DamageTimer;
    public float m_ActivationTime = 5f;
    public float m_ActiveTime = 2f;
    public float m_DamageTime = 0.5f;
    public bool m_ActiveFire;
    private int m_Damage = 1;
    private Animator m_Anim;
    private PlayerController m_Player;

    // Start is called before the first frame update
    void Start()
    {
        m_Anim = gameObject.GetComponent<Animator>();
        m_Player = (PlayerController)FindObjectOfType(typeof(PlayerController));
        m_ActiveFire = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimation();
        UpdateDamage();
    }

    void UpdateAnimation()
    {
        bool isFireActive = m_Anim.GetBool("onFire");
        bool resetTimer = !((m_Timer < m_ActiveTime) ||
                (!isFireActive && (m_Timer < m_ActivationTime)));

        m_ActiveFire = isFireActive && (m_Timer < m_ActiveTime) ||
            !isFireActive && (m_Timer >= m_ActivationTime);


        m_Timer = resetTimer ? 0 : (m_Timer + Time.deltaTime);
        m_Anim.SetBool("onFire", m_ActiveFire);
    }

    void UpdateDamage()
    {
        m_DamageTimer += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player") {
            HitPlayer();
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        bool timeout = (m_DamageTimer >= m_DamageTime);
        if ((collider.gameObject.tag == "Player") && timeout) {
            HitPlayer();
        }
    }

    void HitPlayer()
    {
        if (m_ActiveFire) {
            m_Player.DecrementLife(m_Damage);
            m_DamageTimer = 0;
        }
    }
}
