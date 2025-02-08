using UnityEngine;
using UnityEngine.SceneManagement;

public class GunsScript : CollectableScript {

	[HideInInspector] 
	public Transform firePoint;
	public GameObject bulletPrefab;

	public float fireRate;
	public float fireSpeed;

	private float fireTime;

	public void Shoot() {
		GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

		Vector2 shootDirection = firePoint.right;
		rb.AddForce(shootDirection * fireSpeed, ForceMode2D.Impulse);
	}

	private void Start() {
		firePoint = transform.Find("FirePoint");
		player = GameObject.FindWithTag("Player");
		collectTransform = player.transform.Find("Collect");
	}


	private void Update() {
		CheckClosestCollectable();
		HandleCollectionInput();
		HandleCollection();

		if (collected) {
			if(Input.GetButton("Fire1") && Time.time >= fireTime) {
				Shoot();
				fireTime = Time.time + 1f / fireRate;
			}
		}
	}

}
