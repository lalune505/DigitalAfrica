using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour {
	private const string SaveSessionKey = "firstSession19";
	public static SaveManager instance;
	public SaveState sessionState;


	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance == this)
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);
		
		LoadPlayerPrefs();
	}

	public void SaveSessionEnter() 
	{
		sessionState.IsFirstEnter = false;
		PlayerPrefs.SetString(SaveSessionKey, SaveHelper.Serialize<SaveState>(sessionState));
	}
	public void SavePermissionRequest()
	{
		sessionState.IsPermissionRequested = true;

		PlayerPrefs.SetString(SaveSessionKey, SaveHelper.Serialize<SaveState>(sessionState));
	}
	public void SaveIntroState()
	{
		sessionState.WasIntroShown = true;

		PlayerPrefs.SetString(SaveSessionKey, SaveHelper.Serialize<SaveState>(sessionState));
	}

	public void LoadPlayerPrefs()
	{
		if (PlayerPrefs.HasKey(SaveSessionKey))
		{
			sessionState = SaveHelper.Deserialize<SaveState>(PlayerPrefs.GetString(SaveSessionKey));
			sessionState.IsPermissionRequested = true;
		} else {
			sessionState = new SaveState();
		}
	}
}
