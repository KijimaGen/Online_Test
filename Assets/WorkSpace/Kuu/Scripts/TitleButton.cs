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
        // ボタンコンポーネントの取得
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
        // 全てのフォーカスを解除
        //EventSystem.current.SetSelectedGameObject(null);

        // ゲームシーン
        SceneManager.LoadScene("Game");
    }

    public void OnClickRule() {
        // 全てのフォーカスを解除
        //EventSystem.current.SetSelectedGameObject(null);
    }

    public void OnClickExit() {
        // 全てのフォーカスを解除
        //EventSystem.current.SetSelectedGameObject(null);

        // ゲームを終了する
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
