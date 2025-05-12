using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CommonModule;

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
            if(CheckHasChild("NoStanEffect(Clone)",this.transform))
            RemoveChildByName("NoStanEffect(Clone)", this.transform);
        }
        if (speedUpTime <= 0) {
            if(CheckHasChild("SpeedUpEffect(Clone)",this.transform))
            RemoveChildByName("SpeedUpEffect(Clone)", this.transform);
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

    
    
}
