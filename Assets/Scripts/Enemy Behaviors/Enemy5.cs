using UnityEngine;

public class Enemy5 : EnemyBase {

	private Transform player;

	public  void Start() {
		player = GameObject.FindWithTag("Player").transform;
	}

	public override void EnemyAction() {
		Seek();
		Attack();
	}

	private void Seek() {
		transform.rotation = Quaternion.RotateTowards(transform.rotation, calculateRot(), speed * Time.deltaTime * 100);
		transform.Translate(0, speed * Time.deltaTime, 0);
	}

	private Quaternion calculateRot() {
    	var dir = player.position - transform.position;
    	var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;    
    	return Quaternion.AngleAxis(angle - 90, Vector3.forward);
    } 
}