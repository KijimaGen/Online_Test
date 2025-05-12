using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommonModule{
    public static bool CheckHasChild(string name, Transform transform) {
        foreach (Transform child in transform) {  // ���̃I�u�W�F�N�g�̂��ׂĂ̎q�I�u�W�F�N�g�����[�v
            if (child.gameObject.name == name) {  // �q�̖��O����v���邩�m�F
                return true;
            }
        }
        return false;
    }

    public static void RemoveChildByName(string name,Transform transform) {
        // �e�I�u�W�F�N�g�̂��ׂĂ̎q�I�u�W�F�N�g�����[�v
        foreach (Transform child in transform) {
            if (child.gameObject.name == name) {  // ���O����v����ꍇ
                // �q�I�u�W�F�N�g���폜
                MonoBehaviour.Destroy(child.gameObject);
                return;  // �ŏ��Ɍ������q�I�u�W�F�N�g���폜������I��
            }
        }
    }
}
