using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IDamageable {

	public GameObject _enemyBullet;
	public int bulletScrap = 10;
	public int health = 1;
	public float speed = 3f;
	public float rof = 0.5f;

	private BulletStockpile bulletStockpile;
	private SpriteRenderer enemyRenderer;

	protected int state;
	private bool entered;

	//Attack fields.
	protected float timeTillShoot;

	// Entrance fields.
	private float yStop;

	// Use this for initialization
	protected void Awake () {
		bulletStockpile = GameObject.FindWithTag("GameController").GetComponent<BulletStockpile>();
		enemyRenderer = GetComponent<SpriteRenderer>();
		gameObject.name = "Enemy";

		//Entrance setup.
		yStop = Random.Range(2f, 4.5f);
		transform.position = new Vector3(Random.Range(-6.5f, 6.5f), 5.5f, 0);

		//rof setup.
		timeTillShoot = 1/rof;
	}
	
	// Update is called once per frame
	void Update () {
		EnemyAction();
		DeleteWhenLeaving();
	}

	public void TakeDamage(float damage) {
		if (health > damage) {
			enemyRenderer.color = Color.red;
			Invoke("ClearRed", 0.15f);
			health -= (int)damage;
		} else {
			bulletStockpile.IncrementBulletCount(bulletScrap);
			Destroy(gameObject);
		}
	}

	private void ClearRed() {
		enemyRenderer.color = Color.white;
	}

	private void DeleteWhenLeaving() {
		if(!enemyRenderer.isVisible && entered) {
			Destroy(gameObject);
		} else if (enemyRenderer.isVisible) {
			entered = true;
		}
	}

	public abstract void EnemyAction();

	protected virtual void Entrance() {
		if (transform.position.y > yStop) {
			transform.Translate(0, Time.deltaTime * speed, 0);
		} else {
			state = 1;
		}
	}

	protected virtual void Departure() {
		transform.Translate(0, Time.deltaTime * speed, 0);
	}

	protected virtual void Attack() {
		timeTillShoot -= Time.deltaTime;
		if (timeTillShoot <= 0) {
			timeTillShoot = 1/rof;
			Instantiate(_enemyBullet, transform.position, transform.rotation);
		}
	}
}
