using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectManager : MonoBehaviour {
    // �ǉ�����G�t�F�N�g��Prefab��Inspector����w��
    [SerializeField] public static List<GameObject> effects = new List<GameObject>(4);

    // �e�ɂ������I�u�W�F�N�g�i���̃X�N���v�g��t�����I�u�W�F�N�g��e�ɂ���ꍇ�͕s�v�j
    [SerializeField] public static Transform parentObject;

    // �X�|�[���ʒu�̃I�t�Z�b�g
    [SerializeField] public static Vector3 offset = Vector3.zero;

    // �X�^�[�g���Ɏ����Ő����������ꍇ
    void Start() {
        SpawnEffect("NoStanCoin");
    }

    // �O������Ăяo���郁�\�b�h
    public void SpawnEffect(string ItemName) {

        //�����͉��u���A�������O�������Ŏ󂯎���āA�����I�ɂǂ̃G�t�F�N�g���Ăяo���̂���ݒ�ł���悤�ɂ�����
        int index = 0;
        switch (ItemName) {
            case "NoStanCoin":
                index = 0; 
                break;
        }


        if (effects == null) {
            Debug.LogWarning("Effect prefab is not assigned.");
            return;
        }

        Transform parent = parentObject;

        GameObject effectInstance = Instantiate(effects[index], parent);
        effectInstance.transform.localPosition = offset;
    }
}
