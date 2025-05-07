using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.InputSystem.iOS;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //ゲームステートの enum型
    public enum gameState
    {
        standBy,    //待機状態
        start,      //ゲーム開始
        repaly,     //カメラのリプレイ
        result      //ゲーム終了
    }
    //ゲームステートの変数
    public gameState state;

    [SerializeField] private GameObject playerPrefab;
    public List<Player> playerList = new List<Player>();

    bool[] addPlayer = { false,false,false,false };


    [SerializeField] private GameObject shuttle;


    public bool roundSetting = false;
    public bool roundStart = false;

    public static GameManager instance;
    float startCount = 0;

    public int playerIndex;

    [SerializeField] private GameObject countDownTitle;
    bool setReplay;

    [SerializeField] Camera mainCamera;
    [SerializeField] Camera replayCamera;

    [SerializeField] Text timeText;

    float roundTime;
    bool inGame;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        roundTime = 120;
        //最初のゲームステートは待機状態
        state = gameState.standBy;
        mainCamera.transform.position = new Vector3 (0,14,-17);

        countDownTitle.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            state = gameState.start;
            roundSetting = true;
        }

        if (Input.GetKeyUp(KeyCode.V)|| Input.GetKeyUp("joystick 1 button 5"))
        {
            ShuttleInstantiate(new Vector3(playerList[0].transform.position.x, 8, playerList[0].transform.position.z));
        }

        switch (state)
        {
            case gameState.standBy:
                //replayCamera.gameObject.SetActive(false);
                AddPlayer();
                break;
            case gameState.start:
                //replayCamera.gameObject.SetActive(false);
                RoundStart();
                Round();


                break;
            case gameState.repaly:
                //replayCamera.gameObject.SetActive(true);
                Replay();
                break;
            case gameState.result:
                //replayCamera.gameObject.SetActive(false);
                break;
        }

        if (roundStart)
        {
            startCount += Time.unscaledDeltaTime;
            countDownTitle.SetActive(true);
            countDownTitle.GetComponent<TextMeshProUGUI>().text = (3.4f - startCount).ToString("f0");
            if (startCount > 3f)
            {
                roundStart = false;
                countDownTitle.SetActive(false);
                setReplay = false;
                inGame = true;
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
        if (Input.GetKey("joystick 1 button 4") && !addPlayer[0] || Input.GetKey(KeyCode.T) && !addPlayer[0])
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
        if (Input.GetKey("joystick 3 button 4") && !addPlayer[2])
        {
            Debug.Log("プレイヤー3が追加されました" + playerList.Count);
            PlayerInstantiate(3);
            addPlayer[2] = true;
        }
    }
    void Player4()
    {
        if (Input.GetKey("joystick 4 button 4") && !addPlayer[3])
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
        shuttle.transform.position = pos;
        shuttle.GetComponent<Shuttle>().Initialize();
    }

    private void RoundStart()
    {
        if (!roundSetting) return;

        roundSetting = false;

        //if (playerList.Count != 2)
        //{
        //    Debug.Log("開始できません");
        //    state = gameState.standBy;
        //    return;
        //}

        foreach (var player in playerList)
        {
            if (player.tag == "RedTeam")
            {
                player.transform.position = new Vector3(-5, 0, 2.5f);
                player.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (player.tag == "WhiteTeam")
            {
                player.transform.position = new Vector3(5, 0, 2.5f);
                player.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
        mainCamera.targetDisplay = 0;
        replayCamera.targetDisplay = 1;

        replayCamera.transform.position = new Vector3(0, 10, -1);
        //replayCamera.GetComponent<Rigidbody>().isKinematic = true;
        mainCamera.transform.DOKill();
        mainCamera.transform.DOMove(new Vector3(0, 14, -9), 2);

        ShuttleInstantiate(new Vector3(playerList[0].transform.position.x, 3, playerList[0].transform.position.z));

        // タイマー開始
        startCount = 0;
        roundStart = true;
    }

    public void RoundInitialize()
    {
        roundSetting = true;
        state = gameState.start;
    }

    private void Round()
    {
        if (inGame)
        {
            if (!shuttle.GetComponent<Rigidbody>().isKinematic)
            {
                roundTime -= Time.deltaTime;

                int minutes = Mathf.FloorToInt(roundTime / 60);
                int seconds = Mathf.FloorToInt(roundTime % 60);

                timeText.text = string.Format("{0}:{1:00}", minutes, seconds);
            }
        }
    }

    private void Replay()
    {
        if (setReplay) return;

        inGame = false;
        GameObject shuttle = GameObject.FindGameObjectWithTag("Shuttle");

        for (int i = 0; i < playerList.Count; i++)
        {
            playerList[i].GetComponent<ReplayRecorder>().StartReplay();
        }
        shuttle.GetComponent<ReplayRecorder>().StartReplay();

        setReplay = true;

        mainCamera.targetDisplay = 1;
        replayCamera.targetDisplay = 0;

        replayCamera.GetComponent<Rigidbody>().velocity = Vector3.zero;
        replayCamera.GetComponent<Rigidbody>().isKinematic = false;
        replayCamera.transform.position = new Vector3(0, 10, -1);

    }

}
