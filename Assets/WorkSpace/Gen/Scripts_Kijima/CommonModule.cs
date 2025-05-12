using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommonModule{
    public static bool CheckHasChild(string name, Transform transform) {
        foreach (Transform child in transform) {  // このオブジェクトのすべての子オブジェクトをループ
            if (child.gameObject.name == name) {  // 子の名前が一致するか確認
                return true;
            }
        }
        return false;
    }

    public static void RemoveChildByName(string name,Transform transform) {
        // 親オブジェクトのすべての子オブジェクトをループ
        foreach (Transform child in transform) {
            if (child.gameObject.name == name) {  // 名前が一致する場合
                // 子オブジェクトを削除
                MonoBehaviour.Destroy(child.gameObject);
                return;  // 最初に見つけた子オブジェクトを削除したら終了
            }
        }
    }
}
