using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveT : MonoBehaviour{
    Vector3 move;   //�v���C���[�̈ړ���

    void Start(){
        
    }

    void Update(){
        Move();
    }

    //�ړ��p�֐�
    void Move() {
        //�������̓��͊֌W
        if (Input.GetAxis("Axis 1") > 0f)
            move.x = 0.01f;
        else if (Input.GetAxis("Axis 1") < 0f)
            move.x = -0.01f;

        //�c�����̓��͊֌W
        if (Input.GetAxis("Axis 2") > 0f)
            move.z = -0.01f;
        else if (Input.GetAxis("Axis 2") < 0f)
            move.z = 0.01f;



      
        transform.position += move;     //�v�Z�����ړ��ʂ����ۂɃI�u�W�F�N�g�̍��W�ɉ��Z
        //move = Vector3.zero;            //�ړ������̂ł����p�͖���move��������
    }
}
