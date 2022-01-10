using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 实现效果：使地鼠从洞口上升，并控制地鼠生成的频率


public class MoleControler : MonoBehaviour
{
    // Wac-A-Mole 游戏基本参数设置
    private const float BOTTOM = -3.8f; // 地鼠在地下的 Y 坐标
    private const float TOP = -2.2f; // 地鼠上升后的 Y 坐标
    private float tTime = 0;       // 后续用的计时器
    private float waittime = 0.8f; // 地鼠 UP 状态保持时间
    public float moveSpeed = 0.1f;  // 地鼠上升的速率


    // 枚举地鼠的所有状态
    enum State
    {
        UNDER_GROUND, 
        UP,
        ON_GROUND,
        DOWN,
        HIT,
    }
    State state;

    // 地鼠上升
    public void Up()
    {
        if (this.state == State.UNDER_GROUND)
        {
            this.state = State.UP;
        }
    }

    // 击打地鼠
    public bool Hit()
    {
        // 如果地鼠在地底下，就打不到
        if (this.state == State.UNDER_GROUND)
        {
            return false;
        }
        // 打到地鼠，地鼠下地
        transform.position = new Vector3(transform.position.x, BOTTOM, transform.position.z);
        this.state = State.UNDER_GROUND; // 修改地鼠状态参数
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.state = State.UNDER_GROUND; // 初始化地鼠状态
    }

    // Update is called once per frame
    void Update()
    {
        // 若地鼠的状态是 UP
        if (this.state == State.UP)
        {   // 让地鼠沿着 Y 坐标以 moveSpeed 单位/秒的速率向上移动
            transform.Translate(0, this.moveSpeed, 0);
            // 若地鼠移动过程中的纵坐标超过了上限TOP
            if(transform.position.y >= TOP)
            {
                transform.position = new Vector3(transform.position.x, TOP, transform.position.z);
                this.state = State.ON_GROUND; // 修改地鼠状态参数
                this.tTime = 0; // 开始计时地鼠在地面上的时间
            }
        }

        // 设定地鼠在地面上的逗留时间
        else if(this.state==State.ON_GROUND)
        {
            this.tTime += Time.deltaTime; // Time.deltaTime 返回最后一帧到当前帧的间隔（以秒为单位）
        }
        
        if(tTime>=this.waittime)
        {
            this.state = State.DOWN;
        }

        // 若地鼠的状态是 DOWN
        if (this.state == State.DOWN)
        {   // 让地鼠沿着 Y 坐标以 moveSpeed 单位/秒的速率向下移动
            transform.Translate(0, -this.moveSpeed, 0);
            // 若地鼠移动过程中的纵坐标超过了下限 BOTTOM
            if (transform.position.y <= BOTTOM)
            {
                transform.position = new Vector3(transform.position.x, BOTTOM, transform.position.z);
                this.state = State.UNDER_GROUND; // 修改地鼠状态参数
            }
        }
    }
}
