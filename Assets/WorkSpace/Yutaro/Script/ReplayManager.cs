using UnityEngine;

public class ReplayManager : MonoBehaviour
{
    public static ReplayManager instance { get; private set; } = null;

    public RecordPlayer recordPlayer;
    public ReplayPlayer replayPlayer;

    void Start()
    {
        //ReplayManager
        instance = this;

        //Invoke �֐����Ăяo���Ƃ��Ɏ��Ԃ�����
        //Invoke("StartReplay", 6f); // 5�b��Ƀ��v���C�J�n
    }

    public void StartReplay()
    {
        replayPlayer.gameObject.SetActive(true);
        //�����f�[�^���S�[�X�g�ɂ���
        replayPlayer.recordDatas = recordPlayer.recordDatas;
        replayPlayer.enabled = true;
        recordPlayer.enabled = false;

    }

}