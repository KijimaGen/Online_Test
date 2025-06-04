using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SukonbuBullet : MonoBehaviour
{
    Rigidbody rb;

    public float shakeAmount = 0.1f;
    public float chargeTime = 2f;
    private float power = 1f;

    int num;
    Player player;
    bool shot;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = transform.parent.GetComponent<Player>();
        num = player.index;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GameObject shuttle = GameObject.Find("シャトル 1");

        if (Input.GetKey("joystick " + num + " button 3"))
        {
            rb.isKinematic = true;
            power *= 1.02f;
            transform.LookAt(shuttle.transform.position);
            Transform childAngle = transform.GetChild(0).gameObject.transform;
            childAngle.localRotation = Quaternion.Euler(power, 90, 90);
            //transform.localRotation = Quaternion.Euler(transform.rotation.x,transform.rotation.y,power);
        }
        else
        {
            rb.isKinematic = false;
            if (power > 0)
            {
                Vector3 dir = (shuttle.transform.position - transform.position).normalized;
                rb.AddForce(dir * power * 0.1f, ForceMode.Impulse);

                power = 0;
            }
        }



    }

    private void OnCollisionEnter(Collision collision)
    {
        TriggerShockwave();
    }

    public void TriggerShockwave()
    {
        Vector3 ExplosionPos = new Vector3(transform.position.x, transform.position.y - 3, transform.position.z);
        // 一定範囲内のオブジェクトを取得
        Collider[] colliders = Physics.OverlapSphere(ExplosionPos, 20);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                if (ReplayRecorder.instance.isReplaying) return;
                // 衝撃波を外向きに与える
                rb.AddExplosionForce((power + 1) * 1000, ExplosionPos, 20);
                Destroy(gameObject);
            }
        }

    }

}
