using UnityEngine;

public class BulletScript : MonoBehaviour {
	[HideInInspector] public float maxRange;
	[HideInInspector] public Vector2 startPos;
	[SerializeField] private ParticleSystem particlePrefab;

	private void OnTriggerEnter2D(Collider2D other) {
		ParticleSystem particle = Instantiate(particlePrefab, transform.position, Quaternion.identity);
		Destroy(particle.gameObject, 1.5f);
		Destroy(gameObject);
	}
	public void CheckRange(float maxRange, Vector2 startPos) {
		float dis = Mathf.Abs(Vector2.Distance(startPos, transform.position));

		if (dis > maxRange) {
			ParticleSystem particle = Instantiate(particlePrefab,transform.position,Quaternion.identity);
			Destroy(particle.gameObject,1.5f);
			Destroy(gameObject);
		}
	}
	private void Start() {
		startPos = transform.position;
	}

	private void Update() {
		CheckRange(maxRange, startPos);
	}
}
