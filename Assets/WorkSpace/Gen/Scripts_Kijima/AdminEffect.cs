// 自身のエフェクト管理スクリプト
// 2025年5月12日
// kijima
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CommonModule;

public class AdminEffect : MonoBehaviour{
    [SerializeField] float speedUpTime = 0;
    [SerializeField] float noStanTime = 0;
    const float effectMaxTime = 10;

    private void Update() {
        //効果時間を減らす
        if (speedUpTime > 0) speedUpTime -= Time.deltaTime;
        if (noStanTime > 0) noStanTime -= Time.deltaTime;

        //効果けし
        if (noStanTime <= 0 || GameManager.instance.state != GameManager.gameState.start) {
            if (CheckHasChild("NoStanEffect(Clone)", this.transform))
                RemoveChildByName("NoStanEffect(Clone)",this.transform);
        }
        if (speedUpTime <= 0 || GameManager.instance.state != GameManager.gameState.start) {
            if (CheckHasChild("SpeedUpEffect(Clone)", this.transform))
                RemoveChildByName("SpeedUpEffect(Clone)", this.transform);
        }
    }
    
    //他のスクリプトから呼べる、効果時間をセットする関数たち(重複しないかの管理は、アイテムの基底クラスに任せてる)
    public void SetNoStanCoin() {
        noStanTime = effectMaxTime;
    }
    public void SetSpeedUpItem() {
        speedUpTime = effectMaxTime;
    }
}
