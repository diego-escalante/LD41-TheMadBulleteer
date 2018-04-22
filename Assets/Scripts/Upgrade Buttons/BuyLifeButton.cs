using UnityEngine;

public class BuyLifeButton : UpgradeButton { 

	private PlayerShip playerShip;

	private new void Start () {
		_cost = 100;
		_costIncreaseRate = 2.5f;
		playerShip = GameObject.FindWithTag("Player").GetComponent<PlayerShip>();
		base.Start();
	}

	protected override void GainBenefit() {
		playerShip.IncreaseLives(1);
	}
}
