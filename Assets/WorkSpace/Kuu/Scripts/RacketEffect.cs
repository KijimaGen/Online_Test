using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketEffect : MonoBehaviour
{
    [SerializeField]
    [Tooltip("発生させるエフェクト(パーティクル)")]
    public ParticleSystem particle;

    private bool buttonFlag;

    private void OnTriggerExit(Collider collision) {
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
