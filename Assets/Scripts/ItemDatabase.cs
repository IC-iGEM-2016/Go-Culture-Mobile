using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;

public class ItemDatabase : MonoBehaviour {
	private List<Item> database = new List<Item>();
	private JsonData itemData;
    string collectedString;
	string realPath;

    void Start()
	{
		string itemFilePath = System.IO.Path.Combine(Application.persistentDataPath, "Items.json");
		string dbPath = "";

		/*if (System.IO.File.Exists (itemFilePath)) {
			itemData = JsonMapper.ToObject (File.ReadAllText (itemFilePath));
		} else {
			itemData = JsonMapper.ToObject (File.ReadAllText(System.IO.Path.Combine(Application.streamingAssetsPath, "Items.json")));
			//Debug.Log (Application.streamingAssetsPath + " & " + Application.persistentDataPath);
		}*/

		if (Application.platform == RuntimePlatform.Android) // Android
		{
			if (!File.Exists(itemFilePath)) {
				WWW loadProductDB = new WWW (System.IO.Path.Combine(Application.streamingAssetsPath, "Items.json"));
				while ( ! loadProductDB.isDone) {}
				System.IO.File.WriteAllBytes (System.IO.Path.Combine(Application.persistentDataPath, "Items.json"), loadProductDB.bytes);
				itemData = JsonMapper.ToObject (System.IO.File.ReadAllText (System.IO.Path.Combine(Application.persistentDataPath, "Items.json")));
			} else {
				itemData = JsonMapper.ToObject (System.IO.File.ReadAllText (System.IO.Path.Combine(Application.persistentDataPath, "Items.json")));
			}
		}
		else // iOS
		{
			if (System.IO.File.Exists (itemFilePath)) 
			{
				itemData = JsonMapper.ToObject (File.ReadAllText (itemFilePath));
			} else 
			{
				itemData = JsonMapper.ToObject (File.ReadAllText(System.IO.Path.Combine(Application.streamingAssetsPath, "Items.json")));
			}
		}

		ConstructItemDatabase ();
	}

	public Item FetchItemByID(int id)
	{
		for(int i = 0; i < database.Count; i++)
			if(database[i].ID == id)
				return database[i];
		return null;
	}

    public Item FetchItemByNameAndSave(string name)
    {
        for (int i = 0; i < database.Count; i++)
            if (database[i].Title == name)
            {
                database[i].Collected = true;
                //string itemJson = JsonMapper.ToJson(database);

                string testJson = "[";

                for (int j = 0; j < database.Count; j++)
                {
                    testJson = System.String.Concat(testJson, "{\"id\": ", database[j].ID, ",",
                    "\"title\": \"", database[j].Title, "\",",
                    "\"environment\": \"", database[j].Environment, "\",",
                    "\"description\": \"", database[j].Description, "\",",
                    "\"fact\": \"", database[j].Fact, "\",",
                    "\"slug\": \"", database[j].Slug, "\",");

                    if (j == database.Count - 1)
                    {
                        if (database[j].Collected == true)
                        {
                            collectedString = "true";
                            testJson = System.String.Concat(testJson, "\"collected\": ", collectedString, "}");
                        }
                        else
                        {
                            collectedString = "false";
                            testJson = System.String.Concat(testJson, "\"collected\": ", collectedString, "}");
                        }
                    }
                    else
                    {
                        if (database[j].Collected == true)
                        {
                            collectedString = "true";
                            testJson = System.String.Concat(testJson, "\"collected\": ", collectedString, "},");
                        }
                        else
                        {
                            collectedString = "false";
                            testJson = System.String.Concat(testJson, "\"collected\": ", collectedString, "},");
                        }
                    }    
                }
                testJson = System.String.Concat(testJson, "]");

				File.WriteAllText(System.IO.Path.Combine(Application.persistentDataPath, "Items.json"), testJson);
                //Debug.Log("File Updated Successfully!");
            }
        return null;
    }

	void ConstructItemDatabase()
	{
		for (int i = 0; i < itemData.Count; i++)
		{
			database.Add (new Item ((int)itemData [i] ["id"], 
				itemData [i] ["title"].ToString (),
				itemData [i] ["environment"].ToString (),
				itemData [i] ["description"].ToString (),
				itemData [i] ["fact"].ToString (),
				itemData [i] ["slug"].ToString (),
				(bool)itemData [i] ["collected"]));
		}
	}
}

public class Item 
{
	public int ID { get; set; } // properties go with a capital letter
	public string Title { get; set; }
	public string Environment { get; set; }
	public string Description { get; set; }
	public string Fact { get; set; }
	public string Slug { get; set; }
	public bool Collected { get; set; }
	public Sprite Sprite { get; set; }

	public Item(int id, string title, string environment, string description, string fact, string slug, bool collected)
	{
		this.ID = id;
		this.Title = title;
		this.Environment = environment;
		this.Description = description;
		this.Fact = fact;
		this.Slug = slug;
		this.Collected = collected;
		this.Sprite = Resources.Load<Sprite> ("Sprites/Items/" + slug);
	}

	public Item()
	{
		this.ID = -1;
	}
}