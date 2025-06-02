using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static GameManager;
using static UnityEngine.GraphicsBuffer;
using static CommonModule;

public abstract class Item : MonoBehaviour{
    public float baseSpeed { get; private set;} = 100f;      // 回転の基準速度
    public float amplitude { get; private set;} = 30f;      // スピード揺れの大きさ
    public float frequency { get; private set;} = 1f;       // スピードの揺れの速さ

    public float fadeDuration = 2f;
    private Renderer rend;
    private Material[] materials;
    [SerializeField] public GameObject ownEffect;
    [SerializeField] private Vector3 offset = Vector3.zero;  // エフェクトのオフセット（位置調整用）
    

    private void Start() {
        Initialize();
        rend = GetComponent<Renderer>();
        materials = rend.materials; // 全マテリアル取得

        foreach (var mat in materials) {
            SetupMaterialForFade(mat);
        }

        FadeOut(fadeDuration).Forget(); // UniTaskを起動
        
        
    }

    private void Update() {
        if(GameManager.instance.state != GameManager.gameState.start) {
            Destroy(gameObject);
        }
    }

    private void SetupMaterialForFade(Material mat) {
        mat.SetFloat("_Mode", 3);
        mat.SetInt("_SrcBlend", (int) UnityEngine.Rendering.BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend", (int) UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.EnableKeyword("_ALPHABLEND_ON");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = 3000;
    }

    private async UniTask FadeOut(float time) {
        float t = 0f;

        await UniTask.Delay(10000);

        if (!this || !gameObject) return;

        while (t < 1f) {
            if (!this || !gameObject) return;

            t += Time.deltaTime / time;
            foreach (var mat in materials) {
                Color color = mat.color;
                color.a = Mathf.Lerp(1f, 0f, t);
                mat.color = color;
            }
            await UniTask.Yield();
        }
        if (this.gameObject != null)
            Destroy(gameObject);
    }
    public abstract void Initialize();

    
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "RedTeam" || collision.gameObject.tag == "WhiteTeam") {
            AdminEffect playerScript = collision.gameObject.GetComponent<AdminEffect>();  //ぶつかった相手のプレイヤースクリプトを入手

            if (this.gameObject.name == "NoStanPrefab(Clone)") {
                playerScript.SetNoStanCoin(); 
                if (!CheckHasChild("NoStanEffect(Clone)",collision.transform)) {
                    GameObject instance = Instantiate(ownEffect, collision.gameObject.transform.position + offset, Quaternion.Euler(-90,0,0));
                    instance.transform.parent = collision.gameObject.transform;  // 親を設定して子オブジェクトにする
                }
            }
            if (this.gameObject.name == "SpeedUpPrefab(Clone)") {
                playerScript.SetSpeedUpItem();

                if (!CheckHasChild("SpeedUpEffect(Clone)",collision.transform)) {
                    GameObject instance = Instantiate(ownEffect, collision.gameObject.transform.position + offset, Quaternion.Euler(-90,0,0));
                    instance.transform.parent = collision.gameObject.transform;  // 親を設定して子オブジェクトにする
                }
            }

            Destroy(gameObject);
        }

    }
}
