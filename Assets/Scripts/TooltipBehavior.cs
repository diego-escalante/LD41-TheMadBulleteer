using UnityEngine;

public class TooltipBehavior : MonoBehaviour {

	private RectTransform rect;
	private RectTransform parentRect;

	private void Start() {
		rect = GetComponent<RectTransform>();
		parentRect = transform.parent.GetComponent<RectTransform>();
	}
	
	public void Update() {
		Vector3 viewportPoint = GameObject.FindWithTag("UICamera")
									   .GetComponent<Camera>()
									   .ScreenToViewportPoint(Input.mousePosition);

	
	   	float x = parentRect.rect.width*viewportPoint.x - parentRect.rect.width/2 + 10;
	   	if (viewportPoint.x > 0.5) {
	   		x -= rect.rect.width + 20;
	   	}
	   	float y = parentRect.rect.height*viewportPoint.y - parentRect.rect.height/2 - 10;
	   	if (viewportPoint.y < 0.25) {
	   		y += rect.rect.height + 20;
	   	}

		rect.localPosition = new Vector3(x, y, 0);
	}
}
