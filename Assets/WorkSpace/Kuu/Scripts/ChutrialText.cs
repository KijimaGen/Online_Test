using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChutrialText : MonoBehaviour
{
    public TextMesh text;
    // テキストを変えるためのカウント
    int textCount = 0;
    // テキストの最大数
    int TEXT_MAX = 12;
    // テキストが戻ったことがあるかのフラグ
    bool textJoin = false;
    // Start is called before the first frame update
    void Start()
    {
        textCount = 0;
        textJoin = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 下降制限
        if (textCount < 0) {
            textCount = 0;
        }
        // 上昇制限
        if (textCount > TEXT_MAX) {
            textCount = TEXT_MAX;
        }
        // RBを押したらテキストを進める
        if (Input.GetKeyDown(KeyCode.JoystickButton5) == true || Input.GetMouseButtonDown(0)) {
            // 参加していなかったら進まない
            if (!(textCount == 1) && !textJoin) {
                textCount++;
            } else if(textJoin) {
                textCount++;
            }
        }
        // 一言目でLBを押したら二言目を飛ばす
        if (Input.GetKeyDown(KeyCode.JoystickButton4)) {
            if (textCount == 0) {
                textCount = 2;
                textJoin = true;
                // 順当に進む
            } else if (textCount == 1) {
                textCount++;
                textJoin = true;
            }

        }
        // Bを押したらテキストを戻す
        if (Input.GetKeyDown(KeyCode.JoystickButton2) == true || Input.GetMouseButtonDown(1)) {
            textCount--;
        }

        if (textCount == 0) {
            text.text = "ここはチュートリアルルームです";
        } else if (textCount == 1) {
            text.text = "LBを押してゲームに参加しましょう";
        } else if (textCount == 2) {
            text.text = "最大4人まで参加できます";
        } else if (textCount == 3) {
            text.text = "赤と白の床を踏むとそれぞれのチームになります";
        } else if (textCount == 4) {
            text.text = "Bを押すとラケットを振れます";
        } else if (textCount == 5) {
            text.text = "チャージがMAXになるとジャンプスマッシュが打てます";
        } else if (textCount == 6) {
            text.text = "Bで相手プレイヤーを攻撃すると吹っ飛びます";
        } else if (textCount == 7) {
            text.text = "倒れてしまった時はAを押せば戻ります";
        } else if (textCount == 8) {
            text.text = "スペシャルゲージがMAXの時にYを押すとスペシャルが使えます";
        } else if (textCount == 9) {
            text.text = "スペシャルゲージは時間経過とシャトルを打つことで増えます";
        } else if (textCount == 10) {
            text.text = "スペシャルはコスチュームによって効果が変わります";
        }　else if (textCount == 11) {
            text.text = "イメチェンなどは対応する床を踏みながらLBで変わります";
        } else if (textCount == TEXT_MAX) {
            text.text = "STARTボタンで試合開始です";
        }
    }
}
