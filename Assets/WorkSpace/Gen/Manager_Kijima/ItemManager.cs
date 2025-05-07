using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

    [SerializeField]List<GameObject> items = new List<GameObject>(4);
    [SerializeField]int canSpawn;
    public static bool isStopped;
    Vector3 center;
    float radius = 10;
    [SerializeField]float speed = 1;
     


    void Start () {
        center = transform.position;
        isStopped = true;               //今後こいつを止めないといけなくなるかもしれないから置いておく
        CreateItem();
        CalcPosition();
    }

    private async UniTask CreateItem() {
        while (isStopped) {
            await UniTask.Delay(Random.Range(500, 5000));  //ここで乱数で待ち時間を取ることでランダム化を図る
            //await UniTask.Delay(100);
            canSpawn = Random.Range(0, 10); //乱数を生成
            if (canSpawn < 11) {
                Instantiate(items[Random.Range(0,items.Count)], transform.position, Quaternion.identity); //アイテムの生成
                SoundManager.Instance.PlaySoud(0);
            }
        }
    }

    private async UniTask CalcPosition() {
        float time = 0f;

        while (isStopped) {
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

    public static void StoppSpawn() {
        isStopped = false;
    }
}