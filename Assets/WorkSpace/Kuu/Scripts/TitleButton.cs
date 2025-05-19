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
    //Button OK;
    //Canvas RuleCanvas;
    Canvas SelectCanvas;

    // Start is called before the first frame update
    void Start()
    {
        // �R���|�[�l���g�̎擾
        GameStart = GameObject.Find("/SelectCanvas/GameStart").GetComponent<Button>();
        Rule = GameObject.Find("/SelectCanvas/Rule").GetComponent<Button>();
        Exit = GameObject.Find("/SelectCanvas/Exit").GetComponent<Button>();
        OK = OK.GetComponent<Button>();

        RuleCanvas = RuleCanvas.GetComponent<Canvas>();
        SelectCanvas = GameObject.Find("/SelectCanvas").GetComponent<Canvas>();
        // GameStart�{�^���Ƀt�H�[�J�X
        GameStart.Select();

        // RuleCanvas���\��
        RuleCanvas.transform.gameObject.SetActive(false);
        //button = button.GetComponent<Button>();
        //button.Select();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickStart() {
        // �S�Ẵt�H�[�J�X������
        EventSystem.current.SetSelectedGameObject(null);

        // �Q�[���V�[��
        SceneManager.LoadScene("MainGame");
    }

    public void OnClickRule() {
        // �S�Ẵt�H�[�J�X������
        EventSystem.current.SetSelectedGameObject(null);

        // RuleCanvas��\��
        RuleCanvas.transform.gameObject.SetActive(true);
        // OK�Ƀt�H�[�J�X����
        OK.Select();
        // SelectCanvas���\��
        SelectCanvas.transform.gameObject.SetActive(false);
    }

    public void OnClickExit() {
        // �S�Ẵt�H�[�J�X������
        EventSystem.current.SetSelectedGameObject(null);

        // �Q�[�����I������
        // UnityEditor�̓r���h�ł͎g���Ȃ��̂ŕʂ̕��@�ŗ��Ƃ��K�v������
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void OnClickOk() {
        // �S�Ẵt�H�[�J�X������
        EventSystem.current.SetSelectedGameObject(null);

        // SelectCanvas��\��
        SelectCanvas.transform.gameObject.SetActive(true);
        // GameStart�Ƀt�H�[�J�X����
        GameStart.Select();
        // RuleCanvas���\��
        RuleCanvas.transform.gameObject.SetActive(false);
    }

    public void OnClickChutorial() {
        // �S�Ẵt�H�[�J�X������
        //EventSystem.current.SetSelectedGameObject(null);

        // �Q�[���V�[��
        //SceneManager.LoadScene("MainGame");
    }
}
