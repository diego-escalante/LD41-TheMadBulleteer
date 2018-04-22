using UnityEngine;

public class Enemy2 : EnemyBase {

	private float driftTime = 10f;
	private float direction;

	public  void Start() {
		direction = Mathf.Sign(Random.Range(-2, 1));
	}

	public override void EnemyAction() {
		switch (state) {
			case 0:
				Entrance();
				break;
			case 1:
				Drift();
				break;
			case 2:
				Departure();
				break;
		}

		if (state == 1) {
			Attack();
		}
	}
	
	private void Drift() {
		driftTime -= Time.deltaTime;
		if (driftTime <= 0) {
			state = 2;
		}

		if (direction < 0 && transform.position.x > 6.5f) {

			direction = 1;
		} else if (direction > 0 && transform.position.x < -6.5f) {
			direction = -1;
		}

		transform.Translate(Time.deltaTime * speed * direction, 0, 0);
	}
}
