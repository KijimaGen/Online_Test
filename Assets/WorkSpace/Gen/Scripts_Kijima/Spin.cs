using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour{
        public float baseSpeed { get; private set; } = 100f;      // 回転の基準速度
        public float amplitude { get; private set; } = 30f;      // スピード揺れの大きさ
        public float frequency { get; private set; } = 1f;       // スピードの揺れの速さ
        void Update(){
        // スピードをSinで変化させる
        float speed = baseSpeed + Mathf.Sin(Time.time * frequency) * amplitude;

        // Z軸をスピードに応じて回転
        transform.Rotate(0f, 0f, speed * Time.deltaTime + 1f);
    }
}
