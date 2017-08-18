using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Creditos : MonoBehaviour {


	public GameObject theCreditsScreen;


	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Pause"))
		{
			if(Time.timeScale == 0f)
			{
				VoltaMenu();
			} else {
				Credito();
			}
		}
	}

	public void Credito()
	{
		Time.timeScale = 0;

		theCreditsScreen.SetActive(true);
	}

	public void VoltaMenu()
	{
		theCreditsScreen.SetActive(false);

		Time.timeScale = 1f;
	}
}
