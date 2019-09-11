using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidWaveController : MonoBehaviour
{
	private AsteroidSpawner _asteroidSpawner;

	[SerializeField]
	private float _waveDuration = 20f;

	[SerializeField]
	private float _breakDuration = 5f;

	[SerializeField]
	private float _cooldownDuration = 4f;

	public event System.Action<int> OnWaveStarted;
	public event System.Action<int> OnBreakStarted;
	public event System.Action OnPrepareStarted;

	public int CurrentWaveNumber { get; private set; }

	void Start()
	{
		CurrentWaveNumber = 1;
		_asteroidSpawner = FindObjectOfType<AsteroidSpawner>();
		StartCoroutine(AsteroidWaveControllerCoroutine());
	}

	private IEnumerator AsteroidWaveControllerCoroutine()
	{
		var spawner = FindObjectOfType<AsteroidSpawner>();

		while (true)
		{
			// 1 sec for preparation
			if (OnPrepareStarted != null)
				OnPrepareStarted.Invoke();
			yield return new WaitForSeconds(1f);

			// START
			if (OnWaveStarted != null)
				OnWaveStarted.Invoke(CurrentWaveNumber);

			spawner.AsteroidTypeLevel = CurrentWaveNumber;

			spawner.Spawning = true;
			yield return new WaitForSeconds(_waveDuration);

			spawner.Spawning = false;

			yield return new WaitForSeconds(_cooldownDuration);

			CurrentWaveNumber++;
			if (OnBreakStarted != null)
				OnBreakStarted.Invoke(CurrentWaveNumber);
			yield return new WaitForSeconds(_breakDuration);

			_asteroidSpawner.IncreaseAsteroidsSpeed();
		}
	}
}
