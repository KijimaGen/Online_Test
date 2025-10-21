using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static GameManager;

public class TitleEbiUIManager : MonoBehaviour
{
    // �Z���N�g�L�����o�X
    [SerializeField] Canvas SelectCanvas;
    // �C�x���g�V�X�e��
    [SerializeField] EventSystem eventSystem;
    // ���ꂼ��̃G�rUI
    [SerializeField] Image startEbi;
    [SerializeField] Image ruleEbi;
    [SerializeField] Image exitEbi;
    [SerializeField] Image okEbi;
    [SerializeField] Image tutorialEbi;

    // �Z���N�g���Ă���I�u�W�F�N�g
    GameObject selectObject;

    // Start is called before the first frame update
    void Start()
    {
        startEbi.transform.gameObject.SetActive(false);
        ruleEbi.transform.gameObject.SetActive(false);
        exitEbi.transform.gameObject.SetActive(false);
        okEbi.transform.gameObject.SetActive(false);
        tutorialEbi.transform.gameObject.SetActive(false);

        selectObject = eventSystem.currentSelectedGameObject.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // null�`�F�b�N
        if (selectObject != null)
        {
            // ���݃Z���N�g����Ă���̂�
            // GameStart
            if (selectObject.name == "GameStart")
            {
                startEbi.transform.gameObject.SetActive(true);
            }
            else
            {
                startEbi.transform.gameObject.SetActive(false);
            }
            // Rule
            if (selectObject.name == "Rule")
            {
                ruleEbi.transform.gameObject.SetActive(true);
            }
            else
            {
                ruleEbi.transform.gameObject.SetActive(false);
            }
            // Exit
            if (selectObject.name == "Exit")
            {
                exitEbi.transform.gameObject.SetActive(true);
            }
            else
            {
                exitEbi.transform.gameObject.SetActive(false);
            }
            // OK
            if (selectObject.name == "OK")
            {
                okEbi.transform.gameObject.SetActive(true);
            }
            else
            {
                okEbi.transform.gameObject.SetActive(false);
            }
            // Tutorial
            if (selectObject.name == "Tutorial")
            {
                tutorialEbi.transform.gameObject.SetActive(true);
            }
            else
            {
                tutorialEbi.transform.gameObject.SetActive(false);
            }
        } else
        {
            return;
        }
    }
}
