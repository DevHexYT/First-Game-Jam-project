using UnityEngine;
using UnityEngine.UI;

public class UIBarsScript : MonoBehaviour{

	[HideInInspector] public Slider slider;

	public void SetMaxValue(float maxValue,float startingValue) {
		slider.maxValue = maxValue;
		slider.value = startingValue;
	}

	public void SetBarPosition(Vector3 position, Quaternion rotation) {
		slider.transform.position = Camera.main.WorldToScreenPoint(position);
		slider.transform.rotation = rotation;
	}

	public void SetValue(float value) {
		slider.value = value;
	}
}
