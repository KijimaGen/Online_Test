using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEditor.PlayerSettings;

public class GameManager : MonoBehaviour
{
    //�Q�[���X�e�[�g�� enum�^
    enum gameState
    {
        standBy,    //�ҋ@���
        start,      //�Q�[���J�n
        repaly,     //�J�����̃��v���C
        result      //�Q�[���I��
    }
    //�Q�[���X�e�[�g�̕ϐ�
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

        //�ŏ��̃Q�[���X�e�[�g�͑ҋ@���
        state = gameState.standBy;
        Camera.main.transform.position = new Vector3 (0,14,-17);
        RoundInitialize();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.P))
        {
            ShuttleInstantiate(new Vector3( playerList[0].transform.position.x , 8 , playerList[0].transform.position.z));
            //state = gameState.start;
            //roundSetting = true;
        }

        //�Q�[���X�e�[�g�̃X�C�b�`��
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
        if (Input.GetKeyDown(KeyCode.T) && !addPlayer[0])
        {
            Debug.Log("�v���C���[1���ǉ�����܂���" + playerList.Count);
            PlayerInstantiate(1);
            addPlayer[0] = true;
        }
    }
    void Player2()
    {
        if (Input.GetKeyDown("joystick 2 button 4") && !addPlayer[1])
        {
            Debug.Log("�v���C���[2���ǉ�����܂���" + playerList.Count);
            PlayerInstantiate(2);
            addPlayer[1] = true;
        }
    }
    void Player3()
    {
        if (Input.GetKeyDown("joystick 3 button 4") && !addPlayer[2])
        {
            Debug.Log("�v���C���[3���ǉ�����܂���" + playerList.Count);
            PlayerInstantiate(3);
            addPlayer[2] = true;
        }
    }
    void Player4()
    {
        if (Input.GetKeyDown("joystick 4 button 4") && !addPlayer[3])
        {
            Debug.Log("�v���C���[4���ǉ�����܂���" + playerList.Count);
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

        if(roundSetting)
        {
            for (int i = 0; i < playerList.Count; i++)
            {
                if (playerList.Count == 2)
                {
                    if (playerList[i].gameObject.tag == "RedTeam")
                    {
                        playerList[i].transform.position = new Vector3(-5, 2, 2.5f);
                    }

                    if (playerList[i].gameObject.tag == "WhiteTeam")
                    {
                        playerList[i].transform.position = new Vector3(5, 2, 2.5f);
                    }
                }

                else
                {
                    Debug.Log("�J�n�ł��܂���");
                    roundSetting = false;
                    state = gameState.standBy;
                }
            }


            ShuttleInstantiate(new Vector3(playerList[0].transform.position.x, 8, playerList[0].transform.position.z));
            Camera.main.transform.DOMove(new Vector3(0, 14, -9), 2);
            startCount += Time.deltaTime;
            if(startCount > 3)
            {
                Round();
                roundSetting = false;
            }
            Time.timeScale = 0;
            
        }
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
