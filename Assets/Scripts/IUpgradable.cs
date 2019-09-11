public interface IUpgradable
{
	int MaxLevel { get; }
	int CurrentLevel { get; }
	int UpgradeCost { get; }

	void Upgrade();
}

