using UnityEngine;

public class ShipSpeed : MonoBehaviour, IUpgradable
{
	private AsteroidSpawner _asteroidSpawner;

	#region IUpgradable

	public int MaxLevel
	{
		get => 5;
	}

	public int CurrentLevel { get; private set; }

	public int UpgradeCost
	{
		get => 300 + CurrentLevel * 50;
	}

	public void Upgrade()
	{
		CurrentLevel++;
		_asteroidSpawner.IncreaseShipSpeed();
	}

	#endregion

	void Start()
	{
		CurrentLevel = 0;
		_asteroidSpawner = FindObjectOfType<AsteroidSpawner>();
	}
}
