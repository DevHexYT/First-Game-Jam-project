using UnityEngine;

public class BulletScript : MonoBehaviour {
	private void OnTriggerEnter2D(Collider2D other) {
		Destroy(gameObject,0.01f);
	}
}
