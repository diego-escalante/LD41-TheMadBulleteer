using UnityEngine;

public class UpgradeMultishotButton : UpgradeButton { 

	private PlayerShip playerShip;
	private int buysLeft = 3;

	private new void Start () {
		_cost = 500;
		_costIncreaseRate = 2f;
		playerShip = GameObject.FindWithTag("Player").GetComponent<PlayerShip>();
		base.Start();
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
