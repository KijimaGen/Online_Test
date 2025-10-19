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
        // コンポーネントの取得
        GameStart = GameObject.Find("SelectCanvas/GameStart").GetComponent<Button>();
        Rule = GameObject.Find("/SelectCanvas/Rule").GetComponent<Button>();
        Exit = GameObject.Find("/SelectCanvas/Exit").GetComponent<Button>();
        OK = OK.GetComponent<Button>();

        RuleCanvas = RuleCanvas.GetComponent<Canvas>();
        SelectCanvas = GameObject.Find("/SelectCanvas").GetComponent<Canvas>();
        // GameStartボタンにフォーカス
        GameStart.Select();

        // RuleCanvasを非表示
        RuleCanvas.transform.gameObject.SetActive(false);

        //BGMをタイトル用の物に設定
        SoundManager.Instance.ChangeBGM(0);
    }

    public void OnClickStart() {
        // 全てのフォーカスを解除
        SoundManager.Instance.PlaySound(4);

        // ゲームシーン
        SceneManager.LoadScene("Game");
    }

    public void OnClickRule() {
        // RuleCanvasを表示
        RuleCanvas.transform.gameObject.SetActive(true);
        // OKにフォーカスする
        OK.Select();
        // SelectCanvasを非表示
        SelectCanvas.transform.gameObject.SetActive(false);
        SoundManager.Instance.PlaySound(7);
    }

    public void OnClickExit() {
        // ゲームを終了する
        SoundManager.Instance.PlaySound(7);
        Application.Quit();
    }

    public void OnClickOk() {
        // SelectCanvasを表示
        SelectCanvas.transform.gameObject.SetActive(true);
        // GameStartにフォーカスする
        GameStart.Select();
        // RuleCanvasを非表示
        RuleCanvas.transform.gameObject.SetActive(false);
        SoundManager.Instance.PlaySound(7);
    }

    public void OnClickChutorial() {
        // ゲームシーン
        SceneManager.LoadScene("ChutorialScene");
    }
}
