using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReplayManager : MonoBehaviour
{
    public static ReplayManager instance { get; private set; } = null;

    //public RecordPlayer recordPlayer;
    //public ReplayPlayer replayPlayer;

    public List<RecordPlayer> recordPlayerList = new List<RecordPlayer>();
    public List<ReplayPlayer> replayPlayerList = new List<ReplayPlayer>();

    
    void Start()
    {
        instance = this;

        //Invoke 関数を呼び出すときに時間を作れる
        //Invoke("StartReplay", 6f); // 5秒後にリプレイ開始
    }


    public void StartReplay()
    {
        // RecordPlayerのデータをクリアしてから再度リプレイを開始
        
        for (int i = 0; i < replayPlayerList.Count; i++)
        {
            // リプレイを初期化
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
            replayPlayerList[i].ResetReplay();  // 初期化メソッドを呼び出す
            replayPlayerList[i].enabled = false;
            recordPlayerList[i].enabled = true;
        }
    }
}