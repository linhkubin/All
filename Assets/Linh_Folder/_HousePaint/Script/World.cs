using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class World : Singleton<World>
{
    public Transform tf;
    public AnimationCurve curveTime;
    public ParticleSystem winVFX;

    Quaternion targetRotation;
    bool isMoving;

    UnityAction doneAction;
    private int length;

    public void OnInit()
    {
        tf.rotation = Quaternion.identity;
    }

    public void SetTarget(Vector3 rot, UnityAction doneAction)
    {
        this.doneAction = doneAction;
        targetRotation = Quaternion.Euler(rot);

        if (!isMoving)
        {
            StartCoroutine(IEMoveWorld());
        }
    }

    public void SetWin(UnityAction doneAction)
    {
        this.doneAction = doneAction;
        StartCoroutine(IEMoveWin());
    }

    private IEnumerator IEMoveWorld()
    {
        isMoving = true;

        while (Mathf.Abs(tf.eulerAngles.y - targetRotation.eulerAngles.y) > 1f)
        {
            tf.rotation = Quaternion.Lerp(tf.rotation, targetRotation, Time.deltaTime * 5f);
            yield return null;
        }

        isMoving = false;

        doneAction?.Invoke();
    }  
    
    
    private IEnumerator IEMoveWin()
    {
        winVFX.Play();
        float time = 0;

        while (time < 3.5f)
        {
            time += Time.deltaTime;

            tf.rotation = Quaternion.Euler(Vector3.up * (270 + curveTime.Evaluate(time)));

            yield return null;
        }

        doneAction?.Invoke();
    }
}
