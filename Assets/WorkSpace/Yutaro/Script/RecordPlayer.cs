using System.Collections.Generic;
using UnityEngine;

public class RecordPlayer : MonoBehaviour
{
    public List<RecordData> recordDatas = new List<RecordData>();

    private float recordDuration = 10f; // 保持する秒数（ここを調整）
    private float timer = 0f;

    float recordInterval = 0.01f;
    float recordTimer = 0f;


    void Update()
    {
        timer += Time.deltaTime;

        RecordData data = new RecordData
        {
            time = timer,
            position = transform.position,
            rotation = transform.rotation
        };

        recordTimer += Time.deltaTime;
        if (recordTimer >= recordInterval)
        {
            recordTimer = 0f;
            recordDatas.Add(data);
        }
        
        // 古いデータを削除して最新N秒だけに制限
        float earliestTime = timer - recordDuration;
        while (recordDatas.Count > 0 && recordDatas[0].time < earliestTime)
        {
            recordDatas.RemoveAt(0);
        }

    }
}
