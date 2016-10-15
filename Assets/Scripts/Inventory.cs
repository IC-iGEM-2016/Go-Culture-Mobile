 using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour {
	GameObject inventoryPanel;
	GameObject hiddingPanel;
	GameObject slotPanel;
	GameObject petriPanel;
	GameObject slotPanel2;
	GameObject mask;
	ItemDatabase database;
	public GameObject inventoryItem;

	int slotAmount;
	public List<Item> items = new List<Item> ();
	public List<GameObject> slots = new List<GameObject> ();

	void Start()
	{
		database = GetComponent<ItemDatabase> ();

		// Create slots in the inventory menu

		inventoryPanel = GameObject.Find ("Inventory Panel");
		hiddingPanel = GameObject.Find ("Hidding Panel");
		slotAmount = inventoryPanel.transform.childCount;

		for (int i = 0; i < slotAmount; i++)
		{
			items.Add (new Item ());
			slots.Add(inventoryPanel.transform.GetChild(i).gameObject);
			slots [i].GetComponent<Slot>().id = i;
			mask = hiddingPanel.transform.GetChild (i).gameObject;
			var image = mask.GetComponent<Image>(); 

			if (database.FetchItemByID (i).Collected == true) 
			{
				Destroy (image);
			}
		}

		for (int i = 0; i < items.Count; i++) 
		{
			for (int j = 0; j < 3; j++) 
			{
				AddItem (i);
			}
		}

		// Create slots in the petri dish
		petriPanel = GameObject.Find ("PetriDish");

		for (int i = 0; i < 3; i++)
		{
			items.Add (new Item ());
			slots.Add(petriPanel.transform.GetChild(i).gameObject);
			slots[slotAmount+i].GetComponent<Slot>().id = i + slotAmount;
		}
	}

	public void AddItem(int id)
	{
		Item itemToAdd = database.FetchItemByID(id);
		if (CheckIfItemIsInInventory (itemToAdd)) {
			for (int i = 0; i < items.Count; i++) {
				if (items [i].ID == id) {
					ItemData data = slots [i].transform.GetChild (1).GetComponent<ItemData> ();
					data.amount++; 
					//data.transform.GetChild (0).GetComponent<Text> ().text = data.amount.ToString ();
					//data.transform.GetChild (0).GetComponent<Text> ().text = "";
					slots[i].transform.GetChild (0).GetComponent<Text> ().text = data.amount.ToString ();
					break;
				}
			}
		} else {
			for (int i = 0; i < items.Count; i++) {
				if (items [i].ID == -1) {
					items [i] = itemToAdd;
					GameObject itemObj = Instantiate (inventoryItem);
					itemObj.GetComponent<ItemData>().item = itemToAdd;
					itemObj.GetComponent<ItemData>().amount = 1;
					itemObj.GetComponent<ItemData> ().slot = i;
					itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
					itemObj.transform.position = slots[i].transform.position;
					itemObj.transform.SetParent (slots[i].transform);
					itemObj.transform.localScale = Vector3.one;
					itemObj.name = itemToAdd.Title;
					break;
				}
			}
		}
	}

	bool CheckIfItemIsInInventory(Item item)
	{
		for (int i = 0; i < items.Count; i++) 
			if (items [i].ID == item.ID)
				return true; 
		return false;
	}
}