using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HummerController : MonoBehaviour
{   
    public GameObject particle; // 爆炸的粒子特效
    public AudioClip hitSE; // 击中得分的音效

    AudioSource audio;



    // Start is called before the first frame update
    void Start()
    {
        this.audio = GetComponent<AudioSource>(); // 初始化获得音频
    }

    IEnumerator Hit(Vector3 target)
    {
        // 下锤
        transform.position = new Vector3(target.x, 0, target.z);

        /* Instantiate 用于实例化对象
         * public static Object Instantiate (Object original, Vector3 position, Quaternion rotation)
         * original: 要复制的现有对象
         * position：新对象的位置
         * rotation：新对象的方向*/
        Instantiate(this.particle, transform.position, Quaternion.identity);
        Camera.main.GetComponent<CameraController>().Shake();

        this.audio.PlayOneShot(this.hitSE);

        yield return new WaitForSeconds(0.1f);

        // 上锤
        for(int i=0;i<6;i++)
        {
            transform.Translate(0, 0, 1.0f);
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {   // Camera.ScreenPointToRay(Vector3 pos) 返回从相机通过屏幕点的射线, RaycastHit类用于存储发射射线后产生的碰撞信息。
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            /* Physics.Raycast (Vector3 origin, Vector3 direction, float maxDistance)
             * origin：射线在世界坐标系中的起点
             * direction：射线的方向
             * maxDistance：射线应检查碰撞的最大距离*/
            if (Physics.Raycast(ray, out hit, 100))
            {
                GameObject mole = hit.collider.gameObject; // RaycastHit 中的变量colllider返回命中的物体
                // 判断地鼠是否被打击中
                bool isHit = mole.GetComponent<MoleControler>().Hit();
                // 若被击中，展示锤子和效果
                if (isHit)
                {
                    StartCoroutine(Hit(mole.transform.position));
                    ScoreCounter.score += 10;
                }
            }
        }
    }
}