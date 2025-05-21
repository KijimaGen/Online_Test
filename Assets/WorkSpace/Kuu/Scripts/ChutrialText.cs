using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChutrialText : MonoBehaviour
{
    public TextMesh text;
    // テキストを変えるためのカウント
    int TextCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        TextCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Aを押したらテキストを進める
        if (Input.GetKeyDown(KeyCode.JoystickButton0) == true || Input.GetMouseButtonDown(0)) {
            // 参加していなかったら進まない
            if (!(TextCount == 1)) {
            TextCount++;
            }
        }
        // 一言目でLBを押したら二言目を飛ばす
        if (Input.GetKeyDown(KeyCode.JoystickButton4)) {
            if (TextCount == 0) {
                TextCount = 2;
                // 順当に進む
            } else if (TextCount == 1) {
                TextCount++;
            }

        }

        if (TextCount == 0) {
            text.text = "ここはチュートリアルルームです";
        } else if (TextCount == 1) {
            text.text = "LBを押してゲームに参加しましょう";
        } else if (TextCount == 2) {
            text.text = "最大4人まで参加できます";
        } else if (TextCount == 3) {
            text.text = "赤と白の床を踏むとそれぞれのチームになります";
        } else if (TextCount == 4) {

        }
    }
}
