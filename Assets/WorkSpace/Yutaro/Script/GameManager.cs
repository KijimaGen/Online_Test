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

    [SerializeField] private GameObject playerPrefab;
    List<Player> playerList = new List<Player>();


    // Start is called before the first frame update
    void Start()
    {
        //最初のゲームステートは待機状態
        state = gameState.standBy;


    }

    // Update is called once per frame
    void Update()
    {
        AddPlayer();

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

    void AddPlayer()
    {
        Player2();
    }

    void Player2()
    {
        if (Input.GetKeyDown("joystick 1 button 5") && Input.GetKeyDown("joystick 1 button 6") && playerList.Count == 1)
        {
            Instantiate(playerPrefab , new Vector3(0,0,0) , Quaternion.identity);
        }
    }
}
