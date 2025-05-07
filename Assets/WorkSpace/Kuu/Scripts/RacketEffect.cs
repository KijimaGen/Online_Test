using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketEffect : MonoBehaviour
{
    [SerializeField]
    [Tooltip("発生させるエフェクト(パーティクル)")]
    public ParticleSystem particle;
    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// プレイヤーに衝突した時
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerStay(Collider collision) {
        // 当たった相手が"Player"タグを持っていたら
        if (collision.gameObject.tag == "Shuttle") {
            if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.JoystickButton1) == true) {
                // パーティクルシステムのインスタンスを生成する。
                ParticleSystem newParticle = Instantiate(particle);
                // パーティクルの発生場所をこのスクリプトをアタッチしているGameObjectの場所にする。
                newParticle.transform.position = this.transform.position;
                // パーティクルを発生させる。
                newParticle.Play();
                // インスタンス化したパーティクルシステムのGameObjectを5秒後に削除する。(任意)
                // ※第一引数をnewParticleだけにするとコンポーネントしか削除されない。
                Destroy(newParticle.gameObject, 5.0f);
            }
        }
    }
}
