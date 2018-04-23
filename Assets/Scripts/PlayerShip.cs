using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PlayerShip : MonoBehaviour, IDamageable {

	public GameObject _bulletPrefab;
	public GameObject _rateOfFire;
	public GameObject _damage;
	public GameObject _bombCounter;
	public GameObject _livesCounter;
	public GameObject _SRTCounter;

	private Text rateOfFireText;
	private Text damageText;
	private Text bombText;
	private Text livesText;
	private Text srtText;

	private const float speed = 5f;
	private float rof = 1f; //Rate of Fire
	private float timeTillShoot;
	private int damage = 1;
	private int bombCount;
	private int lives = 3;
	private int srt = 60; //Shield Regeneration Time in seconds.
	private int multishotLevel;
	private int shotCount;

	private BulletStockpile bulletStockpile;
	private ShieldBehavior shieldBehavior;
	private Renderer shipRender;
	private Collider2D shipColl;
	private SoundController soundController;

	public GameObject Explosion;
	public Color ExpColor;

	private void OnEnable() {
		
		lives = 3;
		bombCount = 1;
		damage = 1;
		rof = 1f;
		srt = 60;
		
		bulletStockpile = GameObject.FindWithTag("GameController").GetComponent<BulletStockpile>();
		bulletStockpile.SetBps(0);
		rateOfFireText = _rateOfFire.GetComponent<Text>();
		rateOfFireText.text = rof.ToString();
		damageText = _damage.GetComponent<Text>();
		damageText.text = damage.ToString();
		bombText = _bombCounter.GetComponent<Text>();
		bombText.text = bombCount.ToString();
		livesText = _livesCounter.GetComponent<Text>();
		livesText.text = lives.ToString();
		srtText = _SRTCounter.GetComponent<Text>();
		srtText.text = srt.ToString();

		shieldBehavior = GetComponent<ShieldBehavior>();
		shipRender = GetComponent<SpriteRenderer>();
		shipColl = GetComponent<Collider2D>();
		soundController = GameObject.FindWithTag("GameController").GetComponent<SoundController>();
	} 
	
	// Update is called once per frame
	void Update () {
		// Reset timeTillShoot; allows the player to spam spacebar during early game to shoot more bullets.
		if (Input.GetButtonDown("Fire")) {
			timeTillShoot = 0;
		}

		// Automatically shoot a bullet based on the rate of fire.
		if (Input.GetButton("Fire")) {
			timeTillShoot -= Time.deltaTime;
			if (timeTillShoot <= 0) {
				FireBullet();
			}
		}

		// Bombs
		if (Input.GetButtonDown("Bomb")) {
			//Fire Bomb
			FireBomb();
		}

		// Move the player based on inputs.
		transform.Translate(Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed, 
							Input.GetAxisRaw("Vertical") * Time.deltaTime * speed, 
							0);

		// Keep the player inside the Camera's field of view.
		Vector3 pos = transform.position;
		pos.x = Mathf.Clamp(pos.x, -6.6f, 6.6f);
		pos.y = Mathf.Clamp(pos.y, -5, 5);
		transform.position = pos;

		// if (Input.GetKeyDown(KeyCode.O)) {
		// 	Camera.main.GetComponent<CamShaker>().smallShake();
		// }

		// if (Input.GetKeyDown(KeyCode.P)) {
		// 	Camera.main.GetComponent<CamShaker>().bigShake();
		// }
	}

	private void FireBullet() {
		if (bulletStockpile.GetBulletCount() >= damage) {
			bulletStockpile.IncrementBulletCount(-damage);
			GameObject newBullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
			newBullet.GetComponent<BulletBehavior>().SetDamage(damage);
			timeTillShoot = 1/rof;
			FireMultishot();
		}
	}

	private void FireMultishot() {
		if (multishotLevel == 0) {
			return;
		}

		shotCount++;
		if (shotCount >= 12-(3*multishotLevel)) {
			shotCount = 0;
		}

		if (shotCount == 0) {
			GameObject newBullet = Instantiate(_bulletPrefab, transform.position, Quaternion.AngleAxis(10, Vector3.forward));
			newBullet.GetComponent<BulletBehavior>().SetDamage(damage);
			newBullet = Instantiate(_bulletPrefab, transform.position, Quaternion.AngleAxis(-10, Vector3.forward));
			newBullet.GetComponent<BulletBehavior>().SetDamage(damage);
		}
	}

	public void IncreaseRof(float delta) {
		rof += delta;
		rateOfFireText.text = rof.ToString();
	}

	public void TakeDamage(float damage) {
		ParticleSystem.MainModule exp;
		if (shieldBehavior.CanAbsorbDamage()) {
			Camera.main.GetComponent<CamShaker>().smallShake();
			soundController.playShieldUsed();
			exp = Instantiate(Explosion, transform.position, Quaternion.identity).GetComponent<ParticleSystem>().main;
			exp.startColor = ExpColor;
			return;
		}
		if (lives > 0) {
			IncreaseLives(-1);
			StartCoroutine("Recover");
			Camera.main.GetComponent<CamShaker>().smallShake();
			soundController.playSmallHit();
			exp = Instantiate(Explosion, transform.position, Quaternion.identity).GetComponent<ParticleSystem>().main;
			exp.startColor = ExpColor;
			return;
		}
		
		Camera.main.GetComponent<CamShaker>().bigShake();
		soundController.playBigHit();
		exp = Instantiate(Explosion, transform.position, Quaternion.identity).GetComponent<ParticleSystem>().main;
		exp.startColor = ExpColor;
		GameObject.FindWithTag("GameController").GetComponent<MenuController>().EndGame(bulletStockpile.GetBulletCount());
	}

	public void IncreaseDamage(int delta) {
		damage += delta;
		damageText.text = damage.ToString();
	}

	private void FireBomb() {
		if (bombCount > 0) {
			IncreaseBombs(-1);
			StartCoroutine("ExplodeBomb");
		}
	}

	public void IncreaseBombs(int delta) {
		bombCount += delta;
		bombText.text = bombCount.ToString();
	}

	public void IncreaseLives(int lives) {
		this.lives += lives;
		livesText.text = this.lives.ToString();
	}

	public void IncreaseSRT(int delta) {
		srt += delta;
		srtText.text = srt.ToString();
		shieldBehavior.setSrt(srt);
	}

	private IEnumerator ExplodeBomb() {
		Camera.main.backgroundColor = Color.white;
		Camera.main.GetComponent<CamShaker>().bigShake();
		soundController.playBigHit();
		float flashDuration = 0.4f;

		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach(GameObject enemy in enemies) {
			enemy.GetComponent<IDamageable>().TakeDamage(9999);
		}
		GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
		foreach(GameObject enemyBullet in enemyBullets) {
			Destroy(enemyBullet);
		}

		for (float flashTime = flashDuration; flashTime > 0; flashTime -= Time.deltaTime) {
			Camera.main.backgroundColor = Color.Lerp(Color.white, Color.black, 1-(flashTime/flashDuration));
			yield return null;
		}

		Camera.main.backgroundColor = Color.black;
	}

	private IEnumerator Recover() {
		shipColl.enabled = false;
		shipRender.enabled = false;
		float recoveryTime = 5f;
		float flashInterval = 0.25f;
		float flashElapsed = 0f;

		for (float elapsedTime = 0; elapsedTime < recoveryTime; elapsedTime += Time.deltaTime) {
			flashElapsed += Time.deltaTime;
			if (flashElapsed >= (!shipRender.enabled ? flashInterval/5 : flashInterval)) {
				flashElapsed = 0;
				shipRender.enabled = !shipRender.enabled;
			}
			yield return null;
		}

		shipRender.enabled = true;
		shipColl.enabled = true;
	}

	public void IncreaseMultishot() {
		multishotLevel++;
	}
}
