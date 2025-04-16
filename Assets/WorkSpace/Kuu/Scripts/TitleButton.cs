using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TitleButton : MonoBehaviour
{
    [SerializeField] Button focusButton;

    // Start is called before the first frame update
    void Start()
    {
        // ボタンコンポーネントの取得
        focusButton = focusButton.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        // 全てのフォーカスを解除
        EventSystem.current.SetSelectedGameObject(null);
        // focusBottonにフォーカスする
        focusButton.Select();
    }
}
