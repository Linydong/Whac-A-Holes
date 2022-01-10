using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HummerController : MonoBehaviour
{   
    public GameObject particle; // ��ը��������Ч
    public AudioClip hitSE; // ���е÷ֵ���Ч

    AudioSource audio;



    // Start is called before the first frame update
    void Start()
    {
        this.audio = GetComponent<AudioSource>(); // ��ʼ�������Ƶ
    }

    IEnumerator Hit(Vector3 target)
    {
        // �´�
        transform.position = new Vector3(target.x, 0, target.z);

        /* Instantiate ����ʵ��������
         * public static Object Instantiate (Object original, Vector3 position, Quaternion rotation)
         * original: Ҫ���Ƶ����ж���
         * position���¶����λ��
         * rotation���¶���ķ���*/
        Instantiate(this.particle, transform.position, Quaternion.identity);
        Camera.main.GetComponent<CameraController>().Shake();

        this.audio.PlayOneShot(this.hitSE);

        yield return new WaitForSeconds(0.1f);

        // �ϴ�
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
        {   // Camera.ScreenPointToRay(Vector3 pos) ���ش����ͨ����Ļ�������, RaycastHit�����ڴ洢�������ߺ��������ײ��Ϣ��
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            /* Physics.Raycast (Vector3 origin, Vector3 direction, float maxDistance)
             * origin����������������ϵ�е����
             * direction�����ߵķ���
             * maxDistance������Ӧ�����ײ��������*/
            if (Physics.Raycast(ray, out hit, 100))
            {
                GameObject mole = hit.collider.gameObject; // RaycastHit �еı���colllider�������е�����
                // �жϵ����Ƿ񱻴����
                bool isHit = mole.GetComponent<MoleControler>().Hit();
                // �������У�չʾ���Ӻ�Ч��
                if (isHit)
                {
                    StartCoroutine(Hit(mole.transform.position));
                    ScoreCounter.score += 10;
                }
            }
        }
    }
}