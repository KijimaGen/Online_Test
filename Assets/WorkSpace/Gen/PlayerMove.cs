using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour{
    Vector3 move;   //プレイヤーの移動量
    //Vector3 rotation;   //プレイヤーの回転

    Animator animator;  //アニメーターのコンポーネント

    [SerializeField]bool shoot;         //アニメーションの弾き中か
    ///[SerializeField] Ball ball;


    void Start(){
        animator = GetComponent<Animator>();
    }

    void Update(){
        Move();
        Attack();
       
    }

    //移動用関数
    void Move() {
        //横方向の入力関係
        if (Input.GetAxis("Axis 1") > 0f)
            move.x = 0.01f;
        else if (Input.GetAxis("Axis 1") < 0f)
            move.x = -0.01f;

        //縦方向の入力関係
        if (Input.GetAxis("Axis 2") > 0f)
            move.z = -0.01f;
        else if (Input.GetAxis("Axis 2") < 0f)
            move.z = 0.01f;

        transform.position += move;     //計算した移動量を実際にオブジェクトの座標に加算

        if(move != Vector3.zero) 
            animator.SetBool("Walk", true);
        
        else
            animator.SetBool("Walk", false);

        move = Vector3.zero;            //移動したのでもう用は無きmoveを初期化
    }

    //攻撃用関数
    void Attack() {
        //RTボタン
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
