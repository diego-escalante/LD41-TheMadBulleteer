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
	private SoundController soundController;

	protected int state;
	private bool entered;

	//Attack fields.
	protected float timeTillShoot;

	// Entrance fields.
	private float yStop;

	public GameObject Explosion;
	public Color ExpColor;

	private PlayerShip playerShip;
	private Collider2D playerColl;

	// Use this for initialization
	protected void Awake () {
		bulletStockpile = GameObject.FindWithTag("GameController").GetComponent<BulletStockpile>();
		soundController = GameObject.FindWithTag("GameController").GetComponent<SoundController>();
		enemyRenderer = GetComponent<SpriteRenderer>();
		gameObject.name = "Enemy";

		//Entrance setup.
		yStop = Random.Range(2f, 4.5f);
		transform.position = new Vector3(Random.Range(-6.5f, 6.5f), 5.5f, 0);

		//rof setup.
		timeTillShoot = 1/rof;

		playerShip = GameObject.FindWithTag("Player").GetComponent<PlayerShip>();
		playerColl = GameObject.FindWithTag("Player").GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		EnemyAction();
		DeleteWhenLeaving();
		CheckCollisionWithPlayer();
	}

	public void TakeDamage(float damage) {
		Camera.main.GetComponent<CamShaker>().tinyShake();
		soundController.playTinyHit();
		ParticleSystem.MainModule ps = Instantiate(Explosion, transform.position, Quaternion.identity).GetComponent<ParticleSystem>().main;
		ps.startColor = ExpColor;
		if (health > damage) {
			// enemyRenderer.color = Color.red;
			// Invoke("ClearRed", 0.15f);
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

	private void CheckCollisionWithPlayer() {
		if (!playerColl.enabled) {
			return;
		}

		if (Vector2.Distance(transform.position, playerColl.transform.position) < 0.5f) {
			TakeDamage(1000f);
			playerShip.TakeDamage(1000f);
		}

	}
}
