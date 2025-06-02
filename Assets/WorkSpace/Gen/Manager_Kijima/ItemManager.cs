using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;
using static UnityEngine.GraphicsBuffer;

public class ItemManager : MonoBehaviour {

    [SerializeField]List<GameObject> items = new List<GameObject>(4);
    [SerializeField]int canSpawn;
    public static bool isStopped;
    Vector3 center;
    float radius = 7;
    [SerializeField]float speed = 1;
    float time = 0f;
    


    void Start () {
        center = transform.position;
        isStopped = false;               //���ケ�����~�߂Ȃ��Ƃ����Ȃ��Ȃ邩������Ȃ�����u���Ă���
        CreateItem();
    }

    async void Update() {
        if (GameManager.instance.state != GameManager.gameState.start)
            isStopped = true;

        else
            isStopped = false;
        if (!isStopped) { 
            await CalcPosition();
            
        }

        if(SceneManager.GetActiveScene().name == "ChutorialScene" && Input.GetKeyDown(KeyCode.JoystickButton6)) { //�Q�[���V�[�����`���[�g���A���ŏ�����Back�{�^���ŃA�C�e�����P�X
            Destroy(this.gameObject);
        }
    }

    private async UniTask CreateItem() {
        while (true) {
            await UniTask.Delay(Random.Range(500, 2500));  //�����ŗ����ő҂����Ԃ���邱�ƂŃ����_������}��
            if (!this || !gameObject) return;
            if (!isStopped) {
                canSpawn = Random.Range(0, 10); //�����𐶐�
                if (canSpawn < 5) {
                    if (!this || !gameObject) return;
                    Instantiate(items[Random.Range(0, items.Count)], transform.position, Quaternion.identity); //�A�C�e���̐���
                    SoundManager.Instance.PlaySound(0);
                }
            }
        }
    }

    private async UniTask CalcPosition() {
            time += Time.deltaTime;

            // �p�x�����Ԃŕω�������
            float angle = time * speed * Mathf.PI * 2f; // ���W�A���p

            // x = r * cos(��), z = r * sin(��)
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            // �ʒu���X�V�iX-Y���ʂŉ~�^���j
            transform.position = center + new Vector3(x, 0, z);

            await UniTask.Delay(1);
        
    }

    
}