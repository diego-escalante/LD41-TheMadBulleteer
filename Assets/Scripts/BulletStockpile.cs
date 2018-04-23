using UnityEngine;
using UnityEngine.UI;

public class BulletStockpile : MonoBehaviour {

	public GameObject _bulletCounter;
	private Text bulletCounterText;
	public GameObject _bpsCounter;
	private Text bpsCounterText;
	private float bulletCount;
	private long bps; //Bullets per Second
	private float timeDelta;

	// Use this for initialization
	private void Start () {
		bulletCounterText = _bulletCounter.GetComponent<Text>();
		bpsCounterText = _bpsCounter.GetComponent<Text>();
	}
	
	public void IncrementBulletCount() {
		IncrementBulletCount(1);
	}

	public void IncrementBulletCount(float delta) {
		bulletCount += delta;
		int i = (int)bulletCount;
		bulletCounterText.text = i.ToString();
	}

	private void Update () {
		timeDelta += Time.deltaTime;
		IncrementBulletCount(Time.deltaTime * bps);
		if (timeDelta >= 1f) {
			timeDelta -= 1;
		}
	}

	public void IncrementBps() {
		IncrementBps(1);
	}

	public void IncrementBps(long delta) {
		bps += delta;
		bpsCounterText.text = bps.ToString();
	}

	public int GetBulletCount() {
		return (int)bulletCount;
	}

	public void SetBps(long newBps) {
		bps = newBps;
		bpsCounterText.text = bps.ToString();
	}

	public void SetBulletCount(float count) {
		bulletCount = count;
		int i = (int)bulletCount;
		bulletCounterText.text = i.ToString();
	}
}
