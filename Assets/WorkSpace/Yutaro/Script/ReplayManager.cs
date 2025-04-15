using UnityEngine;

public class ReplayManager : MonoBehaviour
{
    public RecordPlayer recordPlayer;
    public ReplayPlayer replayPlayer;

    void Start()
    {
        Invoke("StartReplay", 5f); // 5�b��Ƀ��v���C�J�n
    }

    void StartReplay()
    {
        replayPlayer.recordDatas = recordPlayer.recordDatas;
        replayPlayer.enabled = true;
        recordPlayer.enabled = false;
    }
}