using UnityEngine;

public static class GameState
{
	private const string _lastGameKey = "Last_Game";
	private const string _recordKey = "Record";

	public static void SetCurrentResult(int points)
	{
		PlayerPrefs.SetInt(_lastGameKey, points);

		if (GetRecord() < points)
			PlayerPrefs.SetInt(_recordKey, points);
	} 
	
	public static int GetLastResult() => PlayerPrefs.GetInt(_lastGameKey, 0);

	public static int GetRecord() => PlayerPrefs.GetInt(_recordKey, 0);
}
