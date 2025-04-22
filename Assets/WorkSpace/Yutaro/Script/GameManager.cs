using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEditor.PlayerSettings;

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
    public List<Player> playerList = new List<Player>();

    bool[] addPlayer = { false,false,false,false };


    [SerializeField] private GameObject shuttlePrefab;


    public bool roundSetting = false;
    public bool roundStart = false;

    public static GameManager instance;
    float startCount = 0;

    public int playerIndex;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        //最初のゲームステートは待機状態
        state = gameState.standBy;
        Camera.main.transform.position = new Vector3 (0,14,-17);
        RoundInitialize();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            state = gameState.start;
            roundSetting = true;
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            ShuttleInstantiate(new Vector3(playerList[0].transform.position.x, 8, playerList[0].transform.position.z));
        }

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

        if (roundStart)
        {
            startCount += Time.unscaledDeltaTime;

            if (startCount > 3f)
            {
                roundStart = false;
                Round();
                //Time.timeScale = 0;
            }
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
        if (Input.GetKey("joystick 1 button 4") && !addPlayer[0])
        {
            Debug.Log("プレイヤー1が追加されました" + playerList.Count);
            PlayerInstantiate(1);
            addPlayer[0] = true;
        }
    }
    void Player2()
    {
        if (Input.GetKey("joystick 2 button 4") && !addPlayer[1])
        {
            Debug.Log("プレイヤー2が追加されました" + playerList.Count);
            PlayerInstantiate(2);
            addPlayer[1] = true;
        }
    }
    void Player3()
    {
        if (Input.GetKeyDown("joystick 3 button 4") && !addPlayer[2])
        {
            Debug.Log("プレイヤー3が追加されました" + playerList.Count);
            PlayerInstantiate(3);
            addPlayer[2] = true;
        }
    }
    void Player4()
    {
        if (Input.GetKeyDown("joystick 4 button 4") && !addPlayer[3])
        {
            Debug.Log("プレイヤー4が追加されました" + playerList.Count);
            PlayerInstantiate(4);
            addPlayer[3] = true;
        }
    }

    private void PlayerInstantiate(int Index)
    {
        GameObject player = Instantiate(playerPrefab, new Vector3(0, 5, -14), playerPrefab.transform.rotation);
        playerList.Add(player.GetComponent<Player>());
        
        playerIndex = Index;
    }

    private void ShuttleInstantiate(Vector3 pos)
    {
        GameObject shuttle = Instantiate(shuttlePrefab, pos, Quaternion.identity);
        shuttle.GetComponent<Shuttle>().Initialize();
    }

    private void RoundStart()
    {
        if (!roundSetting) return;

        roundSetting = false;

        if (playerList.Count != 2)
        {
            Debug.Log("開始できません");
            state = gameState.standBy;
            return;
        }

        foreach (var player in playerList)
        {
            if (player.tag == "RedTeam")
            {
                player.transform.position = new Vector3(-5, 1, 2.5f);
            }
            else if (player.tag == "WhiteTeam")
            {
                player.transform.position = new Vector3(5, 1, 2.5f);
                player.transform.Rotate(new Vector3(0, 180, 0));
            }
        }

        Camera.main.transform.DOKill();
        Camera.main.transform.DOMove(new Vector3(0, 14, -9), 2);

        ShuttleInstantiate(new Vector3(playerList[0].transform.position.x, 8, playerList[0].transform.position.z));

        // タイマー開始
        startCount = 0;
        roundStart = true;
    }

    private void RoundInitialize()
    {
        roundStart = false;
        startCount = 0;
    }

    private void Round()
    {

    }
}
