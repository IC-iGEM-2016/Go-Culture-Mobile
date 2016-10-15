using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;

public class ProductDatabase : MonoBehaviour {
	private List<Products> productDatabase = new List<Products>();
	private JsonData productData;
	GameObject productsLog;

	void Start()
	{
		/*if (System.IO.File.Exists (itemFilePath)) {
			productData = JsonMapper.ToObject (File.ReadAllText (itemFilePath));
		} else {
			productData = JsonMapper.ToObject (File.ReadAllText(System.IO.Path.Combine(Application.streamingAssetsPath, "products.json")));
		}*/

		productsLog = GameObject.Find ("resultImages");
		string itemFilePath = System.IO.Path.Combine(Application.persistentDataPath, "products.json");

		if (Application.platform == RuntimePlatform.Android) // Android
		{
			if (!File.Exists(itemFilePath)) {
				WWW loadProductDB = new WWW (System.IO.Path.Combine(Application.streamingAssetsPath, "products.json"));
				while ( ! loadProductDB.isDone) {}
				System.IO.File.WriteAllBytes (System.IO.Path.Combine(Application.persistentDataPath, "products.json"), loadProductDB.bytes);
				productData = JsonMapper.ToObject (System.IO.File.ReadAllText (System.IO.Path.Combine(Application.persistentDataPath, "products.json")));
			} else {
				productData = JsonMapper.ToObject (System.IO.File.ReadAllText (System.IO.Path.Combine(Application.persistentDataPath, "products.json")));
			}
		}
		else // iOS
		{
			if (System.IO.File.Exists (itemFilePath)) 
			{
				productData = JsonMapper.ToObject (File.ReadAllText (itemFilePath));
			} else 
			{
				productData = JsonMapper.ToObject (File.ReadAllText(System.IO.Path.Combine(Application.streamingAssetsPath, "products.json")));
			}
		}
		ConstructItemDatabase ();

		for (int i = 0; i < productsLog.transform.childCount; i++) {
			if (productDatabase [i].created == true) {
				productsLog.transform.GetChild (i).gameObject.SetActive (true);
			} else {
				productsLog.transform.GetChild (i).gameObject.SetActive (false);
			}
		}
	}

	public Products FetchItemByID(int id)
	{
		for(int i = 0; i < productDatabase.Count; i++)
			if(productDatabase[i].id == id)
				return productDatabase[i];
		return null;
	}

	public Products WriteInJson(int id)
	{
		productDatabase [id].created = true;
		productData = JsonMapper.ToJson (productDatabase);

		File.WriteAllText (System.IO.Path.Combine (Application.persistentDataPath, "products.json"), productData.ToString ());
		//Debug.Log (Application.persistentDataPath);

		return null;
	}

	void ConstructItemDatabase()
	{
		for (int i = 0; i < productData.Count; i++)  
		{
			productDatabase.Add (new Products ((int)productData [i] ["id"],  
				productData [i] ["product"].ToString (),
				productData [i] ["description"].ToString (),
				productData [i] ["slug"].ToString (),
				(bool)productData [i] ["created"]));
		}
	}
}

public class Products
{
	public int id;
	public string product;
	public string description;
	public string slug;
	public bool created;

	public Products(int id, string product, string description, string slug, bool created)
	{
		this.id = id;
		this.product = product;
		this.description = description;
		this.slug = slug;
		this.created = created;
	}

	public Products()
	{
		this.id = -1;
	}
}
	