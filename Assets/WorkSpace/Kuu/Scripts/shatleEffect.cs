using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shatleEffect : MonoBehaviour
{
    [SerializeField]
    [Tooltip("発生させるエフェクト(パーティクル)")]
    public ParticleSystem particle1;

    [SerializeField]
    [Tooltip("発生させるエフェクト(パーティクル)")]
    public ParticleSystem particle2;

    void Update()
    {
        /*
        //OnMove();

        //if (Input.GetKey("joystick " + index + " button 1") || Input.GetKey(KeyCode.Space))
        if (Input.GetKeyDown(KeyCode.JoystickButton0) == true)
        {
            // パーティクルシステムのインスタンスを生成する。
            ParticleSystem newParticle = Instantiate(particle1);
            // パーティクルの発生場所をこのスクリプトをアタッチしているGameObjectの場所にする。
            newParticle.transform.position = this.transform.position;
            // パーティクルを発生させる。
            newParticle.Play();
            // インスタンス化したパーティクルシステムのGameObjectを3秒後に削除する。
            // ※第一引数をnewParticleだけにするとコンポーネントしか削除されない。
            Destroy(newParticle.gameObject, 3.0f);
        }*/
    }
    
    /// <summary>
    /// 床に衝突した時
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionEnter(Collision collision) {
        // 当たった相手がcourtだったら
        if (collision.gameObject.name == "白床" || collision.gameObject.name == "赤床") {
            // パーティクルシステムのインスタンスを生成する。
            ParticleSystem newParticle = Instantiate(particle1);
            // パーティクルの発生場所をこのスクリプトをアタッチしているGameObjectの場所にする。
            newParticle.transform.position = this.transform.position;
            // パーティクルを発生させる。
            newParticle.Play();
            // インスタンス化したパーティクルシステムのGameObjectを5秒後に削除する。(任意)
            // ※第一引数をnewParticleだけにするとコンポーネントしか削除されない。
            Destroy(newParticle.gameObject, 3.0f);
        }
    }
    
    public void ShotEffect() {
        // パーティクルシステムのインスタンスを生成する。
        ParticleSystem newParticle = Instantiate(particle2);
        // パーティクルの発生場所をこのスクリプトをアタッチしているGameObjectの場所にする。
        newParticle.transform.position = this.transform.position;
        // パーティクルを発生させる。
        newParticle.Play();
        // インスタンス化したパーティクルシステムのGameObjectを5秒後に削除する。(任意)
        // ※第一引数をnewParticleだけにするとコンポーネントしか削除されない。
        Destroy(newParticle.gameObject, 3.0f);
    }
}
