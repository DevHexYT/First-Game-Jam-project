using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManngeScript : MonoBehaviour {
	public static GameManngeScript instance;
	public int enemysCount;

	private GameObject staminaCanvas;
	private GameObject winCanvas;

	private void Awake() {
		winCanvas = transform.Find("Win").gameObject;
		staminaCanvas = transform.Find("Stamina Canvas").gameObject;
	}

	private void Update() {
		CheckEnemys();
	}
	public void AddEnemy() {
		enemysCount++;
	}
	public void RemoveEnemy() {
		enemysCount--;
	}
	public void CheckEnemys() {
		if (enemysCount == 0) {
			winCanvas.SetActive(true);
			staminaCanvas.SetActive(false);
			PlayerScript plrScript = GameObject.Find("Player").GetComponent<PlayerScript>();
			plrScript.winLevel = true;
		}
	}
	public void NextLevel() {
		int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

		if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) {
			SceneManager.LoadScene(nextSceneIndex);
		} else {
			SceneManager.LoadScene("MainMenu");
		}
	}
}