/**
 * @file KnockBack.cs
 * @brief ノックバック管理クラス
 * @author kijima
 * @date 2025/5/16
 */
using Cysharp.Threading.Tasks;
using UnityEngine;

public class KnockBack : MonoBehaviour{
    [SerializeField]
    private float knockBackPower = 10.0f;
    Rigidbody rb;
    int index;
    public bool attack;
    private float chargePower = 0;

    
    private void Start() {
        rb = GetComponent<Rigidbody>();
        index = GameManager.instance.playerIndex;
    }

    //もうめんどくさいのでこっちで攻撃判定を取っておく
    private async void Update() {
        //溜めボタン入力
        if(Input.GetKey("joystick " + index + " button 1") && transform.tag != "Player"){
            chargePower += Time.deltaTime;
        }
        else {
            if (chargePower > 0) {
                chargePower = 0;
                await UniTask.Delay(100);
                attack = true;
                ChangeAttack();
            }
        }
    }
    private void OnCollisionStay(Collision collision) {
        //他のプレイヤーが当たったとき
        if(collision.gameObject.tag == "RedTeam" || collision.gameObject.tag == "WhiteTeam") {
            //親オブジェクトと自分のチームが同じならノックバックしない
            if (this.gameObject.tag == collision.gameObject.tag) return;
            if (!collision.gameObject.GetComponent<KnockBack>().GetAttack()) return;

            //現在の位置と当たったものの反転ベクトルを作成(デフォルトの値が低すぎるのでここで大きくしておく)
            Vector3 knockBackVector = (- 1 *(transform.position- collision.transform.position)) * 2 * chargePower / 10;
            //縦方向のベクトルは固定でヨシ
            knockBackVector.y = 0.5f;
            rb.AddForce(knockBackVector * knockBackPower, ForceMode.Impulse);

        }
    }

    private async UniTask ChangeAttack() {
        await UniTask.Delay(100);
        attack = false;
    }

    public bool GetAttack() {
        return attack;
    }
}
