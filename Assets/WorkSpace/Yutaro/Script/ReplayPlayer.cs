using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayPlayer : MonoBehaviour
{
    public List<RecordData> recordDatas;
    private float timer = 0f;
    private int currentIndex = 0;

    void Update()
    {
        if (recordDatas == null || recordDatas.Count == 0)
        {
            gameObject.SetActive(false);
            return;
        }

        timer += Time.deltaTime;

        while (currentIndex < recordDatas.Count && recordDatas[currentIndex].time <= timer)
        {
            transform.position = recordDatas[currentIndex].position;
            transform.rotation = recordDatas[currentIndex].rotation;
            currentIndex++;
        }

        if (currentIndex >= recordDatas.Count)
        {
            ReplayManager.instance.EndReplay();
            gameObject.SetActive(false);
        }

    }

    // ���Z�b�g�p���\�b�h
    public void ResetReplay()
    {
        //timer = 0f;
        currentIndex = 0;
        gameObject.SetActive(true);
    }

    // �������p���\�b�h
    public void Initialize(List<RecordData> newDatas)
    {
        // �ߋ��̃f�[�^���N���A���āA�V�����f�[�^���Z�b�g
        recordDatas.Clear();  // �ȑO�̃f�[�^������
        recordDatas.AddRange(newDatas); // �V�����f�[�^��ǉ�

        ResetReplay();  // �^�C�}�[�ƃC���f�b�N�X�����Z�b�g
    }

}
