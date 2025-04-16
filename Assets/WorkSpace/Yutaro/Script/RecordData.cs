using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct RecordData
{
    public float time;
    public Vector3 position;
    public Quaternion rotation;

}

public struct ReplayFrame
{
    public Vector3 position;
    public Quaternion rotation;
    public float time;
}
