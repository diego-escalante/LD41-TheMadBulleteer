using UnityEngine;

public class Enemy1 : EnemyBase {

	private float pauseTime = 10f;

	public override void EnemyAction() {
		switch (state) {
			// Entrance
			case 0:
				Entrance();
				break;
			// Pause
			case 1:
				Pause();
				break;
			// Departure
			case 2:
				Departure();
				break;
		}

		// If not entering, it can attack.
		if (state == 1) {
			Attack();
		}
	}

	private void Pause() {
		pauseTime -= Time.deltaTime;
		if (pauseTime <= 0) {
			state = 2;
		}
	}
}
