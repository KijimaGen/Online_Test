using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shotPoint;
    [SerializeField] ParticleSystem shotEffect;

    float interval;

    float time;
    Animator anim;
    Vector3 center;
    // Start is called before the first frame update
    void Start()
    {
        center = transform.position;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        interval += Time.deltaTime;
        if(interval > 3)
        {
            interval = 0;
            // ここで大砲の角度に -90度補正した回転を使って弾を生成
            float angleY = transform.eulerAngles.y;
            Quaternion bulletRotation = Quaternion.Euler(0, angleY - 90f, 0);
            GameObject obj = Instantiate(bullet, shotPoint.position, bulletRotation);
            anim.SetBool("Shot", true);
            
        }


        time += Time.deltaTime;
        //float angle = time * 2 * Mathf.PI * 2;
        //float x = Mathf.Abs(Mathf.Cos(angle)) * 5;
        //float z = Mathf.Sin(angle) * 5;
        float angle = Mathf.Cos(time * 0.2f * Mathf.PI * 2f) * 20f;
        float currentY = transform.eulerAngles.y; // ← これで角度（度）として取得
        transform.rotation = Quaternion.Euler(90f, currentY + angle * Time.deltaTime, 0f);

    }

    public void StopAnim()
    {
        anim.SetBool("Shot", false);
    }
}
