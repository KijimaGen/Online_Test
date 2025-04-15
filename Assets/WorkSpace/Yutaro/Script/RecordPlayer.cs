using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RecordPlayer : MonoBehaviour
{

    public List<RecordData> recordDatas = new List<RecordData>();
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        RecordData data = new RecordData
        {
            time = timer,
            position = transform.position,
            rotation = transform.rotation
        };

        recordDatas.Add(data);
    }
}
