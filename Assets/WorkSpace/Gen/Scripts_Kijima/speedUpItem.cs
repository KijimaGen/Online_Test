using UnityEngine;

public class speedUpItem : Item{
    public override void Initialize() {
        this.transform.Rotate(-90, 0, 0); 
    }

    void FixedUpdate() {
        // スピードをSinで変化させる
        float speed = baseSpeed + Mathf.Sin(Time.time * frequency) * amplitude;

        // Z軸をスピードに応じて回転
        transform.Rotate(0f, 0f, speed * Time.deltaTime + 1f);
    }

    
}
