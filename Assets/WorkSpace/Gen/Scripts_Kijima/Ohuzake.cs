using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Ohuzake : MonoBehaviour{
    [SerializeField] ParticleSystem particleSystem;
    float colorSpeed = 1f;

    void Update() {
        if (particleSystem == null) return;

        float t = (Mathf.Sin(Time.time * colorSpeed) + 1f) / 2f;
        Color rainbow = Color.HSVToRGB(t, 1f, 1f);
        particleSystem.startColor = rainbow;
    }
}
