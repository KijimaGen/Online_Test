using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class ReplayRecorder : MonoBehaviour
{
    static public ReplayRecorder instance;
    private float recordDuration = 10f;
    public List<ReplayFrameData> frames = new List<ReplayFrameData>();
    //private float startTime;
    public bool isReplaying = false;
    private int replayIndex = 0;

    private Animator animator;

    private float animTimeEnd;

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
        if (GameManager.instance.state != GameManager.gameState.start) return;
        float currentTime = Time.time;

        // �Â��t���[���폜
        frames.RemoveAll(frame => currentTime - frame.time > recordDuration);

        // �p�����[�^�[���W
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
        

        // �t���[���ǉ�
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

        // �ʒu�Ɖ�]�̍Đ�
        transform.position = frame.position;
        transform.rotation = frame.rotation;

        // �A�j���[�V�����̃p�����[�^���Z�b�g
        if (animator != null)
        {
            foreach (var param in frame.boolParams)
            {
                animator.SetBool(param.Key, param.Value);
            }

        }

        replayIndex++;
    }

    public async void StopReplayAndReset()
    {
        //transform.GetComponent<Rigidbody>().isKinematic = false;
        await Task.Delay(2000);
        GameManager.instance.RoundInitialize();

        isReplaying = false;
        replayIndex = 0;
    }

}
