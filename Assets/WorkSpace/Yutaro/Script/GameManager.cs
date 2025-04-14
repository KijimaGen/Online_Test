using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //ゲームステートの enum型
    enum gameState
    {
        standBy,    //待機状態
        start,      //ゲーム開始
        repaly,     //カメラのリプレイ
        result      //ゲーム終了
    }
    //ゲームステートの変数
    gameState state;

    


    // Start is called before the first frame update
    void Start()
    {
        //最初のゲームステートは待機状態
        state = gameState.standBy;


    }

    // Update is called once per frame
    void Update()
    {
        //ゲームステートのスイッチ文
        switch (state)
        {
            case gameState.standBy:
                break;
            case gameState.start:
                break;
            case gameState.repaly:
                break;
            case gameState.result:
                break;
        }
    }
}
