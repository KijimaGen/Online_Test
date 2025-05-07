using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoStanCoin : Item{

    public override void Initialize() {
        
    }

    void FixedUpdate() {
        // スピードをSinで変化させる
        float speed = baseSpeed + Mathf.Sin(Time.time * frequency) * amplitude;

        // Z軸をスピードに応じて回転
        transform.Rotate(0f, speed * Time.deltaTime + 1f, 0f);
    }

}
