using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject E1, E2, E3, E4, E5;
	private float E1SpawnRate, E1SpawnDelta, 
				  E2SpawnRate, E2SpawnDelta, 
				  E3SpawnRate, E3SpawnDelta, 
				  E4SpawnRate, E4SpawnDelta, 
				  E5SpawnRate, E5SpawnDelta;

	private void Start() {
		Invoke("Phase1", 10);
	}

	private void E1Spawner() {
		Instantiate(E1);
		E1SpawnRate = Mathf.Max(E1SpawnRate - E1SpawnDelta, 1f);
		Invoke("E1Spawner", E1SpawnRate);
	}

	private void E2Spawner() {
		Instantiate(E2);
		E2SpawnRate = Mathf.Max(E2SpawnRate - E2SpawnDelta, 1f);
		Invoke("E2Spawner", E2SpawnRate);
	}

	private void E3Spawner() {
		Instantiate(E3);
		E3SpawnRate = Mathf.Max(E3SpawnRate - E3SpawnDelta, 1f);
		Invoke("E3Spawner", E3SpawnRate);
	}

	private void E4Spawner() {
		Instantiate(E4);
		E4SpawnRate = Mathf.Max(E4SpawnRate - E4SpawnDelta, 1f);
		Invoke("E4Spawner", E4SpawnRate);
	}

	private void E5Spawner() {
		Instantiate(E5);
		E5SpawnRate = Mathf.Max(E5SpawnRate - E5SpawnDelta, 1f);
		Invoke("E5Spawner", E5SpawnRate);
	}

	private void Phase1() {
		Debug.Log("Phase 1");
		CancelInvoke();
		
		E1SpawnRate = 5f;
		E1SpawnDelta = 0.1f;
		E1Spawner();

		Invoke("Phase2", 60);
	}

	private void Phase2() {
		Debug.Log("Phase 2");
		CancelInvoke();
		
		E1SpawnRate = 6f;
		E1SpawnDelta = 0.1f;
		Invoke("E1Spawner", 20);
		
		E2SpawnRate = 5f;
		E2SpawnDelta = 0.2f;
		Invoke("E2Spawner", 10);

		Invoke("Phase3", 70);
	}

	private void Phase3() {
		Debug.Log("Phase 3");
		CancelInvoke();

		E1SpawnRate = 10f;
		E1SpawnDelta = 0.5f;
		Invoke("E1Spawner", 20);
		
		E2SpawnRate = 5f;
		E2SpawnDelta = 0.1f;
		Invoke("E2Spawner", 20);

		E3SpawnRate = 5f;
		E3SpawnDelta = 0.1f;
		Invoke("E3Spawner", 10);

		Invoke("Phase4", 130);
	}

	private void Phase4() {
		Debug.Log("Phase 4");
		CancelInvoke();

		E1SpawnRate = 10f;
		E1SpawnDelta = 0.5f;
		Invoke("E1Spawner", 20);
		
		E2SpawnRate = 5f;
		E2SpawnDelta = 0.1f;
		Invoke("E2Spawner", 20);

		E3SpawnRate = 5f;
		E3SpawnDelta = 0.1f;
		Invoke("E3Spawner", 20);

		E4SpawnRate = 5f;
		E4SpawnDelta = 0.1f;
		Invoke("E4Spawner", 10);

		Invoke("Phase5", 130);
	}

	private void Phase5() {
		Debug.Log("Phase 5");
		CancelInvoke();

		E1SpawnRate = 10f;
		E1SpawnDelta = 0.5f;
		Invoke("E1Spawner", 20);
		
		E2SpawnRate = 5f;
		E2SpawnDelta = 0.1f;
		Invoke("E2Spawner", 20);

		E3SpawnRate = 5f;
		E3SpawnDelta = 0.1f;
		Invoke("E3Spawner", 20);

		E4SpawnRate = 5f;
		E4SpawnDelta = 0.1f;
		Invoke("E4Spawner", 20);

		E5SpawnRate = 5f;
		E5SpawnDelta = 0.1f;
		Invoke("E5Spawner", 10);

		Invoke("FinalPhase", 130);
	}

	private void FinalPhase() {
		Debug.Log("Endless!");
		CancelInvoke();

		E1SpawnRate = 10f;
		E1SpawnDelta = 0.5f;
		Invoke("E1Spawner", 10);
		
		E2SpawnRate = 5f;
		E2SpawnDelta = 0.1f;
		Invoke("E2Spawner", 10);

		E3SpawnRate = 5f;
		E3SpawnDelta = 0.1f;
		Invoke("E3Spawner", 10);

		E4SpawnRate = 5f;
		E4SpawnDelta = 0.1f;
		Invoke("E4Spawner", 10);

		E5SpawnRate = 5f;
		E5SpawnDelta = 0.1f;
		Invoke("E5Spawner", 10);
	}

}
