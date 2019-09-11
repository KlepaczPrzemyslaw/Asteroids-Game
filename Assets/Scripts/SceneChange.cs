using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
	private float _delay = 0.5f; 

	[SerializeField]
	string _sceneName = "";

	void Update()
	{
		if (Time.timeSinceLevelLoad < _delay) return;

		if (Input.GetMouseButtonDown(0))
			if (!string.IsNullOrWhiteSpace(_sceneName))
				SceneManager.LoadScene(_sceneName);

		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
	}
}
