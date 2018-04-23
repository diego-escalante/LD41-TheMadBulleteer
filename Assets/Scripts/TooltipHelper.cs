using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TooltipHelper : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	[Multiline]
	public string _tooltipText;
	private Text tooltip;
	private Image tooltipBackground;

	private void Start() {
		tooltip = GameObject.FindWithTag("Tooltip").GetComponentInChildren<Text>();
		tooltipBackground = GameObject.FindWithTag("Tooltip").GetComponent<Image>();
	}

	public void OnPointerEnter(PointerEventData eventData) {
		tooltip.text = _tooltipText;
		tooltipBackground.color = new Color(0,0,0,0.6f);
	}

	public void OnPointerExit(PointerEventData eventData) {
		tooltip.text = "";
		tooltipBackground.color = Color.clear;
	}

}
