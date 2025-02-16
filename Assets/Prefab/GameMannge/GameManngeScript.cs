using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManngeScript : MonoBehaviour {
	public static GameManngeScript instance;
	public static bool gameStarted = false;
	public int enemysCount;

	public GameObject staminaCanvas;
	public Slider staminaBar;
	private GameObject winCanvas;

	private void Awake() {
		winCanvas = transform.Find("Win").gameObject;
		staminaCanvas = transform.Find("StaminaCanvas").gameObject;
		staminaBar = staminaCanvas.transform.Find("StaminaBar").GetComponent<Slider>();
		staminaCanvas.SetActive(true);
		gameStarted = false;
	}

	private void Update() {
		CheckEnemys();
	}
	public void AddEnemy() {
		gameStarted = true;
		enemysCount++;
	}
	public void RemoveEnemy() {
		enemysCount--;
	}
	public void CheckEnemys() {
		if (enemysCount == 0 && gameStarted) {
			winCanvas.SetActive(true);
			staminaCanvas.SetActive(false);
			PlayerScript plrScript = GameObject.Find("Player").GetComponent<PlayerScript>();
			plrScript.winLevel = true;
			gameStarted = false;
			Debug.Log(enemysCount);
			Debug.Log("Won the game");
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