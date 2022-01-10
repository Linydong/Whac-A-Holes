using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleManager : MonoBehaviour
{
    // C# ����List<T>�����ഴ�����ϣ�MoleControler ������Ҫ�õ�����
    List<MoleControler> moles = new List<MoleControler>();
    bool generate;   // generate �ж��Ƿ�Ӧ�����ɵ���
    public AnimationCurve maxMoles;

    // Start is called before the first frame update
    void Start()
    {   // GameObject�� Unity ����������ʵ��Ļ���,����һ���µ� GameObject ��������Ϊ TARGETS
        GameObject[] TARGETS = GameObject.FindGameObjectsWithTag("Mole");

        foreach (GameObject TARGET in TARGETS)
        {   // ÿһֻ������ MoleController������������ Unity ���ֶ���ק������
            moles.Add(TARGET.GetComponent<MoleControler>());
        }

        this.generate = false;
    }

    public void StartGenerate()
    {
        /* MonoBehaviour.StartCoroutine ��������Э�̣����� Coroutine
         * Эͬ������һ��������ִͣ�� (yield) �ĺ�����ֱ�������� YieldInstruction ���
         * ʹ�� yield ���ʱ��Э�̻���ִͣ�У�������һ֡�Զ��ָ� */
        StartCoroutine("Generate");
    }

    public void StopGenerate()
    {
        this.generate = false;
    }

    // C# IEnumerator ������
    IEnumerator Generate()
    {
        this.generate = true;

        while (this.generate)
        {
            // WaitForSeconds ʹ������ʱ����ָ����������ͣ����ִ�е�Э��
            yield return new WaitForSeconds(1.0f); // ��Э���У�WaitForSeconds ֻ���� yield �����ʹ��

            int n = moles.Count;
            int maxNum = (int)this.maxMoles.Evaluate(GameManager.time);

            for (int i = 0; i < maxNum; i++)
            {
                // ���ѡ���������
                this.moles[Random.Range(0, n)].Up();
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}