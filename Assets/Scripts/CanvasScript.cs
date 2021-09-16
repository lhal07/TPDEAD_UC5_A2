using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
    Text m_Score;
    Image m_VictoryImage;

    // Start is called before the first frame update
    void Start()
    {
        m_Score = this.transform.GetChild(1).GetComponent<Text>();
        m_VictoryImage = this.transform.GetChild(2).GetComponent<Image>();
        SetScore(0);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetScore(int score) {
        m_Score.text = " SCORE: " + score.ToString();
    }

    public void SetVictory(bool state=true) {
        m_VictoryImage.enabled = true;
    }
}
