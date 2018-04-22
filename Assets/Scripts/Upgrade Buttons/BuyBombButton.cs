using UnityEngine;

public class BuyBombButton : UpgradeButton { 

	private PlayerShip playerShip;

	private new void Start () {
		_cost = 100;
		_costIncreaseRate = 2f;
		playerShip = GameObject.FindWithTag("Player").GetComponent<PlayerShip>();
		base.Start();
	}

	protected override void GainBenefit() {
		playerShip.IncreaseBombs(1);
	}
}
