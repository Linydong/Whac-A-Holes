using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // ʹ�� UI ����ʱ��Ҫ����İ�
using UnityEngine.SceneManagement;  

public class GameManager : MonoBehaviour
{
    // ��Ϸ�ܿ���̨����
    private float timer; //��ʱ��
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
        // Application.targetFrameRate ����Ϸ������ָ����֡����Ⱦ
        Application.targetFrameRate = 60;
        this.state = State.START;  // ��Ϸ״̬����Ϊ��ʼ
        this.timer = 0;  // ��ʱ����ʼ��
        this.anim = GameObject.Find("Canvas").GetComponent<Animator>(); // ��ȡ���� Canvas �����ϵĶ���
        this.moleManager = GameObject.Find("GameManager").GetComponent<MoleManager>(); // ��ȡ GameManager �ϵ� MoleManager ���
        this.remainingTime = GameObject.Find("RemainingTime").GetComponent<Text>(); // ��ȡʣ��ʱ��
        this.audio = GetComponent<AudioSource>(); // ��Ϸ��Ƶ��ʼ��
    }

    // Update is called once per frame
    void Update()
    {
        if (this.state == State.START)
        {   // ��Ϸ��ʼǰ��������Ƿ񰴼�
            if (Input.GetMouseButtonDown(0))
            {
                this.state = State.PLAY; // ����������������״̬
                // ���� start �ı�־
                this.anim.SetTrigger("StartTrigger");

                // ��ʼ���ɵ���
                this.moleManager.StartGenerate();

                // ��ʼ��������
                this.audio.Play();
            }
        }
        else if(this.state == State.PLAY)
           {
            this.timer += Time.deltaTime;
            time = this.timer / timeLimit;

            // ʱ���ж�
            if(this.timer>timeLimit)
            {
                this.state = State.GAMEOVER; //��ʱ�䵽����Ϸ����

                // ��ʾ��Ϸ������־
                this.anim.SetTrigger("GameOverTrigger");

                // ֹͣ���ɵ���
                this.moleManager.StopGenerate();

                this.timer = 0;

                // ����ֹͣ����
                this.audio.loop = false;

            }
            // ��ʾ����ʱʱ��
            this.remainingTime.text = "Time:" + ((int)(timeLimit - timer)).ToString("D2"); 
        }
        else if(this.state==State.GAMEOVER)
        {   // ��Ϸ����
            this.timer += Time.deltaTime;
            if(this.timer>waitTime)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            this.remainingTime.text = "";
        }
    }
}
