using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayPlayer : MonoBehaviour
{
    public List<RecordData> recordDatas;
    private float timer = 0f;
    private int currentIndex = 0;

    void Update()
    {
        if (recordDatas == null || recordDatas.Count == 0)
        {
            gameObject.SetActive(false);
            return;
        }

        timer += Time.deltaTime;

        while (currentIndex < recordDatas.Count && recordDatas[currentIndex].time <= timer)
        {
            transform.position = recordDatas[currentIndex].position;
            transform.rotation = recordDatas[currentIndex].rotation;
            currentIndex++;
        }

        if (currentIndex >= recordDatas.Count)
        {
            ReplayManager.instance.EndReplay();
            gameObject.SetActive(false);
        }

    }

    // リセット用メソッド
    public void ResetReplay()
    {
        //timer = 0f;
        currentIndex = 0;
        gameObject.SetActive(true);
    }

    // 初期化用メソッド
    public void Initialize(List<RecordData> newDatas)
    {
        // 過去のデータをクリアして、新しいデータをセット
        recordDatas.Clear();  // 以前のデータを消去
        recordDatas.AddRange(newDatas); // 新しいデータを追加

        ResetReplay();  // タイマーとインデックスをリセット
    }

}
