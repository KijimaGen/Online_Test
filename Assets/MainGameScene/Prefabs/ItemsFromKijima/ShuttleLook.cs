/**
 * @file ShuttleLook.cs
 * @brief シャトル凝視用クラス
 * @author kijima
 * @date 2025/5/23
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class ShuttleLook : MonoBehaviour{
    [SerializeField] GameObject shuttle;
    private void Update() {
        if(shuttle == null || shuttle.active != false)
        gameObject.transform.LookAt(shuttle.transform);

        transform.rotation = Quaternion.Euler(0, transform.rotation.y + 180, 0);
    }
}
