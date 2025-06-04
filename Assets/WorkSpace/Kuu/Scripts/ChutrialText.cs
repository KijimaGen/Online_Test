using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChutrialText : MonoBehaviour
{
    public TextMesh text;
    // �e�L�X�g��ς��邽�߂̃J�E���g
    int textCount = 0;
    // �e�L�X�g�̍ő吔
    int TEXT_MAX = 12;
    // �e�L�X�g���߂������Ƃ����邩�̃t���O
    bool textJoin = false;
    // Start is called before the first frame update
    void Start()
    {
        textCount = 0;
        textJoin = false;
    }

    // Update is called once per frame
    void Update()
    {
        // ���~����
        if (textCount < 0) {
            textCount = 0;
        }
        // �㏸����
        if (textCount > TEXT_MAX) {
            textCount = TEXT_MAX;
        }
        // RB����������e�L�X�g��i�߂�
        if (Input.GetKeyDown(KeyCode.JoystickButton5) == true || Input.GetMouseButtonDown(0)) {
            // �Q�����Ă��Ȃ�������i�܂Ȃ�
            if (!(textCount == 1) && !textJoin) {
                textCount++;
            } else if(textJoin) {
                textCount++;
            }
        }
        // �ꌾ�ڂ�LB����������񌾖ڂ��΂�
        if (Input.GetKeyDown(KeyCode.JoystickButton4)) {
            if (textCount == 0) {
                textCount = 2;
                textJoin = true;
                // �����ɐi��
            } else if (textCount == 1) {
                textCount++;
                textJoin = true;
            }

        }
        // B����������e�L�X�g��߂�
        if (Input.GetKeyDown(KeyCode.JoystickButton2) == true || Input.GetMouseButtonDown(1)) {
            textCount--;
        }

        if (textCount == 0) {
            text.text = "�����̓`���[�g���A�����[���ł�";
        } else if (textCount == 1) {
            text.text = "LB�������ăQ�[���ɎQ�����܂��傤";
        } else if (textCount == 2) {
            text.text = "�ő�4�l�܂ŎQ���ł��܂�";
        } else if (textCount == 3) {
            text.text = "�ԂƔ��̏��𓥂ނƂ��ꂼ��̃`�[���ɂȂ�܂�";
        } else if (textCount == 4) {
            text.text = "B�������ƃ��P�b�g��U��܂�";
        } else if (textCount == 5) {
            text.text = "�`���[�W��MAX�ɂȂ�ƃW�����v�X�}�b�V�����łĂ܂�";
        } else if (textCount == 6) {
            text.text = "B�ő���v���C���[���U������Ɛ�����т܂�";
        } else if (textCount == 7) {
            text.text = "�|��Ă��܂�������A�������Ζ߂�܂�";
        } else if (textCount == 8) {
            text.text = "�X�y�V�����Q�[�W��MAX�̎���Y�������ƃX�y�V�������g���܂�";
        } else if (textCount == 9) {
            text.text = "�X�y�V�����Q�[�W�͎��Ԍo�߂ƃV���g����ł��Ƃő����܂�";
        } else if (textCount == 10) {
            text.text = "�X�y�V�����̓R�X�`���[���ɂ���Č��ʂ��ς��܂�";
        }�@else if (textCount == 11) {
            text.text = "�C���`�F���Ȃǂ͑Ή����鏰�𓥂݂Ȃ���LB�ŕς��܂�";
        } else if (textCount == TEXT_MAX) {
            text.text = "START�{�^���Ŏ����J�n�ł�";
        }
    }
}
