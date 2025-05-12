using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketEffect : MonoBehaviour
{
    [SerializeField]
    [Tooltip("発生させるエフェクト(パーティクル)")]
    public ParticleSystem particle;

    private bool buttonFlag;
    // Update is called once per frame
    void Update()
    {
        /*if (GetComponent<PlayerK>().GetAnimPlay()) {
            buttonFlag = true;
            //Debug.Log("buttonFlag;true");
        } else {
            buttonFlag = false;
        }*/

        // 使えないらしい
        /*if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) {
            Debug.Log("アニメーション");
        }*/
    }

    //メモ;OnTriggerExit,打った時にフラグを受け取る

    /// <summary>
    /// プレイヤーに衝突した時
    /// </summary>
    /// <param name="collision"></param>
    /*public void OnTriggerExit(Collider collision) {
        // 当たった相手が"Player"タグを持っていたら
        if (collision.gameObject.tag == "Racket") {
            //Debug.Log("当たってまーーす");
            //if (buttonFlag) {
                // パーティクルシステムのインスタンスを生成する。
                ParticleSystem newParticle = Instantiate(particle);
                // パーティクルの発生場所をこのスクリプトをアタッチしているGameObjectの場所にする。
                newParticle.transform.position = this.transform.position;
                // パーティクルを発生させる。
                newParticle.Play();
                // インスタンス化したパーティクルシステムのGameObjectを5秒後に削除する。(任意)
                // ※第一引数をnewParticleだけにするとコンポーネントしか削除されない。
                Destroy(newParticle.gameObject, 5.0f);
            //}
        }
    }*/

    private void OnTriggerStay(Collider collision) {
        if (collision.gameObject.tag == "Racket") {
            if (buttonFlag) {
                // パーティクルシステムのインスタンスを生成する。
                ParticleSystem newParticle = Instantiate(particle);
            // パーティクルの発生場所をこのスクリプトをアタッチしているGameObjectの場所にする。
            newParticle.transform.position = this.transform.position;
            // パーティクルを発生させる。
            newParticle.Play();
            // インスタンス化したパーティクルシステムのGameObjectを5秒後に削除する。
            // ※第一引数をnewParticleだけにするとコンポーネントしか削除されない。
            Destroy(newParticle.gameObject, 5.0f);
            }
        }
    }
}
