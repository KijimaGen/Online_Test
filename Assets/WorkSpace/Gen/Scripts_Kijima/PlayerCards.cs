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
    RenderTexture[] PlayerTextureRender;
    const int maxCards = 4;
    public static PlayerCards Instance { get; private set; }

    int PlayerNumberIndex = 0;

    private void Start() {
        Instance = this;
    }
    private void Update() {
        //ゲーム中、スタンバイ中は描画
        if (GameManager.instance.state == GameManager.gameState.start || GameManager.instance.state == GameManager.gameState.standBy) {
            for (int i = 0; i < PlayerNumberIndex; i++) {
                PlayerCard[i].SetActive(true);
            }
        }
        else {
            for (int i = 0; i < PlayerCard.Length; i++) {
                PlayerCard[i].SetActive(false);
            }
        }
    }

    public void AddPlayer(GameObject _player) {
        //左から順番に確認して、順番にカードの見た目のオフからオンを切り替えていく処理
        for (int i = 0; i < PlayerCard.Length; i++) {
            if (PlayerCard[i].gameObject.activeSelf == false) {
                PlayerCard[i].SetActive(true);
                // 子オブジェクト "Camera" を取得
                Transform child = transform.Find("Camera");
                PlayerCard[i].GetComponent<Cards>().ConnectPlayer(_player);
                
               
                break;
            }
        }
    }

    public RenderTexture GetPlayerIcon() {
        if (PlayerNumberIndex >= maxCards) {
            
            return null;
        }

        PlayerNumberIndex++;
       return PlayerTextureRender[PlayerNumberIndex-1];
        
    }
}
