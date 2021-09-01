using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsController : MonoBehaviour
{

    private Animator m_Anim;
    private PlayerController m_Player;
    public int m_Points = 3;

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

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            m_Anim.SetTrigger("collected");
            m_Player.IncrementScore(m_Points);
            Destroy(this.gameObject,0.5f);
        }
    }


}
