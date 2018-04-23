using UnityEngine;

public class BuyBombButton : UpgradeButton { 

	private PlayerShip playerShip;

	private new void OnEnable() {
		_cost = 100;
		_costIncreaseRate = 2f;
		playerShip = GameObject.FindWithTag("Player").GetComponent<PlayerShip>();
		base.OnEnable();
	}

	protected override void GainBenefit() {
		playerShip.IncreaseBombs(1);
	}
}
