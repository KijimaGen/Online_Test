using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChangeMaterialTest : MonoBehaviour{
    Color targetColor = Color.white;
    [SerializeField] Material material;

    public float colorSpeed = 1f;

    private void Update() {
        if (material == null) return;

        float t = Mathf.PingPong(Time.time * colorSpeed, 1f);
        Color rainbow = Color.HSVToRGB(t, 1f, 1f); // HSV‚Å‚®‚é‚®‚é
        material.color = rainbow;
    }  
}
