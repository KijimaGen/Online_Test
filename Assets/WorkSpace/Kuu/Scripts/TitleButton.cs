using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleButton : MonoBehaviour
{
    [SerializeField] Button button;
    Button GameStart;
    Button Rule;
    Button Exit;

    // Start is called before the first frame update
    void Start()
    {
        // �{�^���R���|�[�l���g�̎擾
        //GameStart = GameObject.Find("/Canvas/GameStart").GetComponent<Button>();
        //Rule = GameObject.Find("/Canvas/Rule").GetComponent<Button>();
        //Exit = GameObject.Find("/Canvas/Exit").GetComponent<Button>();
        //GameStart.Select();

        button = button.GetComponent<Button>();
        button.Select();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickStart() {
        // �S�Ẵt�H�[�J�X������
        //EventSystem.current.SetSelectedGameObject(null);

        // �Q�[���V�[��
        SceneManager.LoadScene("Game");
    }

    public void OnClickRule() {
        // �S�Ẵt�H�[�J�X������
        //EventSystem.current.SetSelectedGameObject(null);
    }

    public void OnClickExit() {
        // �S�Ẵt�H�[�J�X������
        //EventSystem.current.SetSelectedGameObject(null);

        // �Q�[�����I������
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
