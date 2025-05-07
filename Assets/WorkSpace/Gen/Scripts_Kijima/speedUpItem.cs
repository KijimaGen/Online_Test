using UnityEngine;

public class speedUpItem : Item{
    public override void Initialize() {
        this.transform.Rotate(-90, 0, 0); 
    }

    void FixedUpdate() {
        // �X�s�[�h��Sin�ŕω�������
        float speed = baseSpeed + Mathf.Sin(Time.time * frequency) * amplitude;

        // Z�����X�s�[�h�ɉ����ĉ�]
        transform.Rotate(0f, 0f, speed * Time.deltaTime + 1f);
    }

    
}
