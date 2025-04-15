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
            return;

        timer += Time.deltaTime;

        while (currentIndex < recordDatas.Count && recordDatas[currentIndex].time <= timer)
        {
            transform.position = recordDatas[currentIndex].position;
            transform.rotation = recordDatas[currentIndex].rotation;
            currentIndex++;
        }
        
    }
}