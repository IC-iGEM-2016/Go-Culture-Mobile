using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;

public class LevelProgress : MonoBehaviour {
	private List<Levels> levelsDatabase = new List<Levels>();
	private JsonData levelsData;
	GameObject CompletedPanel;

	void Start()
	{
		string itemFilePath = System.IO.Path.Combine(Application.persistentDataPath, "levels.json");

		if (Application.platform == RuntimePlatform.Android) // Android
		{
			if (!File.Exists(itemFilePath)) {
				WWW loadProductDB = new WWW (System.IO.Path.Combine(Application.streamingAssetsPath, "levels.json"));
				while ( ! loadProductDB.isDone) {}
				System.IO.File.WriteAllBytes (System.IO.Path.Combine(Application.persistentDataPath, "levels.json"), loadProductDB.bytes);
				levelsData = JsonMapper.ToObject (System.IO.File.ReadAllText (System.IO.Path.Combine(Application.persistentDataPath, "levels.json")));
			} else {
				levelsData = JsonMapper.ToObject (System.IO.File.ReadAllText (System.IO.Path.Combine(Application.persistentDataPath, "levels.json")));
			}
		}
		else // iOS
		{
			if (System.IO.File.Exists (itemFilePath)) 
			{
				levelsData = JsonMapper.ToObject (File.ReadAllText (itemFilePath));
			} else 
			{
				levelsData = JsonMapper.ToObject (File.ReadAllText(System.IO.Path.Combine(Application.streamingAssetsPath, "levels.json")));
			}
		}

		ConstructItemDatabase ();

		for (int i = 0; i < CompletedPanel.transform.childCount; i++) {
			if (levelsDatabase [i].unlocked == true) {
				CompletedPanel.transform.GetChild (i).gameObject.SetActive (true);
			} else {
				CompletedPanel.transform.GetChild (i).gameObject.SetActive (false);
			}
		}

		if (levelsDatabase [0].unlocked == false) {
			Handheld.PlayFullScreenMovie ("Maptuto.mp4", Color.black, FullScreenMovieControlMode.CancelOnInput);
			levelsDatabase [0].unlocked = true;
			levelsData = JsonMapper.ToJson (levelsDatabase);
			File.WriteAllText (System.IO.Path.Combine (Application.persistentDataPath, "levels.json"), levelsData.ToString ());
		}
	}

	public Levels FetchItemByID(int id)
	{
		for(int i = 0; i < levelsDatabase.Count; i++)
			if(levelsDatabase[i].id == id)
				return levelsDatabase[i];
		return null;
	}

	public Levels WriteInJson(int id)
	{
		levelsDatabase [id].unlocked = true;
		levelsData = JsonMapper.ToJson (levelsDatabase);

		File.WriteAllText (System.IO.Path.Combine (Application.persistentDataPath, "levels.json"), levelsData.ToString ());

		return null;
	}

	void ConstructItemDatabase()
	{
		for (int i = 0; i < levelsData.Count; i++)  
		{
			levelsDatabase.Add (new Levels ((int)levelsData [i] ["id"],  
				levelsData [i] ["level"].ToString (),
				(bool)levelsData [i] ["unlocked"]));
		}
	}
}

public class Levels
{
	public int id;
	public string level;
	public bool unlocked;

	public Levels(int id, string level, bool unlocked)
	{
		this.id = id;
		this.level = level;
		this.unlocked = unlocked;
	}

	public Levels()
	{
		this.id = -1;
	}
}
