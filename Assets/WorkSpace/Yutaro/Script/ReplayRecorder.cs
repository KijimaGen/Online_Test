using System.Collections.Generic;
using UnityEngine;

public class ReplayRecorder : MonoBehaviour
{
    static public ReplayRecorder instance;
    private float recordDuration = 10f;
    public List<ReplayFrameData> frames = new List<ReplayFrameData>();
    //private float startTime;
    public bool isReplaying = false;
    private int replayIndex = 0;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        instance = this;
    }
    void FixedUpdate()
    {
        if (isReplaying)
        {
            PlayReplay();
        }
        else
        {
            RecordFrame();
        }
    }

    void RecordFrame()
    {
        float currentTime = Time.time;

        // 古いフレーム削除
        frames.RemoveAll(frame => currentTime - frame.time > recordDuration);

        // パラメーター収集
        var floatParams = new Dictionary<string, float>();
        var boolParams = new Dictionary<string, bool>();

        if(animator != null)
        {
            foreach (AnimatorControllerParameter param in animator.parameters)
            {
                switch (param.type)
                {
                    case AnimatorControllerParameterType.Bool:
                        boolParams[param.name] = animator.GetBool(param.name);
                        break;
                }
            }
        }
        

        // フレーム追加
        frames.Add(new ReplayFrameData
        {
            position = transform.position,
            rotation = transform.rotation,
            time = currentTime,
            floatParams = floatParams,
            boolParams = boolParams
        });
    }

    public void StartReplay()
    {
        isReplaying = true;
        replayIndex = 0;
        transform.GetComponent<Rigidbody>().isKinematic = true;
    }

    void PlayReplay()
    {
        if (replayIndex >= frames.Count)
        {
            StopReplayAndReset();
            return;
        }

        ReplayFrameData frame = frames[replayIndex];

        // 位置と回転の再生
        transform.position = frame.position;
        transform.rotation = frame.rotation;

        // アニメーションのパラメータをセット
        if (animator != null)
        {
            foreach (var param in frame.boolParams)
            {
                animator.SetBool(param.Key, param.Value);
            }

        }

        replayIndex++;
    }


    void EndReplay()
    {
        isReplaying = false;
        replayIndex = 0;
        // 必要なら、初期位置に戻す処理などもここで！
    }

    public void StopReplayAndReset()
    {
        transform.GetComponent<Rigidbody>().isKinematic = false;
        EndReplay();
        frames.Clear(); // ← これで記録も完全リセット！
    }
}
