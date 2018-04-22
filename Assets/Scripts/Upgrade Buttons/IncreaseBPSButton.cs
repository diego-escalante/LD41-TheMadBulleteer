public class IncreaseBPSButton : UpgradeButton {

	private new void Start() {
		_cost = 50;
		_costIncreaseRate = 1.1f;
		base.Start();
	}

	protected override void GainBenefit() {
		_bulletStockpile.IncrementBps(2);
	}

}
