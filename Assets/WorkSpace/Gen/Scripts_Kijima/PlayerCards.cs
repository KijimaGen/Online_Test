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
        //�����珇�ԂɊm�F���āA���ԂɃJ�[�h�̌����ڂ̃I�t����I����؂�ւ��Ă�������
        for (int i = 0; i < PlayerCard.Length; i++) {
            if (PlayerCard[i].gameObject.activeSelf == false) {
                PlayerCard[i].SetActive(true);
                // �q�I�u�W�F�N�g "MyChild" ���擾
                Transform child = transform.Find("Camera");

                // SpriteRenderer ���擾���ĐF��ύX
                SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
                PlayerCard[i].transform.Find("");
                break;
            }
        }
    }
}
