using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private Animator m_Anim;
    private PlayerController m_Player;

    // Start is called before the first frame update
    void Start()
    {
        m_Anim = gameObject.GetComponent<Animator>();
        m_Player = (PlayerController)FindObjectOfType(typeof(PlayerController));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.DetachChildren();
        }
    }
}
