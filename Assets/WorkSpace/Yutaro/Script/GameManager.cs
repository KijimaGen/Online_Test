using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using SuperBlur;

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

    [SerializeField] GameObject resultTitle;
    bool setResult;


    int tempScore = 0;
    GameObject crownPlayer;
    [SerializeField] GameObject crown;

    [SerializeField] Text winnerTeamText;

    [SerializeField] private GameObject wall;

    int redCount = 0;
    int whiteCount = 0;

    public int serveTeam = 0;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        //最初のゲームステートは待機状態
        state = gameState.standBy;
        mainCamera.transform.position = new Vector3 (0,14,-17);

        countDownTitle.SetActive(false);
        roundTime = 120;
        int minutes = Mathf.FloorToInt(roundTime / 60);
        int seconds = Mathf.FloorToInt(roundTime % 60);

        if (roundTime <= 0)
        {
            state = gameState.result;
            roundTime = 0.0f;
            timeText.text = "0:00";
        }
        else
        {
            timeText.text = string.Format("{0}:{1:00}", minutes, seconds);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.V)|| Input.GetKeyUp("joystick 1 button 5"))
        {
            ShuttleInstantiate(new Vector3(playerList[0].transform.position.x, 8, playerList[0].transform.position.z));
        }

        switch (state)
        {
            case gameState.standBy:
                Time.timeScale = 1;
                replayCamera.gameObject.SetActive(false);
                AddPlayer();
                if (Input.GetKeyUp(KeyCode.P))
                {
                    state = gameState.start;
                    roundSetting = true;
                }

                
                ScoreManager.instance.redScore = 0;
                ScoreManager.instance.whiteScore = 0;
                resultTitle.SetActive(false);
                wall.SetActive(false);
                break;
            case gameState.start:
                replayCamera.gameObject.SetActive(false);
                wall.SetActive(true);
                RoundStart();
                Round();

                break;
            case gameState.repaly:
                replayCamera.gameObject.SetActive(true);
                Replay();
                ReplayCancel();

                break;
            case gameState.result:
                Result();
                replayCamera.gameObject.SetActive(false);
                if (Input.GetKeyUp(KeyCode.P))
                {
                    state = gameState.standBy;
                    roundSetting = true;
                }
                break;
        }

        if (roundStart)
        {
            startCount += Time.deltaTime;
            countDownTitle.SetActive(true);
            countDownTitle.GetComponent<TextMeshProUGUI>().text = (3f - startCount).ToString("f0");
            if (startCount > 3f)
            {
                roundStart = false;
                countDownTitle.SetActive(false);
                setReplay = false;
                inGame = true;
                startCount = 0;
            }
            Debug.Log(startCount);
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
                redCount++;
            }
            else if (player.tag == "WhiteTeam")
            {
                whiteCount++;
            }
        }

        for (int i = 0; i < playerList.Count; i++)
        {
            if (playerList[i].tag == "RedTeam")
            {
                if(redCount == 1)
                {
                    playerList[i].transform.position = new Vector3(-5, 0, 2.5f);
                    playerList[i].transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    playerList[i].transform.position = new Vector3(-5, 0, 5 - (i * 5f));
                    playerList[i].transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }

            if (playerList[i].tag == "WhiteTeam")
            {
                if (whiteCount == 1)
                {
                    playerList[i].transform.position = new Vector3(5, 0, 2.5f);
                    playerList[i].transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                else
                {
                    playerList[i].transform.position = new Vector3(5, 0, 5f - (i * 5));
                    playerList[i].transform.rotation = Quaternion.Euler(0, 180, 0);
                }
            }
        }


        redCount = 0;
        whiteCount = 0;
        mainCamera.targetDisplay = 0;
        replayCamera.targetDisplay = 1;

        replayCamera.transform.position = new Vector3(0, 10, -1);
        mainCamera.transform.DOKill();
        mainCamera.transform.DOMove(new Vector3(0, 14, -9), 2);

        if(serveTeam == 0)
            ShuttleInstantiate(new Vector3(-4,3,3.5f));

        if (serveTeam == 1)
            ShuttleInstantiate(new Vector3(4, 3, 3.5f));

        // タイマー開始
        
        roundStart = true;
    }

    public void RoundInitialize()
    {
        roundSetting = true;
        state = gameState.start;
        shuttle.GetComponent<Shuttle>().initialize = false;
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

                if (roundTime <= 0)
                {
                    state = gameState.result;
                    roundTime = 0.0f;
                    timeText.text = "0:00";
                }
                else
                {
                    timeText.text = string.Format("{0}:{1:00}", minutes, seconds);
                }
                
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
        shuttle.GetComponent<Shuttle>().initialize = false;
        setReplay = true;

        mainCamera.targetDisplay = 1;
        replayCamera.targetDisplay = 0;
        replayCamera.transform.position = new Vector3(0, 10, -1);

    }

    private void ReplayCancel()
    {
        for (int i = 0;i < playerList.Count;i++)
        {
            if (!playerList[i].replayCancel) return;
            playerList[i].replayCancel = false;
        }

        ReplayRecorder[] replayObj = FindObjectsOfType<ReplayRecorder>();
        foreach(ReplayRecorder rec in replayObj)
        {
            rec.StopReplayAndReset();
        }
    }

    private void Result()
    {
        if(setResult) return;
        Camera.main.GetComponent<SuperBlurBase>().interpolation = 1;
        Camera.main.GetComponent<SuperBlurBase>().downsample = 1;
        
        resultTitle.SetActive(true);
        resultTitle.GetComponent<Animator>().SetBool("result",true);

        for (int i = 0; i < playerList.Count; i++)
        {    
            playerList[i].GetComponent<Rigidbody>().isKinematic = true;
            playerList[i].transform.localScale = new Vector3(8, 8, 8);
            if (playerList[i].gameObject.tag == "RedTeam")
            {
                if(redCount == 1)
                {
                    playerList[i].transform.parent = resultTitle.transform.Find("Canvas/red壁").transform;
                    playerList[i].transform.localPosition = new Vector3(-18, 0, 0);
                    playerList[i].transform.localRotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    playerList[i].transform.parent = resultTitle.transform.Find("Canvas/red壁").transform;
                    playerList[i].transform.localPosition = new Vector3(-18, 70 - (i * 140), 0);
                    playerList[i].transform.localRotation = Quaternion.Euler(0, 0, 0);
                }
                
            }

            if (playerList[i].gameObject.tag == "WhiteTeam")
            {
                playerList[i].transform.parent = resultTitle.transform.Find("Canvas/white壁").transform;
                playerList[i].transform.localPosition = new Vector3(18, 0, 0);
                playerList[i].transform.localRotation = Quaternion.Euler(0, 180, 0);
            }

            if (playerList[i].score >= tempScore)
            {
                tempScore = playerList[i].score;
                crownPlayer = playerList[i].gameObject;
            }
            else
            {
                if(playerList[i].transform.Find("王冠") != null)
                {
                    Destroy(playerList[i].transform.Find("王冠").gameObject);
                }
            }
        }

        GameObject crownPrefab = Instantiate(crown, crownPlayer.transform);
        crownPrefab.transform.localPosition= new Vector3(3,2,0);

        if(ScoreManager.instance.redScore > ScoreManager.instance.whiteScore)
        {
            winnerTeamText.text = "Red";
        }
        else if(ScoreManager.instance.redScore < ScoreManager.instance.whiteScore)
        {
            winnerTeamText.text = "White";
        }
        else
        {
            winnerTeamText.text = "Drow";
        }
       

        Time.timeScale = 0;
        //setReplay = true;
    }
}
