using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    private Slider m_Slider;
    // Start is called before the first frame update
    void Start()
    {
        m_Slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetHealth(int health) {
        m_Slider.value = health;
    }
}
