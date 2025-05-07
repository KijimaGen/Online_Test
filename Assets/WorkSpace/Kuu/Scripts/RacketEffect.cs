using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketEffect : MonoBehaviour
{
    [SerializeField]
    [Tooltip("����������G�t�F�N�g(�p�[�e�B�N��)")]
    public ParticleSystem particle;
    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// �v���C���[�ɏՓ˂�����
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerStay(Collider collision) {
        // �����������肪"Player"�^�O�������Ă�����
        if (collision.gameObject.tag == "Shuttle") {
            if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.JoystickButton1) == true) {
                // �p�[�e�B�N���V�X�e���̃C���X�^���X�𐶐�����B
                ParticleSystem newParticle = Instantiate(particle);
                // �p�[�e�B�N���̔����ꏊ�����̃X�N���v�g���A�^�b�`���Ă���GameObject�̏ꏊ�ɂ���B
                newParticle.transform.position = this.transform.position;
                // �p�[�e�B�N���𔭐�������B
                newParticle.Play();
                // �C���X�^���X�������p�[�e�B�N���V�X�e����GameObject��5�b��ɍ폜����B(�C��)
                // ����������newParticle�����ɂ���ƃR���|�[�l���g�����폜����Ȃ��B
                Destroy(newParticle.gameObject, 5.0f);
            }
        }
    }
}
