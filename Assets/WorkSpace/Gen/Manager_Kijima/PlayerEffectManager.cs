using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectManager : MonoBehaviour {
    // 追加するエフェクトのPrefabをInspectorから指定
    [SerializeField] public static List<GameObject> effects = new List<GameObject>(4);

    // 親にしたいオブジェクト（このスクリプトを付けたオブジェクトを親にする場合は不要）
    [SerializeField] public static Transform parentObject;

    // スポーン位置のオフセット
    [SerializeField] public static Vector3 offset = Vector3.zero;

    // スタート時に自動で生成したい場合
    void Start() {
        SpawnEffect("NoStanCoin");
    }

    // 外部から呼び出せるメソッド
    public void SpawnEffect(string ItemName) {

        //こいつは仮置き、いつか名前を引数で受け取って、直感的にどのエフェクトを呼び出すのかを設定できるようにしたい
        int index = 0;
        switch (ItemName) {
            case "NoStanCoin":
                index = 0; 
                break;
        }


        if (effects == null) {
            Debug.LogWarning("Effect prefab is not assigned.");
            return;
        }

        Transform parent = parentObject;

        GameObject effectInstance = Instantiate(effects[index], parent);
        effectInstance.transform.localPosition = offset;
    }
}
