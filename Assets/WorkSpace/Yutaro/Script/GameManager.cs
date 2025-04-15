using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    List<Player> playerList = new List<Player>();


    // Start is called before the first frame update
    void Start()
    {
        //�ŏ��̃Q�[���X�e�[�g�͑ҋ@���
        state = gameState.standBy;


    }

    // Update is called once per frame
    void Update()
    {
        AddPlayer();

        //�Q�[���X�e�[�g�̃X�C�b�`��
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
