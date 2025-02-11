using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectableScript : MonoBehaviour {
	public float radius = 1.5f;
	public LayerMask playerMask;

	[HideInInspector] public Transform collectTransform;
	[HideInInspector] public GameObject player;

	public bool canCollect = false;
	public bool collected = false;

	private static CollectableScript closestCollectable;
	private static bool isItemCollected = false;

	private void OnEnable() {
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnDisable() {
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		ResetCollectionState();
	}

	public void CheckClosestCollectable() {
		if (isItemCollected) {
			canCollect = false;
			return;
		}

		Collider2D collider = Physics2D.OverlapCircle(transform.position, radius, playerMask);
		if (collider != null && collider.gameObject == player) {
			float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
			if (closestCollectable == null || distanceToPlayer < Vector2.Distance(closestCollectable.transform.position, player.transform.position)) {
				closestCollectable = this;
				canCollect = true;
			} else {
				canCollect = false;
			}
		} else {
			canCollect = false;
		}
	}

	public void HandleCollectionInput() {
		if (closestCollectable == this && Input.GetKeyDown(KeyCode.E)) {
			if (!collected && !isItemCollected) {
				collected = true;
				isItemCollected = true;
			} else if (collected) {
				ResetCollectionState();
			}
		}
	}

	public void HandleCollection() {
		if (collected) {
			transform.position = collectTransform.position;
			transform.rotation = collectTransform.rotation;
		} else ResetCollectionState();
	}

	public void ResetCollectionState() {
		collected = false;
		isItemCollected = false;
		closestCollectable = null;
		canCollect = false;
	}
}
