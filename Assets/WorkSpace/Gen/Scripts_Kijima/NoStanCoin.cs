using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoStanCoin : Item{

    public override void Initialize() {
        
    }

    void FixedUpdate() {
        // �X�s�[�h��Sin�ŕω�������
        float speed = baseSpeed + Mathf.Sin(Time.time * frequency) * amplitude;

        // Z�����X�s�[�h�ɉ����ĉ�]
        transform.Rotate(0f, speed * Time.deltaTime + 1f, 0f);
    }

}
