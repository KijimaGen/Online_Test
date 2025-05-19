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
        // コンポーネントの取得
        GameStart = GameObject.Find("/SelectCanvas/GameStart").GetComponent<Button>();
        Rule = GameObject.Find("/SelectCanvas/Rule").GetComponent<Button>();
        Exit = GameObject.Find("/SelectCanvas/Exit").GetComponent<Button>();
        OK = OK.GetComponent<Button>();

        RuleCanvas = RuleCanvas.GetComponent<Canvas>();
        SelectCanvas = GameObject.Find("/SelectCanvas").GetComponent<Canvas>();
        // GameStartボタンにフォーカス
        GameStart.Select();

        // RuleCanvasを非表示
        RuleCanvas.transform.gameObject.SetActive(false);
        //button = button.GetComponent<Button>();
        //button.Select();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickStart() {
        // 全てのフォーカスを解除
        EventSystem.current.SetSelectedGameObject(null);

        // ゲームシーン
        SceneManager.LoadScene("MainGame");
    }

    public void OnClickRule() {
        // 全てのフォーカスを解除
        EventSystem.current.SetSelectedGameObject(null);

        // RuleCanvasを表示
        RuleCanvas.transform.gameObject.SetActive(true);
        // OKにフォーカスする
        OK.Select();
        // SelectCanvasを非表示
        SelectCanvas.transform.gameObject.SetActive(false);
    }

    public void OnClickExit() {
        // 全てのフォーカスを解除
        EventSystem.current.SetSelectedGameObject(null);

        // ゲームを終了する
        // UnityEditorはビルドでは使えないので別の方法で落とす必要がある
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void OnClickOk() {
        // 全てのフォーカスを解除
        EventSystem.current.SetSelectedGameObject(null);

        // SelectCanvasを表示
        SelectCanvas.transform.gameObject.SetActive(true);
        // GameStartにフォーカスする
        GameStart.Select();
        // RuleCanvasを非表示
        RuleCanvas.transform.gameObject.SetActive(false);
    }

    public void OnClickChutorial() {
        // 全てのフォーカスを解除
        //EventSystem.current.SetSelectedGameObject(null);

        // ゲームシーン
        //SceneManager.LoadScene("MainGame");
    }
}
