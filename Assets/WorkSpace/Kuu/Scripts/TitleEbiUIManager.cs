using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static GameManager;

public class TitleEbiUIManager : MonoBehaviour
{
    [SerializeField] Canvas SelectCanvas;
    [SerializeField] EventSystem eventSystem;
    [SerializeField] Image startEbi;
    [SerializeField] Image ruleEbi;
    [SerializeField] Image exitEbi;
    [SerializeField] Image okEbi;
    [SerializeField] Image tutorialEbi;

    // Start is called before the first frame update
    void Start()
    {
        startEbi.transform.gameObject.SetActive(false);
        ruleEbi.transform.gameObject.SetActive(false);
        exitEbi.transform.gameObject.SetActive(false);
        okEbi.transform.gameObject.SetActive(false);
        tutorialEbi.transform.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (eventSystem.currentSelectedGameObject.gameObject.name == "GameStart") {
            startEbi.transform.gameObject.SetActive(true);
        }
        else {
            startEbi.transform.gameObject.SetActive(false); 
        }
        if (eventSystem.currentSelectedGameObject.gameObject.name == "Rule") {
            ruleEbi.transform.gameObject.SetActive(true);
        }
        else {
            ruleEbi.transform.gameObject.SetActive(false);
        }
        if (eventSystem.currentSelectedGameObject.gameObject.name == "Exit") {
            exitEbi.transform.gameObject.SetActive(true);
        }
        else {
            exitEbi.transform.gameObject.SetActive(false);
        }
        if (eventSystem.currentSelectedGameObject.gameObject.name == "OK") {
            okEbi.transform.gameObject.SetActive(true);
        }
        else {
            okEbi.transform.gameObject.SetActive(false);
        }
        if (eventSystem.currentSelectedGameObject.gameObject.name == "Tutorial") {
            tutorialEbi.transform.gameObject.SetActive(true);
        }
        else {
            tutorialEbi.transform.gameObject.SetActive(false);
        }
        
    }
}
