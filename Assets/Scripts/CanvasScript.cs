using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
    Text m_Score;

    // Start is called before the first frame update
    void Start()
    {
        m_Score = this.transform.GetChild(1).GetComponent<Text>();
        SetScore(0);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetScore(int score) {
        m_Score.text = " SCORE: " + score.ToString();
    }
}
