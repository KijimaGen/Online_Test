using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MoveTest : MonoBehaviour{
    [SerializeField] float speedUpTime = 0;
    [SerializeField] float noStanTime = 0;
    const float effectMaxTime = 10;


    //�ȈՓI�ȃv���C���[����
    private void Update() {
        if(speedUpTime > 0) speedUpTime-=Time.deltaTime;
        if (noStanTime > 0) noStanTime -=Time.deltaTime;

        //���ʂ���
        if (noStanTime <= 0) {
            if(CheckHasChild("NoStanEffect(Clone)"))
            RemoveChildByName("NoStanEffect(Clone)");
        }
        if (speedUpTime <= 0) {
            if(CheckHasChild("SpeedUpEffect(Clone)"))
            RemoveChildByName("SpeedUpEffect(Clone)");
        }

        Vector3 move = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {move.x--;}
        if (Input.GetKeyDown(KeyCode.RightArrow)) { move.x++;}
        if (Input.GetKeyDown(KeyCode.UpArrow)) { move.z++; }
        if (Input.GetKeyDown(KeyCode.DownArrow)) { move.z--; }

        if (speedUpTime > 0) move *= 2;

        transform.position += move;
    }

    public void SetNoStanCoin() {
        noStanTime = 10.0f;
    }
    public void SetSpeedUpItem() {
        speedUpTime = 10.0f;
    }

    public bool CheckHasChild(string name) {
        foreach (Transform child in transform)  // ���̃I�u�W�F�N�g�̂��ׂĂ̎q�I�u�W�F�N�g�����[�v
        {
            if (child.gameObject.name == name) { // �q�̖��O����v���邩�m�F
                return true;
            }
        }
        return false;
    }
    public void RemoveChildByName(string name) {
        // �e�I�u�W�F�N�g�̂��ׂĂ̎q�I�u�W�F�N�g�����[�v
        foreach (Transform child in transform) {
            if (child.gameObject.name == name) {  // ���O����v����ꍇ
                // �q�I�u�W�F�N�g���폜
                Destroy(child.gameObject);
                return;  // �ŏ��Ɍ������q�I�u�W�F�N�g���폜������I��
            }
        }
    }
}
