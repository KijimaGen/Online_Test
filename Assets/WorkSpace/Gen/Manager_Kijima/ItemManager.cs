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
        isStopped = false;               //今後こいつを止めないといけなくなるかもしれないから置いておく
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

        if(SceneManager.GetActiveScene().name == "ChutorialScene" && Input.GetKeyDown(KeyCode.JoystickButton6)) { //ゲームシーンがチュートリアルで尚且つBackボタンでアイテムをケス
            Destroy(this.gameObject);
        }
    }

    private async UniTask CreateItem() {
        while (true) {
            await UniTask.Delay(Random.Range(500, 2500));  //ここで乱数で待ち時間を取ることでランダム化を図る
            if (!this || !gameObject) return;
            if (!isStopped) {
                canSpawn = Random.Range(0, 10); //乱数を生成
                if (canSpawn < 5) {
                    if (!this || !gameObject) return;
                    Instantiate(items[Random.Range(0, items.Count)], transform.position, Quaternion.identity); //アイテムの生成
                    SoundManager.Instance.PlaySound(0);
                }
            }
        }
    }

    private async UniTask CalcPosition() {
            time += Time.deltaTime;

            // 角度を時間で変化させる
            float angle = time * speed * Mathf.PI * 2f; // ラジアン角

            // x = r * cos(θ), z = r * sin(θ)
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            // 位置を更新（X-Y平面で円運動）
            transform.position = center + new Vector3(x, 0, z);

            await UniTask.Delay(1);
        
    }

    
}