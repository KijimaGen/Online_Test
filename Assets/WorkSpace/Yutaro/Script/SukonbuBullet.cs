using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SukonbuBullet : MonoBehaviour
{
    Rigidbody rb;

    public float shakeAmount = 0.1f;
    public float chargeTime = 2f;
    private float power = 0f;

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
        if (Input.GetKey("joystick " + num + " button 3"))
        {
            Vector3 shake = Random.insideUnitSphere * shakeAmount;
            transform.position = shake;

            power += 0.1f;
        }
        else
        {
            if (power > 0)
            {
                GameObject shuttle = GameObject.Find("シャトル 1");
                Vector3 dir = (shuttle.transform.position - transform.position).normalized;
                float force = power * 10f; // パワーに応じた力
                rb.AddForce(dir * force, ForceMode.Impulse);

                power = 0;
            }
        }

    }

}
