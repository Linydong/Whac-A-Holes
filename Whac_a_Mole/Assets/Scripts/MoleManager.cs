using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleManager : MonoBehaviour
{
    // C# 利用List<T>泛型类创建集合，MoleControler 是我们要用的类型
    List<MoleControler> moles = new List<MoleControler>();
    bool generate;   // generate 判断是否应该生成地鼠
    public AnimationCurve maxMoles;

    // Start is called before the first frame update
    void Start()
    {   // GameObject是 Unity 场景中所有实体的基类,创建一个新的 GameObject 对象并命名为 TARGETS
        GameObject[] TARGETS = GameObject.FindGameObjectsWithTag("Mole");

        foreach (GameObject TARGET in TARGETS)
        {   // 每一只地鼠附上 MoleController的组件（亦可在 Unity 中手动拖拽完整）
            moles.Add(TARGET.GetComponent<MoleControler>());
        }

        this.generate = false;
    }

    public void StartGenerate()
    {
        /* MonoBehaviour.StartCoroutine 用于启动协程，返回 Coroutine
         * 协同程序是一个可以暂停执行 (yield) 的函数，直到给定的 YieldInstruction 完成
         * 使用 yield 语句时，协程会暂停执行，并在下一帧自动恢复 */
        StartCoroutine("Generate");
    }

    public void StopGenerate()
    {
        this.generate = false;
    }

    // C# IEnumerator 迭代器
    IEnumerator Generate()
    {
        this.generate = true;

        while (this.generate)
        {
            // WaitForSeconds 使用缩放时间以指定的秒数暂停正在执行的协程
            yield return new WaitForSeconds(1.0f); // 在协程中，WaitForSeconds 只能与 yield 语句结合使用

            int n = moles.Count;
            int maxNum = (int)this.maxMoles.Evaluate(GameManager.time);

            for (int i = 0; i < maxNum; i++)
            {
                // 随机选择地鼠上升
                this.moles[Random.Range(0, n)].Up();
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}