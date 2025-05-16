/**
 * @file KnockBack.cs
 * @brief �m�b�N�o�b�N�Ǘ��N���X
 * @author kijima
 * @date 2025/5/16
 */
using Cysharp.Threading.Tasks;
using UnityEngine;

public class KnockBack : MonoBehaviour{
    [SerializeField]
    private float knockBackPower = 10.0f;
    Rigidbody rb;
    int index;
    public bool attack;
    private float chargePower = 0;

    
    private void Start() {
        rb = GetComponent<Rigidbody>();
        index = GameManager.instance.playerIndex;
    }

    //�����߂�ǂ������̂ł������ōU�����������Ă���
    private async void Update() {
        //���߃{�^������
        if(Input.GetKey("joystick " + index + " button 1") && transform.tag != "Player"){
            chargePower += Time.deltaTime;
        }
        else {
            if (chargePower > 0) {
                chargePower = 0;
                await UniTask.Delay(100);
                attack = true;
                ChangeAttack();
            }
        }
    }
    private void OnCollisionStay(Collision collision) {
        //���̃v���C���[�����������Ƃ�
        if(collision.gameObject.tag == "RedTeam" || collision.gameObject.tag == "WhiteTeam") {
            //�e�I�u�W�F�N�g�Ǝ����̃`�[���������Ȃ�m�b�N�o�b�N���Ȃ�
            if (this.gameObject.tag == collision.gameObject.tag) return;
            if (!collision.gameObject.GetComponent<KnockBack>().GetAttack()) return;

            //���݂̈ʒu�Ɠ����������̂̔��]�x�N�g�����쐬(�f�t�H���g�̒l���Ⴗ����̂ł����ő傫�����Ă���)
            Vector3 knockBackVector = (- 1 *(transform.position- collision.transform.position)) * 2 * chargePower / 10;
            //�c�����̃x�N�g���͌Œ�Ń��V
            knockBackVector.y = 0.5f;
            rb.AddForce(knockBackVector * knockBackPower, ForceMode.Impulse);

        }
    }

    private async UniTask ChangeAttack() {
        await UniTask.Delay(100);
        attack = false;
    }

    public bool GetAttack() {
        return attack;
    }
}
