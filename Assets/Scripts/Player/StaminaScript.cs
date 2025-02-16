using UnityEngine;
using UnityEngine.UI;

public class StaminaScript : UIBarsScript {
	public static StaminaScript instance;

	[HideInInspector] public GameObject uiManage;
	[HideInInspector] public GameObject staminaCanvas;
	private GameObject fill;
	public float curStamina;
	public float maxStamina;
	public float startingValue;

	void Start() {
		instance = this;
		curStamina = startingValue;

		uiManage = GameObject.Find("UIMannge");
		if (uiManage == null) {
			Debug.LogError("UI Manage reference is missing in StaminaScript!");
			return;
		}

		GameManngeScript gameManage = uiManage.GetComponent<GameManngeScript>();
		if (gameManage == null) {
			Debug.LogError("GameManngeScript component is missing on UI Manage!");
			return;
		}

		staminaCanvas = gameManage.staminaCanvas;
		slider = gameManage.staminaBar;

		if (slider == null) {
			Debug.LogError("Stamina bar (Slider) is missing in GameManngeScript!");
			return;
		}

		fill = slider.transform.Find("Fill").gameObject;
		SetMaxValue(maxStamina, startingValue);
	}

	private void Update() {
		if (curStamina == 0) fill.SetActive(false); else fill.SetActive(true);
		curStamina = Mathf.Min(curStamina, 100f);
	}
}
