using System.Collections.Generic;
using UnityEngine;

public class ReplayRecorder : MonoBehaviour
{
    public float recordDuration = 5f;
    private List<ReplayFrame> frames = new List<ReplayFrame>();
    private float startTime;
    private bool isReplaying = false;
    private int replayIndex = 0;

    void Update()
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
        if (Time.time - startTime > recordDuration)
        {
            if (frames.Count > 0) frames.RemoveAt(0);
        }

        frames.Add(new ReplayFrame
        {
            position = transform.position,
            rotation = transform.rotation,
            time = Time.time
        });
    }

    public void StartReplay()
    {
        isReplaying = true;
        replayIndex = 0;
    }

    void PlayReplay()
    {
        if (replayIndex >= frames.Count)
        {
            EndReplay(); // �����I�����ď�ԃ��Z�b�g
            return;
        }

        ReplayFrame frame = frames[replayIndex];
        transform.position = frame.position;
        transform.rotation = frame.rotation;

        replayIndex++;
    }

    void EndReplay()
    {
        isReplaying = false;
        replayIndex = 0;
        // �K�v�Ȃ�A�����ʒu�ɖ߂������Ȃǂ������ŁI
    }

    public void StopReplayAndReset()
    {
        EndReplay();
        frames.Clear(); // �� ����ŋL�^�����S���Z�b�g�I
    }
}
