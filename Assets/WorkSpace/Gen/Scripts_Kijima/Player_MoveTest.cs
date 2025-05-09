using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MoveTest : MonoBehaviour{
    [SerializeField] float speedUpTime = 0;
    [SerializeField] float noStanTime = 0;
    const float effectMaxTime = 10;


    //簡易的なプレイヤー操作
    private void Update() {
        if(speedUpTime > 0) speedUpTime-=Time.deltaTime;
        if (noStanTime > 0) noStanTime -=Time.deltaTime;

        //効果けし
        if (noStanTime <= 0) {
            if(CheckHasChild("NoStanEffect(Clone)"))
            RemoveChildByName("NoStanEffect(Clone)");
        }
        if (speedUpTime <= 0) {
            if(CheckHasChild("SpeedUpEffect(Clone)"))
            RemoveChildByName("SpeedUpEffect(Clone)");
        }

        Vector3 move = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {move.x--;}
        if (Input.GetKeyDown(KeyCode.RightArrow)) { move.x++;}
        if (Input.GetKeyDown(KeyCode.UpArrow)) { move.z++; }
        if (Input.GetKeyDown(KeyCode.DownArrow)) { move.z--; }

        if (speedUpTime > 0) move *= 2;

        transform.position += move;
    }

    public void SetNoStanCoin() {
        noStanTime = 10.0f;
    }
    public void SetSpeedUpItem() {
        speedUpTime = 10.0f;
    }

    public bool CheckHasChild(string name) {
        foreach (Transform child in transform)  // このオブジェクトのすべての子オブジェクトをループ
        {
            if (child.gameObject.name == name) { // 子の名前が一致するか確認
                return true;
            }
        }
        return false;
    }
    public void RemoveChildByName(string name) {
        // 親オブジェクトのすべての子オブジェクトをループ
        foreach (Transform child in transform) {
            if (child.gameObject.name == name) {  // 名前が一致する場合
                // 子オブジェクトを削除
                Destroy(child.gameObject);
                return;  // 最初に見つけた子オブジェクトを削除したら終了
            }
        }
    }
}
