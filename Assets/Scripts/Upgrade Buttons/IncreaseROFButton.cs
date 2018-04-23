using UnityEngine;

public class IncreaseROFButton : UpgradeButton { 

	private PlayerShip playerShip;
	private int buysLeft = 20;

	private new void OnEnable () {
		_cost = 50;
		_costIncreaseRate = 1.5f;
		buysLeft = 20;
		playerShip = GameObject.FindWithTag("Player").GetComponent<PlayerShip>();
		base.OnEnable();
	}

	protected override void GainBenefit() {
		playerShip.IncreaseRof(1f);
		buysLeft--;
		if (buysLeft == 0) {
			costText.text = "MAXED";
			button.interactable = false;
			enabled = false;
		}
	}
}
