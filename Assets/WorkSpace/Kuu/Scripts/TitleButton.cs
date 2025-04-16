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
        // �{�^���R���|�[�l���g�̎擾
        focusButton = focusButton.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        // �S�Ẵt�H�[�J�X������
        EventSystem.current.SetSelectedGameObject(null);
        // focusBotton�Ƀt�H�[�J�X����
        focusButton.Select();
    }
}
