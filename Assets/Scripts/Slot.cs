 using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropHandler {
	private Inventory inv;
	private GameObject petriSlots;
	private GameObject inventoryPanel;
	private GameObject bacteriaInPetri;
	int originalSlotNumber;
	public int id;


	void Start()
	{
		inv = GameObject.Find("Canvas").GetComponent<Inventory>();
		petriSlots = GameObject.Find ("PetriDish");
		inventoryPanel = GameObject.Find ("Inventory Panel");
	}

	#region IDropHandler implementation

	public void OnDrop (PointerEventData eventData)
	{
		ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData> ();
		//Debug.Log(inv.items[droppedItem.slot].ID + " & " + droppedItem.slot + " & " + id);
		//Debug.Log(inv.items[droppedItem.slot].ID + " & " + id);


		if (id == inv.items [droppedItem.slot].ID) {
			inv.items [droppedItem.slot] = new Item ();
			inv.items [id] = droppedItem.item;
			droppedItem.slot = id;

		} else if (id > 16) {
			if (droppedItem.slot != id && petriSlots.transform.GetChild (id - 17).childCount == 0) 
			{
				inv.items [droppedItem.slot] = new Item ();
				inv.items [id] = droppedItem.item;
				droppedItem.slot = id;

			} else if (droppedItem.slot != id && petriSlots.transform.GetChild (id - 17).childCount > 0)
			{
				
				Transform item = this.transform.GetChild (0);

				for (int i = 0; i < inventoryPanel.transform.childCount; i++)
				{
					var nameTemp = inventoryPanel.transform.GetChild (i).GetChild (1).name;
					if (nameTemp == item.name) 
					{
						//Transform originalSlot = inventoryPanel.transform.GetChild (i);
						originalSlotNumber = i;
					}
				}
					
				item.GetComponent<ItemData> ().slot = originalSlotNumber;
				item.GetComponent<ItemData> ().amount = item.GetComponent<ItemData> ().amount + 1;
				inventoryPanel.transform.GetChild(originalSlotNumber).GetChild(0).GetComponent<Text> ().text = (System.Convert.ToInt32(inventoryPanel.transform.GetChild(originalSlotNumber).GetChild(0).GetComponent<Text> ().text) + 1).ToString ();
				//Debug.Log (originalSlotNumber + " & " + inv.slots [originalSlotNumber].transform);
				item.transform.SetParent (inv.slots [originalSlotNumber].transform);
				item.transform.position = inv.slots [originalSlotNumber].transform.position;

				droppedItem.slot = id;
				droppedItem.transform.SetParent (this.transform);
				droppedItem.transform.position = this.transform.position;

				inv.items [originalSlotNumber] = item.GetComponent<ItemData> ().item;
				inv.items [id] = droppedItem.item;
			}
		}
	}
	#endregion
}