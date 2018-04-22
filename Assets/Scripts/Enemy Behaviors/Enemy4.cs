using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : EnemyBase {

	private Quaternion targetRotation;
	private Quaternion previousRotation;
	private float angleLimit = 30f;
	private float pivotTime = 1f;
	private float pivotElapsedTime;
	private float attackTime = 10f;

	public void Start() {
		previousRotation = Quaternion.AngleAxis(180 - angleLimit, Vector3.forward);
		targetRotation = Quaternion.AngleAxis(180 + angleLimit, Vector3.forward);
		pivotElapsedTime = pivotTime/2;
	}

	public override void EnemyAction() {
		switch (state) {
			case 0:
				Entrance();
				break;
			case 1:
				Pivot();
				break;
			case 2:
				Departure();
				break;
		}

		if (state != 0) {
			Attack();
		}
	}

	private void Pivot() {
		if (transform.rotation == targetRotation) {
			angleLimit *= -1;
			previousRotation = targetRotation;
			targetRotation = Quaternion.AngleAxis(180 + angleLimit, Vector3.forward);
			pivotElapsedTime = 0;
		}

		pivotElapsedTime += Time.deltaTime;
		transform.rotation = Quaternion.Slerp(previousRotation, targetRotation, pivotElapsedTime/pivotTime);

		attackTime -= Time.deltaTime;
		if (attackTime <= 0) {
			state = 2;
		}
	}
}
