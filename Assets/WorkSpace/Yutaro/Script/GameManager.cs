using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
    [SerializeField] private GameObject playerGhostPrefab;
    List<Player> playerList = new List<Player>();

    bool[] addPlayer = { false,false,false,false };


    [SerializeField] private GameObject shuttlePrefab;
    [SerializeField] private GameObject shuttleGhostPrefab;


    public bool roundStart = false;

    public static GameManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        //最初のゲームステートは待機状態
        state = gameState.standBy;
        Camera.main.transform.position = new Vector3 (0,14,-17);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.P))
        {
            //ShuttleInstantiate();
            state = gameState.start;
        }

        //ゲームステートのスイッチ文
        switch (state)
        {
            case gameState.standBy:
                AddPlayer();
                break;
            case gameState.start:
                RoundStart();
                

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
        GameObject player = Instantiate(playerPrefab, new Vector3(0, 5, -14), playerPrefab.transform.rotation);
        playerList.Add(player.GetComponent<Player>());
        GameObject ghost = Instantiate(playerGhostPrefab);
        ghost.transform.localPosition = Vector3.zero;

        ReplayManager.instance.recordPlayerList.Add(player.GetComponent<RecordPlayer>());
        ReplayManager.instance.replayPlayerList.Add(ghost.GetComponent<ReplayPlayer>());
    }

    private void ShuttleInstantiate()
    {
        GameObject shuttle = Instantiate(shuttlePrefab, new Vector3(-3f, 7, 0), Quaternion.identity);
        GameObject ghost = Instantiate(shuttleGhostPrefab);
        ghost.transform.localPosition = Vector3.zero;

        ReplayManager.instance.recordPlayerList.Add(shuttle.GetComponent<RecordPlayer>());
        ReplayManager.instance.replayPlayerList.Add(ghost.GetComponent<ReplayPlayer>());
    }

    private void RoundStart()
    {
        if (!roundStart)
        {
            for (int i = 0; i < playerList.Count; i++)
            {
                if (playerList[i].gameObject.tag == "RedTeam")
                {
                    playerList[i].transform.position = new Vector3(-5, 0, 0);
                }
            }

            Camera.main.transform.DOMove(new Vector3(0, 14, -9), 2);


            roundStart = true;
        }
    }

    private void Initialize()
    {

    }
}
