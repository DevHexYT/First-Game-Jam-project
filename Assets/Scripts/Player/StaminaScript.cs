using UnityEngine;
using UnityEngine.UI;

public class StaminaScript : UIBarsScript {
	public static StaminaScript instance;

	[HideInInspector] private GameObject fill;
	public float curStamina;
	public float maxStamina;
	public float startingValue;
	public GameObject staminaCanvas;

	private void Awake() {
		instance = this;
		curStamina = startingValue;
		
		SetMaxValue(maxStamina,startingValue);
		fill = slider.transform.Find("Fill").gameObject;
	}
	private void Update() {
		if (curStamina == 0) fill.SetActive(false); else fill.SetActive(true);
		curStamina = Mathf.Min(curStamina, 100f);
	}
}
