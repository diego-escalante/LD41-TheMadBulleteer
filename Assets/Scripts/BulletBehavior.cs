using UnityEngine;

public class BulletBehavior : MonoBehaviour {

	public float _speed = 20f;
	public bool _fromEnemy;
	private Collider2D[] overlappingColliders = new Collider2D[1];
	private int damage = 1;

	private LayerMask layerMask;

	void Start() {
		layerMask = LayerMask.GetMask(_fromEnemy ? "Player" : "Enemies");
		GameObject.FindWithTag("GameController").GetComponent<SoundController>().playShotFired();
	}
	
	void Update () {
		// Detect if bullet hit anything. Destroy both the bullet and the object if it did.
		Vector3 deltaPos = new Vector3(0, Time.deltaTime * _speed, 0);
		if (Physics2D.OverlapPointNonAlloc(transform.position + deltaPos, overlappingColliders, layerMask) > 0) {
			overlappingColliders[0].GetComponent<IDamageable>().TakeDamage(damage);
			Destroy(gameObject);
		}


		transform.Translate(deltaPos);
	}

	void OnBecameInvisible() {
		Destroy(gameObject);
	}

	public void SetDamage(int damage) {
		this.damage = damage;
	}
}
