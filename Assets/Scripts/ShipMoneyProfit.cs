using UnityEngine;

public class ShipMoneyProfit : MonoBehaviour, IUpgradable
{
	#region IUpgradable

	public int MaxLevel
	{
		get => 1;
	}

	public int CurrentLevel { get; private set; }

	public int UpgradeCost
	{
		get => 1000;
	}

	public void Upgrade() => CurrentLevel++;

	#endregion

	void Start() => CurrentLevel = 0;

	public int GetMoneyMultiplier() => CurrentLevel + 1;
}
