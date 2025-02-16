using Unity.Cinemachine;
using UnityEngine;

public class CameraShakeManager : MonoBehaviour {
	public static CameraShakeManager instance;

	[SerializeField] private float globalShakeForce = 1f;
	[SerializeField] private CinemachineCamera cinemaCam;
	private Transform player;

	private void Awake() {
		if (instance == null) instance = this;
	}

	private void Update() {
		if (player == null) {
			player = GameObject.Find("Player").transform;
			if (player != null) {
				Camera.main.transform.position = player.position;
				cinemaCam.transform.position = player.position;
			}
		} else {
			cinemaCam.Follow = player;
		}
	}

	public void CameraShake(CinemachineImpulseSource impualseSource) {
		impualseSource.GenerateImpulseWithForce(globalShakeForce);
	}
}
