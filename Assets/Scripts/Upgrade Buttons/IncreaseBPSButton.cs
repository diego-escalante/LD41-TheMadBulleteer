public class IncreaseBPSButton : UpgradeButton {

	private new void OnEnable() {
		_cost = 50;
		_costIncreaseRate = 1.1f;
		base.OnEnable();
	}

	protected override void GainBenefit() {
		_bulletStockpile.IncrementBps(2);
	}

}
