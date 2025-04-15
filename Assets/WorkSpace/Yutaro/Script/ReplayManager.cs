using System.Collections.Generic;
using UnityEngine;

public class ReplayManager : MonoBehaviour
{
    public static ReplayManager instance { get; private set; } = null;

    //public RecordPlayer recordPlayer;
    //public ReplayPlayer replayPlayer;

    public List<RecordPlayer> recordPlayerList = new List<RecordPlayer>();
    public List<ReplayPlayer> replayPlayerList = new List<ReplayPlayer>();

    public bool isPlay = false;
    
    void Start()
    {
        //ReplayManager
        instance = this;

        //Invoke �֐����Ăяo���Ƃ��Ɏ��Ԃ�����
        //Invoke("StartReplay", 6f); // 5�b��Ƀ��v���C�J�n
    }

    public void StartReplay()
    {
        // RecordPlayer�̃f�[�^���N���A���Ă���ēx���v���C���J�n
        
        for (int i = 0; i < replayPlayerList.Count; i++)
        {
            // ���v���C��������
            replayPlayerList[i].Initialize(new List<RecordData>(recordPlayerList[i].recordDatas));
            replayPlayerList[i].enabled = true;
            recordPlayerList[i].enabled = false;
        }
        
    }


    public void EndReplay()
    {
        for (int i = 0; i < replayPlayerList.Count; i++)
        {
            recordPlayerList[i].recordDatas.Clear();
            replayPlayerList[i].recordDatas.Clear();
            replayPlayerList[i].ResetReplay();  // ���������\�b�h���Ăяo��
            replayPlayerList[i].enabled = false;
            recordPlayerList[i].enabled = true;
        }
    }

}