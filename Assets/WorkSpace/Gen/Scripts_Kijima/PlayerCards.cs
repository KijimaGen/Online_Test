using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static CommonModule;

public class PlayerCards : MonoBehaviour{
    [SerializeField]
    GameObject[] PlayerCard;
    [SerializeField]
    RawImage[] PlayerTextureRender;
    const int maxCards = 4;
    public static PlayerCards Instance { get; private set; }

    private void Start() {
        Instance = this;
    }

    public void AddPlayer(Camera cam) {
        //左から順番に確認して、順番にカードの見た目のオフからオンを切り替えていく処理
        for (int i = 0; i < PlayerCard.Length; i++) {
            if (PlayerCard[i].gameObject.activeSelf == false) {
                PlayerCard[i].SetActive(true);
                // 子オブジェクト "MyChild" を取得
                Transform child = transform.Find("Camera");

                // SpriteRenderer を取得して色を変更
                SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
                PlayerCard[i].transform.Find("");
                break;
            }
        }
    }
}
