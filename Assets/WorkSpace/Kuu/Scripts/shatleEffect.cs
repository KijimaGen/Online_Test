using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shatleEffect : MonoBehaviour
{
    [SerializeField]
    [Tooltip("����������G�t�F�N�g(�p�[�e�B�N��)")]
    public ParticleSystem particle1;

    [SerializeField]
    [Tooltip("����������G�t�F�N�g(�p�[�e�B�N��)")]
    public ParticleSystem particle2;

    [SerializeField]
    [Tooltip("����������G�t�F�N�g(�p�[�e�B�N��)")]
    public ParticleSystem particle3;

    void Update()
    {
        /*
        //OnMove();

        //if (Input.GetKey("joystick " + index + " button 1") || Input.GetKey(KeyCode.Space))
        if (Input.GetKeyDown(KeyCode.JoystickButton0) == true)
        {
            // �p�[�e�B�N���V�X�e���̃C���X�^���X�𐶐�����B
            ParticleSystem newParticle = Instantiate(particle1);
            // �p�[�e�B�N���̔����ꏊ�����̃X�N���v�g���A�^�b�`���Ă���GameObject�̏ꏊ�ɂ���B
            newParticle.transform.position = this.transform.position;
            // �p�[�e�B�N���𔭐�������B
            newParticle.Play();
            // �C���X�^���X�������p�[�e�B�N���V�X�e����GameObject��3�b��ɍ폜����B
            // ����������newParticle�����ɂ���ƃR���|�[�l���g�����폜����Ȃ��B
            Destroy(newParticle.gameObject, 3.0f);
        }*/
    }
    
    /// <summary>
    /// ���ɏՓ˂�����
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionEnter(Collision collision) {
        // �`���[�g���A���̓G�t�F�N�g�����Ȃ�
        if (!TutorialRule.tutorial) {
            // �����������肪����or�ԏ���������
            if (collision.gameObject.name == "����" || collision.gameObject.name == "�ԏ�") {
                // �p�[�e�B�N���V�X�e���̃C���X�^���X�𐶐�����B
                ParticleSystem newParticle = Instantiate(particle1);
                // �p�[�e�B�N���̔����ꏊ�����̃X�N���v�g���A�^�b�`���Ă���GameObject�̏ꏊ�ɂ���B
                newParticle.transform.position = this.transform.position;
                // �p�[�e�B�N���𔭐�������B
                newParticle.Play();
                // �C���X�^���X�������p�[�e�B�N���V�X�e����GameObject��3�b��ɍ폜����B
                // ����������newParticle�����ɂ���ƃR���|�[�l���g�����폜����Ȃ��B
                Destroy(newParticle.gameObject, 3.0f);
            }
            // �A�E�g���C���ɓ���������
            if (collision.gameObject.name == "outCourt") {
                // �p�[�e�B�N���V�X�e���̃C���X�^���X�𐶐�����B
                ParticleSystem newParticle = Instantiate(particle2);
                // �p�[�e�B�N���̔����ꏊ�����̃X�N���v�g���A�^�b�`���Ă���GameObject�̏ꏊ�ɂ���B
                newParticle.transform.position = this.transform.position;
                // �p�[�e�B�N���𔭐�������B
                newParticle.Play();
                // �C���X�^���X�������p�[�e�B�N���V�X�e����GameObject��1.5�b��ɍ폜����B
                // ����������newParticle�����ɂ���ƃR���|�[�l���g�����폜����Ȃ��B
                Destroy(newParticle.gameObject, 2.0f);
            }
        }
    }
    
    public void ShotEffect() {
        // �p�[�e�B�N���V�X�e���̃C���X�^���X�𐶐�����B
        ParticleSystem newParticle = Instantiate(particle3);
        // �p�[�e�B�N���̔����ꏊ�����̃X�N���v�g���A�^�b�`���Ă���GameObject�̏ꏊ�ɂ���B
        newParticle.transform.position = this.transform.position;
        // �p�[�e�B�N���𔭐�������B
        newParticle.Play();
        // �C���X�^���X�������p�[�e�B�N���V�X�e����GameObject��1�b��ɍ폜����B
        // ����������newParticle�����ɂ���ƃR���|�[�l���g�����폜����Ȃ��B
        Destroy(newParticle.gameObject, 1f);
    }
}
