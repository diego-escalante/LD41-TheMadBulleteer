using UnityEngine;

public class Enemy3 : EnemyBase {

	private Transform player;
	private float pointTime = 10f;

	public void Start() {
		player = GameObject.FindWithTag("Player").transform;
	}

	public override void EnemyAction() {
		switch(state) {
			case 0:
				Entrance();
				break;
			case 1:
				Point();
				break;
			case 2:
				Departure();
				break;
		}

		if (state == 1) {
			Attack();
		}
	}

	private void Point() {
		pointTime -= Time.deltaTime;
		if (pointTime <= 0) {
			state = 2;
		}
		transform.rotation = Quaternion.RotateTowards(transform.rotation, calculateRot(), speed * Time.deltaTime * 100);
	}

	private Quaternion calculateRot() {
    var dir = player.position - transform.position;
    var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;    
    return Quaternion.AngleAxis(angle - 90, Vector3.forward);    
  }
}
