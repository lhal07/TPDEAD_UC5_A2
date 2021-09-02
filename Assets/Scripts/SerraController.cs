using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerraController : MonoBehaviour
{
    private PlayerController m_Player;
    public int m_Damage = 3;
    // Start is called before the first frame update
    void Start()
    {
        m_Player = (PlayerController)FindObjectOfType(typeof(PlayerController));
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            m_Player.DecrementLife(m_Damage);
        }
    }
}
