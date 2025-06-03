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
            DontDestroyOnLoad(gameObject); // シーンをまたいでも消えない
        }
        else {
            Destroy(gameObject); // 複数にならないように
        }
        
    }

    
    public void PlaySound(int soundIndex) {
        if(soundIndex < -1 || soundIndex > audioClips.Count) {
            Debug.LogWarning(soundIndex + "番はaudioClipsのListの範囲外です");
            return;
        }
        if (audioClips[soundIndex] == null) {
            Debug.LogWarning("audioClipsの" + soundIndex + "番目は存在しませんInspectorのListをご確認ください");
            return;
        }
        SESource.PlayOneShot(audioClips[soundIndex]);
    }

    /// <summary>
    /// 効果音がかぶらないように放つ
    /// </summary>
    /// <param name="soundIndex"></param>
    public void PlaySoundOne(int soundIndex) {
        if (audioClips[soundIndex] == null) {
            Debug.LogWarning("audioClipsの" + soundIndex + "番目は存在しませんInspectorのListをご確認ください");
            return;
        }
        if (!SESource.isPlaying)
            SESource.PlayOneShot(audioClips[soundIndex]);
    }

    /// <summary>
    /// BGM変更
    /// </summary>
    /// <param name="BGMIndex"></param>
    public void ChangeBGM(int BGMIndex) {
        if (BGMSource == null || BGMClips[BGMIndex] == null || BGMIndex > BGMClips.Count) return;
        BGMSource.clip = BGMClips[BGMIndex];
        BGMSource.Play();
    }
}
