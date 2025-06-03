using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static GameManager;
using static UnityEngine.GraphicsBuffer;
using static CommonModule;

public abstract class Item : MonoBehaviour{
    public float baseSpeed { get; private set;} = 100f;      // ��]�̊���x
    public float amplitude { get; private set;} = 30f;      // �X�s�[�h�h��̑傫��
    public float frequency { get; private set;} = 1f;       // �X�s�[�h�̗h��̑���

    public float fadeDuration = 2f;
    private Renderer rend;
    private Material[] materials;
    [SerializeField] public GameObject ownEffect;
    [SerializeField] private Vector3 offset = Vector3.zero;  // �G�t�F�N�g�̃I�t�Z�b�g�i�ʒu�����p�j
    

    private void Start() {
        Initialize();
        rend = GetComponent<Renderer>();
        materials = rend.materials; // �S�}�e���A���擾

        foreach (var mat in materials) {
            SetupMaterialForFade(mat);
        }

        FadeOut(fadeDuration).Forget(); // UniTask���N��
        
        
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
            AdminEffect playerScript = collision.gameObject.GetComponent<AdminEffect>();  //�Ԃ���������̃v���C���[�X�N���v�g�����

            if (this.gameObject.name == "NoStanPrefab(Clone)") {
                playerScript.SetNoStanCoin(); 
                if (!CheckHasChild("NoStanEffect(Clone)",collision.transform)) {
                    GameObject instance = Instantiate(ownEffect, collision.gameObject.transform.position + offset, Quaternion.Euler(-90,0,0));
                    instance.transform.parent = collision.gameObject.transform;  // �e��ݒ肵�Ďq�I�u�W�F�N�g�ɂ���
                }
            }
            if (this.gameObject.name == "SpeedUpPrefab(Clone)") {
                playerScript.SetSpeedUpItem();

                if (!CheckHasChild("SpeedUpEffect(Clone)",collision.transform)) {
                    GameObject instance = Instantiate(ownEffect, collision.gameObject.transform.position + offset, Quaternion.Euler(-90,0,0));
                    instance.transform.parent = collision.gameObject.transform;  // �e��ݒ肵�Ďq�I�u�W�F�N�g�ɂ���
                }
            }

            Destroy(gameObject);
        }

    }
}
