using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveT : MonoBehaviour{
    Vector3 move;   //プレイヤーの移動量

    void Start(){
        
    }

    void Update(){
        Move();
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
        //move = Vector3.zero;            //移動したのでもう用は無きmoveを初期化
    }
}
