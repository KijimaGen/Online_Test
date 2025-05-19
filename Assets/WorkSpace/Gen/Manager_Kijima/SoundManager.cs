using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour{
    public static SoundManager Instance { get; private set; }
    [SerializeField]List<AudioClip>audioClips = new List<AudioClip>();
    AudioSource audioSource;
    
    void Start() {
        audioSource = GetComponent<AudioSource>();
       
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �V�[�����܂����ł������Ȃ�
        }
        else {
            Destroy(gameObject); // �����ɂȂ�Ȃ��悤��
        }
        
    }

    
    public void PlaySound(int soundIndex) {
        if(soundIndex < -1 || soundIndex > audioClips.Count) {
            Debug.LogWarning(soundIndex + "�Ԃ�audioClips��List�͈̔͊O�ł�");
            return;
        }
        if (audioClips[soundIndex] == null) {
            Debug.LogWarning("audioClips��" + soundIndex + "�Ԗڂ͑��݂��܂���Inspector��List�����m�F��������");
            return;
        }
        audioSource.PlayOneShot(audioClips[soundIndex]);
    }

    public void PlaySoundOne(int soundIndex) {
        if (audioClips[soundIndex] == null) {
            Debug.LogWarning("audioClips��" + soundIndex + "�Ԗڂ͑��݂��܂���Inspector��List�����m�F��������");
            return;
        }
        if (!audioSource.isPlaying)
            audioSource.PlayOneShot(audioClips[soundIndex]);
    }

}
