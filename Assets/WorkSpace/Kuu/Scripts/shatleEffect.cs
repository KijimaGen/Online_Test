using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shatleEffect : MonoBehaviour
{
    [SerializeField]
    [Tooltip("発生させるエフェクト(パーティクル)")]
    public ParticleSystem particle;

    /*
    void Update()
    {
        
        //OnMove();

        //if (Input.GetKey("joystick " + index + " button 1") || Input.GetKey(KeyCode.Space))
        if (Input.GetKey(KeyCode.Space))
        {
            // パーティクルシステムのインスタンスを生成する。
            ParticleSystem newParticle = Instantiate(particle);
            // パーティクルの発生場所をこのスクリプトをアタッチしているGameObjectの場所にする。
            newParticle.transform.position = this.transform.position;
            // パーティクルを発生させる。
            newParticle.Play();
            // インスタンス化したパーティクルシステムのGameObjectを5秒後に削除する。(任意)
            // ※第一引数をnewParticleだけにするとコンポーネントしか削除されない。
            Destroy(newParticle.gameObject, 0.1f);
        }
    }
    */
    /// <summary>
    /// 衝突した時
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerEnter(Collider collision)
    {
        // 当たった相手が"Player"タグを持っていたら
        if (collision.gameObject.tag == "Player")
        {
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
