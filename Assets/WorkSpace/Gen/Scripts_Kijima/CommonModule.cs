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
}
