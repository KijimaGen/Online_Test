using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour{
    public static SoundManager Instance { get; private set; }
    [SerializeField]List<AudioClip>audioClips = new List<AudioClip>();
    [SerializeField] List<AudioClip> BGMClips = new List<AudioClip>();

    [SerializeField]
    AudioSource SESource;

    [SerializeField]
    AudioSource BGMSource;

    void Awake() {
        SESource = GetComponent<AudioSource>();
       
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
        SESource.PlayOneShot(audioClips[soundIndex]);
    }

    /// <summary>
    /// ���ʉ������Ԃ�Ȃ��悤�ɕ���
    /// </summary>
    /// <param name="soundIndex"></param>
    public void PlaySoundOne(int soundIndex) {
        if (audioClips[soundIndex] == null) {
            Debug.LogWarning("audioClips��" + soundIndex + "�Ԗڂ͑��݂��܂���Inspector��List�����m�F��������");
            return;
        }
        if (!SESource.isPlaying)
            SESource.PlayOneShot(audioClips[soundIndex]);
    }

    /// <summary>
    /// BGM�ύX
    /// </summary>
    /// <param name="BGMIndex"></param>
    public void ChangeBGM(int BGMIndex) {
        if (BGMSource == null || BGMClips[BGMIndex] == null || BGMIndex > BGMClips.Count) return;
        BGMSource.clip = BGMClips[BGMIndex];
        BGMSource.Play();
    }
}
