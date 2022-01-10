using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // 使用 UI 界面时需要导入的包
using UnityEngine.SceneManagement;  

public class GameManager : MonoBehaviour
{
    // 游戏总控制台参数
    private float timer; //计时器
    public float timeLimit = 30;
    const float waitTime = 5;
    public static float time;

    enum State
    {
        START,
        PLAY,
        GAMEOVER,
    }State state;

    Animator anim;
    MoleManager moleManager;
    Text remainingTime;
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        // Application.targetFrameRate 让游戏尝试以指定的帧率渲染
        Application.targetFrameRate = 60;
        this.state = State.START;  // 游戏状态设置为开始
        this.timer = 0;  // 计时器初始化
        this.anim = GameObject.Find("Canvas").GetComponent<Animator>(); // 获取挂在 Canvas 对象上的动画
        this.moleManager = GameObject.Find("GameManager").GetComponent<MoleManager>(); // 获取 GameManager 上的 MoleManager 组件
        this.remainingTime = GameObject.Find("RemainingTime").GetComponent<Text>(); // 获取剩余时间
        this.audio = GetComponent<AudioSource>(); // 游戏音频初始化
    }

    // Update is called once per frame
    void Update()
    {
        if (this.state == State.START)
        {   // 游戏开始前监听鼠标是否按键
            if (Input.GetMouseButtonDown(0))
            {
                this.state = State.PLAY; // 若按键，进入游玩状态
                // 藏起 start 的标志
                this.anim.SetTrigger("StartTrigger");

                // 开始生成地鼠
                this.moleManager.StartGenerate();

                // 开始播放音乐
                this.audio.Play();
            }
        }
        else if(this.state == State.PLAY)
           {
            this.timer += Time.deltaTime;
            time = this.timer / timeLimit;

            // 时间判断
            if(this.timer>timeLimit)
            {
                this.state = State.GAMEOVER; //若时间到，游戏结束

                // 显示游戏结束标志
                this.anim.SetTrigger("GameOverTrigger");

                // 停止生成地鼠
                this.moleManager.StopGenerate();

                this.timer = 0;

                // 音乐停止播放
                this.audio.loop = false;

            }
            // 显示倒计时时间
            this.remainingTime.text = "Time:" + ((int)(timeLimit - timer)).ToString("D2"); 
        }
        else if(this.state==State.GAMEOVER)
        {   // 游戏重启
            this.timer += Time.deltaTime;
            if(this.timer>waitTime)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            this.remainingTime.text = "";
        }
    }
}
