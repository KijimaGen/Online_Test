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
}
