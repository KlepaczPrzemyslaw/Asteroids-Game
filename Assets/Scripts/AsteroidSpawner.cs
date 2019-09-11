using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidSpawner : MonoBehaviour
{
	[SerializeField]
	private AsteroidType[] _asteroidTypes;
	
	[SerializeField]
	private GameObject _asteroidPrefab = null;

	[SerializeField]
	private float _asteroidMinSpawningTime = 1.2f;

	[SerializeField]
	private float _asteroidMaxSpawningTime = 2.2f;

	public bool Spawning;
	public int AsteroidTypeLevel { get; set; }
	public int AsteroidTypeRange { get; set; }

	void Start()
	{
		AsteroidTypeLevel = 0;
		AsteroidTypeRange = 5;
		StartCoroutine(SpawnCoroutine());
	}

	public void IncreaseAsteroidsSpeed()
	{
		_asteroidMinSpawningTime -= 0.1f;
		_asteroidMinSpawningTime = Mathf.Clamp(_asteroidMinSpawningTime, 0, int.MaxValue);
		_asteroidMaxSpawningTime -= 0.1f;
		_asteroidMaxSpawningTime = Mathf.Clamp(_asteroidMaxSpawningTime, 0, int.MaxValue);
	}

	public void IncreaseShipSpeed()
	{
		_asteroidMinSpawningTime += 0.1f;
		_asteroidMaxSpawningTime += 0.1f;
	}

	private IEnumerator SpawnCoroutine()
	{
		while (true)
		{
			while (Spawning)
			{
				SpawnAsteroid();
				yield return new WaitForSeconds(Random.Range(
					_asteroidMinSpawningTime, _asteroidMaxSpawningTime));
			}

			yield return new WaitForEndOfFrame();
		}
	}

	private AsteroidType GetRandomAsteroid()
	{
		var index = AsteroidTypeLevel + Random.Range(- AsteroidTypeRange, AsteroidTypeRange);
		index = Mathf.Clamp(index, 0, _asteroidTypes.Length - 1);

		return _asteroidTypes[index];
	}

	private void SpawnAsteroid()
	{
		var obj = Instantiate(_asteroidPrefab, transform.position, Quaternion.identity);
		obj.transform.position += Vector3.right * Random.Range(-2.2f, 2.2f);
		obj.GetComponent<Asteroid>().Configure(GetRandomAsteroid());
	}
}
