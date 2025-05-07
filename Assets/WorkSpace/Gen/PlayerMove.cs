using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour{
    Vector3 move;   //�v���C���[�̈ړ���
    //Vector3 rotation;   //�v���C���[�̉�]

    Animator animator;  //�A�j���[�^�[�̃R���|�[�l���g

    [SerializeField]bool shoot;         //�A�j���[�V�����̒e������
    ///[SerializeField] Ball ball;


    void Start(){
        animator = GetComponent<Animator>();
    }

    void Update(){
        Move();
        Attack();
       
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

        if(move != Vector3.zero) 
            animator.SetBool("Walk", true);
        
        else
            animator.SetBool("Walk", false);

        move = Vector3.zero;            //�ړ������̂ł����p�͖���move��������
    }

    //�U���p�֐�
    void Attack() {
        //RT�{�^��
        if (Input.GetAxis("Axis 3") > 0f && Input.GetAxis("Axis 10") > 0f) {
            animator.SetTrigger("Attack");
            shoot = true;
        }
    }

    private void OnTriggerStay(UnityEngine.Collider other) {
        if (shoot && other.gameObject.tag == "Ball") {

        }
           
    }

    public void OnAnimationEnd() {
        shoot = false;
    }
}
