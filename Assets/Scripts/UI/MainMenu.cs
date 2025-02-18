using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void ResetScene() {
		Scene currentScene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(currentScene.name);
	}
	public void ChooseScene(string sceneName) {
		SceneManager.LoadScene(sceneName);
	}

	public void OpenLevels() {
		SceneManager.LoadScene("Levels");
	}

	public void OpenMainMenu() {
		SceneManager.LoadScene("MainMenu");
	}
	public void openYotube(string url) {
		Application.OpenURL(url);
	}
}
