using UnityEngine;
using UnityEngine.UI;

public class GameOverUserInterface : MonoBehaviour
{
	[SerializeField]
	private Text _recordText;

	[SerializeField]
	private Text _lastGameText;

	void Start()
	{
		_lastGameText.text = $"Points: {GameState.GetLastResult().ToString()}";
		_recordText.text = $"Record: {GameState.GetRecord().ToString()}";
	}
}
