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
    [SerializeField] private GameObject ghostPrefab;
    List<Player> playerList = new List<Player>();

    bool[] addPlayer = { false,false,false,false };

    // Start is called before the first frame update
    void Start()
    {
        //最初のゲームステートは待機状態
        state = gameState.standBy;
        //playerList.Add(GameObject.Find("Player1").GetComponent<Player>());

    }

    // Update is called once per frame
    void Update()
    {
        

        //ゲームステートのスイッチ文
        switch (state)
        {
            case gameState.standBy:
                AddPlayer();
                break;
            case gameState.start:
                for (int i = 0; i < playerList.Count; i++)
                {
                    if(playerList[i].gameObject.tag == "RedTeam")
                    {
                        playerList[i].transform.position = new Vector3(-5 , 0 ,0);
                    }
                }
                    break;
            case gameState.repaly:
                break;
            case gameState.result:
                break;
        }
    }

    void AddPlayer()
    {
        Player1();
        Player2();
        Player3();
        Player4();
    }

    void Player1()
    {
        if (Input.GetKeyDown("joystick 1 button 4") && !addPlayer[0])
        {
            Debug.Log("プレイヤー1が追加されました" + playerList.Count);
            PlayerInstantiate();
            addPlayer[0] = true;
        }
    }
    void Player2()
    {
        if (Input.GetKeyDown("joystick 2 button 4") && !addPlayer[1])
        {
            Debug.Log("プレイヤー2が追加されました" + playerList.Count);
            PlayerInstantiate();
            addPlayer[1] = true;
        }
    }
    void Player3()
    {
        if (Input.GetKeyDown("joystick 3 button 4") && !addPlayer[2])
        {
            Debug.Log("プレイヤー3が追加されました" + playerList.Count);
            PlayerInstantiate();
            addPlayer[2] = true;
        }
    }
    void Player4()
    {
        if (Input.GetKeyDown("joystick 4 button 4") && !addPlayer[3])
        {
            Debug.Log("プレイヤー4が追加されました" + playerList.Count);
            PlayerInstantiate();
            addPlayer[3] = true;
        }
    }

    private void PlayerInstantiate()
    {
        GameObject player = Instantiate(playerPrefab, new Vector3(0, 0, -14), playerPrefab.transform.rotation);
        GameObject ghost = Instantiate(ghostPrefab);
        ghost.transform.localPosition = Vector3.zero;

        ReplayManager.instance.recordPlayerList.Add(player.GetComponent<RecordPlayer>());
        ReplayManager.instance.replayPlayerList.Add(ghost.GetComponent<ReplayPlayer>());
    }
}
