using UnityEngine;
using UnityEngine.UI;

public class StaminaScript : UIBarsScript {
	public static StaminaScript instance;

	private GameObject fill;
	public float curStamina;
	public float maxStamina;
	public float startingValue;
	public GameObject canvas;

	private void Awake() {
		instance = this;
		curStamina = startingValue;
		
		SetMaxValue(maxStamina,startingValue);
		slider = canvas.transform.Find("StaminaBar").gameObject.GetComponent<Slider>();
		fill = slider.transform.Find("Fill").gameObject;
	}
	private void Update() {
		if (curStamina == 0) fill.SetActive(false); else fill.SetActive(true);
		curStamina = Mathf.Min(curStamina, 100f);
	}
}
