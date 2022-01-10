using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public static int score;

    Text text;

    private void Awake()
    {
        this.text = GetComponent<Text>();
        score = 0;
    }
   
    // Update is called once per frame
    void Update()
    {   // 每一帧刷新的时候显示分数
        this.text.text = "Score:" + score;
    }
}
