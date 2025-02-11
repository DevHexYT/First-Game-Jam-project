using UnityEngine;
using UnityEngine.UI;

public class UIBarsScript : MonoBehaviour{
	[HideInInspector] public Slider slider;

	public void SetMaxValue(float maxValue,float startingValue) {
		slider.maxValue = maxValue;
		slider.value = startingValue;
	}

	public void SetValue(float value) {
		slider.value = value;
	}
}
