using UnityEngine;
using UnityEngine.SceneManagement;

public class PoisonScript : CollectableScript {
	private StaminaScript staminaScript;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start() {
		player = GameObject.FindWithTag("Player");
		collectTransform = player.transform.Find("Collect");
		if (player == null) Debug.Log("Player NOT found"); else staminaScript = player.GetComponent<StaminaScript>();
	}

	public void gainStamina() {
		FindFirstObjectByType<AudioManager>().Play("PoisonDrink");
		staminaScript.curStamina = 100;
		staminaScript.SetValue(staminaScript.curStamina);
		collected = false;
		ResetCollectionState();
		Destroy(gameObject);
	}
	void Update() {
		CheckClosestCollectable();
		HandleCollectionInput();
		HandleCollection();

		if (collected && Input.GetMouseButtonDown(0)) {
			gainStamina();
		}
	}
}
