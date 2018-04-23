using UnityEngine;

public class IncreaseDMGButton : UpgradeButton {

	private PlayerShip playerShip;

	private new void OnEnable() {
		_cost = 200;
		_costIncreaseRate = 1.5f;
		playerShip = GameObject.FindWithTag("Player").GetComponent<PlayerShip>();
		base.OnEnable();
	}

	protected override void GainBenefit() {
		playerShip.IncreaseDamage(1);
	}

}
