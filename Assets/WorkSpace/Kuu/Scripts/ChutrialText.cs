using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChutrialText : MonoBehaviour
{
    public TextMesh text;
    // �e�L�X�g��ς��邽�߂̃J�E���g
    int TextCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        TextCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // A����������e�L�X�g��i�߂�
        if (Input.GetKeyDown(KeyCode.JoystickButton0) == true || Input.GetMouseButtonDown(0)) {
            // �Q�����Ă��Ȃ�������i�܂Ȃ�
            if (!(TextCount == 1)) {
            TextCount++;
            }
        }
        // �ꌾ�ڂ�LB����������񌾖ڂ��΂�
        if (Input.GetKeyDown(KeyCode.JoystickButton4)) {
            if (TextCount == 0) {
                TextCount = 2;
                // �����ɐi��
            } else if (TextCount == 1) {
                TextCount++;
            }

        }

        if (TextCount == 0) {
            text.text = "�����̓`���[�g���A�����[���ł�";
        } else if (TextCount == 1) {
            text.text = "LB�������ăQ�[���ɎQ�����܂��傤";
        } else if (TextCount == 2) {
            text.text = "�ő�4�l�܂ŎQ���ł��܂�";
        } else if (TextCount == 3) {
            text.text = "�ԂƔ��̏��𓥂ނƂ��ꂼ��̃`�[���ɂȂ�܂�";
        } else if (TextCount == 4) {

        }
    }
}
