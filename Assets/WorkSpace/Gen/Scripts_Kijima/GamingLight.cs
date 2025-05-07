using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamingLight : MonoBehaviour{
    [SerializeField] Light dirLight; // InspectorでDirectional Lightをアタッチ
     float colorSpeed = 1f;

    void Update() {
        if (dirLight == null) return;

        float t = (Mathf.Sin(Time.time * colorSpeed) + 1f) / 2f;
        Color rainbow = Color.HSVToRGB(t, 1f, 1f);
        dirLight.color = rainbow;
    }
}
