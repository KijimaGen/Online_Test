using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CommonModule;
public class PlayerIcon : MonoBehaviour{
    [SerializeField]
    Camera camera;

    private void Start() {
        camera.targetTexture = PlayerCards.Instance.GetPlayerIcon();
        PlayerCards.Instance.AddPlayer();
    }
    private void Update() {
        if(CheckHasChild("NoStanEffect(Clone)", this.transform)) {

        }
    }

}
