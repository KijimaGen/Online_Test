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

    


    // Start is called before the first frame update
    void Start()
    {
        //�ŏ��̃Q�[���X�e�[�g�͑ҋ@���
        state = gameState.standBy;


    }

    // Update is called once per frame
    void Update()
    {
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
}
