using UnityEngine;
using UnityEngine.UI;

public abstract class UpgradeButton : MonoBehaviour {

	protected BulletStockpile _bulletStockpile;
	protected Button button;
	protected Text costText;
	
	protected int _cost;
	protected float _costIncreaseRate;

	protected void Start () {
		_bulletStockpile = GameObject.FindWithTag("GameController").GetComponent<BulletStockpile>();
		button = GetComponent<Button>();
		button.onClick.AddListener(BuyUpgrade);
		costText = transform.Find("Cost").GetComponent<Text>();
		costText.text = getCostText();
	}
	
	private void Update () {
		button.interactable = _bulletStockpile.GetBulletCount() >= _cost;
	}

	private string getCostText() {
		return "Cost: " + _cost + " bullets";
	}

	public void BuyUpgrade() {
		int bulletCount = _bulletStockpile.GetBulletCount();
		if (bulletCount >= _cost) {
			_bulletStockpile.IncrementBulletCount(-_cost);
			_cost = (int)(_cost * _costIncreaseRate);
			costText.text = getCostText();
			GainBenefit();
		}
	}

	protected abstract void GainBenefit();
}
