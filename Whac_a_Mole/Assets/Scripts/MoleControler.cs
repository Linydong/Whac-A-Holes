using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ʵ��Ч����ʹ����Ӷ��������������Ƶ������ɵ�Ƶ��


public class MoleControler : MonoBehaviour
{
    // Wac-A-Mole ��Ϸ������������
    private const float BOTTOM = -3.8f; // �����ڵ��µ� Y ����
    private const float TOP = -2.2f; // ����������� Y ����
    private float tTime = 0;       // �����õļ�ʱ��
    private float waittime = 0.8f; // ���� UP ״̬����ʱ��
    public float moveSpeed = 0.1f;  // ��������������


    // ö�ٵ��������״̬
    enum State
    {
        UNDER_GROUND, 
        UP,
        ON_GROUND,
        DOWN,
        HIT,
    }
    State state;

    // ��������
    public void Up()
    {
        if (this.state == State.UNDER_GROUND)
        {
            this.state = State.UP;
        }
    }

    // �������
    public bool Hit()
    {
        // ��������ڵص��£��ʹ򲻵�
        if (this.state == State.UNDER_GROUND)
        {
            return false;
        }
        // �򵽵��󣬵����µ�
        transform.position = new Vector3(transform.position.x, BOTTOM, transform.position.z);
        this.state = State.UNDER_GROUND; // �޸ĵ���״̬����
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.state = State.UNDER_GROUND; // ��ʼ������״̬
    }

    // Update is called once per frame
    void Update()
    {
        // �������״̬�� UP
        if (this.state == State.UP)
        {   // �õ������� Y ������ moveSpeed ��λ/������������ƶ�
            transform.Translate(0, this.moveSpeed, 0);
            // �������ƶ������е������곬��������TOP
            if(transform.position.y >= TOP)
            {
                transform.position = new Vector3(transform.position.x, TOP, transform.position.z);
                this.state = State.ON_GROUND; // �޸ĵ���״̬����
                this.tTime = 0; // ��ʼ��ʱ�����ڵ����ϵ�ʱ��
            }
        }

        // �趨�����ڵ����ϵĶ���ʱ��
        else if(this.state==State.ON_GROUND)
        {
            this.tTime += Time.deltaTime; // Time.deltaTime �������һ֡����ǰ֡�ļ��������Ϊ��λ��
        }
        
        if(tTime>=this.waittime)
        {
            this.state = State.DOWN;
        }

        // �������״̬�� DOWN
        if (this.state == State.DOWN)
        {   // �õ������� Y ������ moveSpeed ��λ/������������ƶ�
            transform.Translate(0, -this.moveSpeed, 0);
            // �������ƶ������е������곬�������� BOTTOM
            if (transform.position.y <= BOTTOM)
            {
                transform.position = new Vector3(transform.position.x, BOTTOM, transform.position.z);
                this.state = State.UNDER_GROUND; // �޸ĵ���״̬����
            }
        }
    }
}
