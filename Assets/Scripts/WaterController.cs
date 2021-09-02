using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    private PlayerController m_Player;
    private float m_Timer;
    private float m_TimeDamage = 1.0f;
    private int m_Damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        m_Player = (PlayerController)FindObjectOfType(typeof(PlayerController));
        m_Timer = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            m_Timer = 0;
        }
    }
    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            m_Timer += Time.deltaTime;
            print("Player is under water\n");
            if (m_Timer >= m_TimeDamage)
            {
                m_Player.DecrementLife(m_Damage);
                m_Timer = 0;
            }
        }
    }

}
