using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour{
        public float baseSpeed { get; private set; } = 100f;      // ��]�̊���x
        public float amplitude { get; private set; } = 30f;      // �X�s�[�h�h��̑傫��
        public float frequency { get; private set; } = 1f;       // �X�s�[�h�̗h��̑���
        void Update(){
        // �X�s�[�h��Sin�ŕω�������
        float speed = baseSpeed + Mathf.Sin(Time.time * frequency) * amplitude;

        // Z�����X�s�[�h�ɉ����ĉ�]
        transform.Rotate(0f, 0f, speed * Time.deltaTime + 1f);
    }
}
