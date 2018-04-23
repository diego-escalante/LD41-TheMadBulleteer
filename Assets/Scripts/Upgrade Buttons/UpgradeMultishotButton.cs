using UnityEngine;

public class UpgradeMultishotButton : UpgradeButton { 

	private PlayerShip playerShip;
	private int buysLeft = 3;

	private new void OnEnable () {
		_cost = 500;
		_costIncreaseRate = 2f;
		buysLeft = 3;
		playerShip = GameObject.FindWithTag("Player").GetComponent<PlayerShip>();
		base.OnEnable();
	}

	protected override void GainBenefit() {
		playerShip.IncreaseMultishot();
		buysLeft--;
		if(buysLeft == 0) {
			costText.text = "MAXED";
			button.interactable = false;
			enabled = false;
		}
	}
}
