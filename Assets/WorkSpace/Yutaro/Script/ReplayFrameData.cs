using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ReplayFrameData
{
    public Vector3 position;
    public Quaternion rotation;
    public float time;

    // アニメーション状態（パラメーターで管理する想定）
    public Dictionary<string, float> floatParams = new Dictionary<string, float>();
    public Dictionary<string, bool> boolParams = new Dictionary<string, bool>();
}
