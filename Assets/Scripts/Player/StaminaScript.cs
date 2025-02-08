using UnityEngine;

public class StaminaScript : UIBarsScript {
	public static StaminaScript instance;

	public GameObject fill;
	public float curStamina;
	public float maxStamina;
	public float startingValue;
	public GameObject canvas;

	private void Awake() {
		instance = this;
		curStamina = startingValue;

		SetMaxValue(maxStamina,startingValue);
	}
	private void Update() {
		if (curStamina == 0) fill.SetActive(false); else fill.SetActive(true);
		curStamina = Mathf.Min(curStamina, 100f);
	}
}
