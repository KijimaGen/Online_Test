using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    bool redTeam;
    bool whiteTeam;
    bool independentTeam;

    [SerializeField] private Image chargeSlider;
    [SerializeField] private Text nameText;

    int playerName;
    void Start()
    {
        chargeSlider.fillAmount = 0f;
        playerName = GameManager.instance.playerList.Count;
    }

    private void FixedUpdate()
    {
        nameText.text = playerName.ToString() + "P";
        nameText.rectTransform.LookAt(Camera.main.transform);
        if (Input.GetKey(KeyCode.R))
        {
            GetComponent<ReplayRecorder>().StartReplay();
            //chargeSlider.fillAmount += 0.0005f;
        }
        else
        {
            chargeSlider.fillAmount = 0;
        }



        if(chargeSlider.fillAmount >= 0.4f)
        {
            chargeSlider.fillAmount = 0.4f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "RedSetTeam")
        {
            gameObject.tag = "RedTeam";
            //gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
        if (collision.gameObject.name == "WhiteSetTeam")
        {
            gameObject.tag = "WhiteTeam";
            //gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
    }


}