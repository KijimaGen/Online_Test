using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleButton : MonoBehaviour
{
    [SerializeField] Canvas RuleCanvas;
    [SerializeField] Button OK;
    Button GameStart;
    Button Rule;
    Button Exit;
    Canvas SelectCanvas;

    EventSystem eventSystem;

    // Start is called before the first frame update
    void Start()
    {
        // �R���|�[�l���g�̎擾
        GameStart = GameObject.Find("SelectCanvas/GameStart").GetComponent<Button>();
        Rule = GameObject.Find("/SelectCanvas/Rule").GetComponent<Button>();
        Exit = GameObject.Find("/SelectCanvas/Exit").GetComponent<Button>();
        OK = OK.GetComponent<Button>();

        RuleCanvas = RuleCanvas.GetComponent<Canvas>();
        SelectCanvas = GameObject.Find("/SelectCanvas").GetComponent<Canvas>();
        // GameStart�{�^���Ƀt�H�[�J�X
        GameStart.Select();

        // RuleCanvas���\��
        RuleCanvas.transform.gameObject.SetActive(false);

        //BGM���^�C�g���p�̕��ɐݒ�
        SoundManager.Instance.ChangeBGM(0);
    }

    public void OnClickStart() {
        // �S�Ẵt�H�[�J�X������
        SoundManager.Instance.PlaySound(4);

        // �Q�[���V�[��
        SceneManager.LoadScene("Game");
    }

    public void OnClickRule() {
        // RuleCanvas��\��
        RuleCanvas.transform.gameObject.SetActive(true);
        // OK�Ƀt�H�[�J�X����
        OK.Select();
        // SelectCanvas���\��
        SelectCanvas.transform.gameObject.SetActive(false);
        SoundManager.Instance.PlaySound(7);
    }

    public void OnClickExit() {
        // �Q�[�����I������
        SoundManager.Instance.PlaySound(7);
        Application.Quit();
    }

    public void OnClickOk() {
        // SelectCanvas��\��
        SelectCanvas.transform.gameObject.SetActive(true);
        // GameStart�Ƀt�H�[�J�X����
        GameStart.Select();
        // RuleCanvas���\��
        RuleCanvas.transform.gameObject.SetActive(false);
        SoundManager.Instance.PlaySound(7);
    }

    public void OnClickChutorial() {
        // �Q�[���V�[��
        SceneManager.LoadScene("ChutorialScene");
    }
}
