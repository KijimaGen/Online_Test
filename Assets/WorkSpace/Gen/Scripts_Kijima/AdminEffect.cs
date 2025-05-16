// ���g�̃G�t�F�N�g�Ǘ��X�N���v�g
// 2025�N5��12��
// kijima
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CommonModule;

public class AdminEffect : MonoBehaviour{
    [SerializeField] float speedUpTime = 0;
    [SerializeField] float noStanTime = 0;
    const float effectMaxTime = 10;

    private void Update() {
        //���ʎ��Ԃ����炷
        if (speedUpTime > 0) speedUpTime -= Time.deltaTime;
        if (noStanTime > 0) noStanTime -= Time.deltaTime;

        //���ʂ���
        if (noStanTime <= 0 || GameManager.instance.state != GameManager.gameState.start) {
            if (CheckHasChild("NoStanEffect(Clone)", this.transform))
                RemoveChildByName("NoStanEffect(Clone)",this.transform);
        }
        if (speedUpTime <= 0 || GameManager.instance.state != GameManager.gameState.start) {
            if (CheckHasChild("SpeedUpEffect(Clone)", this.transform))
                RemoveChildByName("SpeedUpEffect(Clone)", this.transform);
        }
    }
    
    //���̃X�N���v�g����Ăׂ�A���ʎ��Ԃ��Z�b�g����֐�����(�d�����Ȃ����̊Ǘ��́A�A�C�e���̊��N���X�ɔC���Ă�)
    public void SetNoStanCoin() {
        noStanTime = effectMaxTime;
    }
    public void SetSpeedUpItem() {
        speedUpTime = effectMaxTime;
    }
}
