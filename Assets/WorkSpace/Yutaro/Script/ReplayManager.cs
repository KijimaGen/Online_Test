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

        //Invoke 関数を呼び出すときに時間を作れる
        //Invoke("StartReplay", 6f); // 5秒後にリプレイ開始
    }

    public void StartReplay()
    {
        replayPlayer.gameObject.SetActive(true);
        //流すデータをゴーストにする
        replayPlayer.recordDatas = recordPlayer.recordDatas;
        replayPlayer.enabled = true;
        recordPlayer.enabled = false;

    }

}