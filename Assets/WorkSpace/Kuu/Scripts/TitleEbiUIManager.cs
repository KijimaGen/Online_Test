using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static GameManager;

public class TitleEbiUIManager : MonoBehaviour
{
    // セレクトキャンバス
    [SerializeField] Canvas SelectCanvas;
    // イベントシステム
    [SerializeField] EventSystem eventSystem;
    // それぞれのエビUI
    [SerializeField] Image startEbi;
    [SerializeField] Image ruleEbi;
    [SerializeField] Image exitEbi;
    [SerializeField] Image okEbi;
    [SerializeField] Image tutorialEbi;

    // セレクトしているオブジェクト
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
        // nullチェック
        if (selectObject != null)
        {
            // 現在セレクトされているのが
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
