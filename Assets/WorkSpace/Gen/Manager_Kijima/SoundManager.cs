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
        audioSource.PlayOneShot(audioClips[soundIndex]);
    }

    public void PlaySoundOne(int soundIndex) {
        if (audioClips[soundIndex] == null) {
            Debug.LogWarning("audioClipsの" + soundIndex + "番目は存在しませんInspectorのListをご確認ください");
            return;
        }
        if (!audioSource.isPlaying)
            audioSource.PlayOneShot(audioClips[soundIndex]);
    }

}
