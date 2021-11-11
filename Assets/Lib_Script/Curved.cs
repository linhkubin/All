using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Curved : GameUnit
{
	public const float HZ = 1 / 50f;

	public Transform model;

	public AnimationCurve curve;

	public float bodySize;

    private void OnDisable()
    {
		StopAllCoroutines();
    }

    public void SetTarget(Vector3 startPos, Transform target, UnityAction doneAction)
	{
		model.localScale = Vector3.one;

		StartCoroutine(IEMoveCurve(startPos, target, tf, doneAction));
	}

	public virtual void Progress(Vector3 target, float ratio)
    {
		model.localScale = Vector3.one * curve.Evaluate(ratio);
	}

	private IEnumerator IEMoveCurve(Vector3 startPoint, Transform target, Transform item, UnityAction callBack)
	{
		Vector3 start = startPoint;
		Vector3 finish = target.position;

		//chieu cao test
		Vector3 height = new Vector3((start.x + finish.x) / 2, 7, (start.z + finish.z) / 2);

		float ratio = 0;

		while (ratio <= 1)
		{
			var tangent1 = Vector3.Lerp(start, height, ratio);
			var tangent2 = Vector3.Lerp(height, target.position, ratio);
			var curve = Vector3.Lerp(tangent1, tangent2, ratio);


			ratio += HZ;
			Progress(curve, ratio);
			item.position = curve;

			yield return null;
		}

		callBack?.Invoke();
	}

	//public void Feed(Vector3 targetBait)
 //   {
	//	targetBait = targetBait + Vector3.down * 3;
	//	Vector3 start = GetOnCircle(0)* 5 + targetBait;
	//	StartCoroutine(IERun(start, targetBait, 40, 1, null));
 //   }

	//public void RunOut()
 //   {
	//	Vector3 finish = GetOnCircle(0) * 5 + tf.position;

	//	StartCoroutine(IERun(tf.position, finish, 10, 0, () => SimplePool.Despawn(this)));
	//}

	public void Patrol(Vector3 target, Vector3 direct)
    {
		int randDeep = Random.Range(6, 8);
		target = target + Vector3.down * randDeep;

		int speed = Random.Range(90, 150);

		StartCoroutine(IERun(target - direct * 10, target + direct * 10, speed, 0, () => SimplePool.Despawn(this)));
	}

	private IEnumerator IERun(Vector3 start, Vector3 finish, float speed, float delay, UnityAction callBack)
    {
		if (delay > 0)
		{
			yield return Cache.GetWFS(delay);
		}

		tf.position = start;

		model.localScale = Vector3.one;

		tf.forward = (finish - start).normalized;

		finish = finish + (start - finish).normalized * bodySize;

		float ratio = 0;
		float hz = 1 / speed;

		while (ratio <= 1)
		{
			tf.position = Vector3.Lerp(start, finish, ratio);
			ratio += hz;

			yield return null;
		}

		callBack?.Invoke();
    }

	private Vector3 GetOnCircle(float y)
    {
		Vector2 onCircle = Random.insideUnitCircle;
		return new Vector3(onCircle.x, y, onCircle.y);
    }


    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(tf.position, bodySize);
    //}
}
