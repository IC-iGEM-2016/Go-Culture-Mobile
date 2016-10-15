using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler{//, IPointerEnterHandler, IPointerExitHandler {
	public Item item;
	public int amount;
	public int slot; 

	GameObject inventoryPanel;
	GameObject slotPanel;
	GameObject bacText;
	GameObject petriPanel;
	GameObject slotPanel2;
	GameObject slotParent;
	GameObject slotChild;

	Transform parentSlot;
	Transform slot1;
	Transform slot2;
	Transform slot3;

	private Inventory inv;
	private Tooltip tooltip;
	//private Vector2 offset;

	void Start()
	{ 
		inv = GameObject.Find ("Canvas").GetComponent<Inventory> ();
		tooltip = inv.GetComponent<Tooltip> ();


		// Set up the text region to display which bacteria are in the Petri dish
		bacText = GameObject.Find ("Bacteria Text");
	}

	#region IBeginDragHandler implementation
	public void OnBeginDrag (PointerEventData eventData)
	{
		if (item != null) {
			parentSlot = this.transform.parent;
			//ItemData pickedItem = eventData.pointerDrag.GetComponent<ItemData> ();

			if (this.transform.GetChild (0).GetComponent<Text> ().text != "") {
				if (System.Convert.ToInt32 (transform.GetChild (0).GetComponent<Text> ().text) == 3) {
					parentSlot.transform.GetChild (0).GetComponent<Text> ().text = System.Convert.ToString (3);
					//Debug.Log (parentSlot.transform.GetChild (0).GetComponent<Text> ().text);
					this.transform.GetChild (0).GetComponent<Text> ().text = "";
					this.amount = 3;
				}
			}

			for (int i = 0; i < transform.childCount; i++) {
				if (parentSlot.transform.GetChild (i).GetComponent<Text> () != null) {

					if (parentSlot.transform.childCount == 2 && System.Convert.ToInt32 (parentSlot.transform.GetChild (0).GetComponent<Text> ().text)>1) {
						slotParent = parentSlot.gameObject;
						slotChild = Instantiate (this.gameObject);
						slotChild.GetComponent<ItemData> ().item = this.item;
						slotChild.GetComponent<ItemData> ().amount = this.amount;
						slotChild.GetComponent<ItemData> ().slot = this.slot;
						slotChild.GetComponent<Image> ().sprite = this.GetComponent<Image> ().sprite;
						slotChild.transform.SetParent (slotParent.transform);
						slotChild.transform.position = slotParent.transform.position;
						slotChild.transform.localScale = Vector3.one;
						slotChild.name = this.name;
					}
					parentSlot.transform.GetChild (0).GetComponent<Text> ().text = (System.Convert.ToInt32 (parentSlot.transform.GetChild (0).GetComponent<Text> ().text) - 1).ToString ();
					this.amount = this.amount - 1;
				}
			}
				   
			//offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
			inventoryPanel = GameObject.Find ("Inventory Panel");
			//slotPanel = inventoryPanel.transform.FindChild ("Slot Panel").gameObject;
			this.transform.SetParent (inventoryPanel.transform.parent);
			this.transform.position = eventData.position; //- offset;
			GetComponent<CanvasGroup> ().blocksRaycasts = false;
		}
	}

	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		if (item != null) 
		{
			this.transform.position = eventData.position; //- offset;
		}
	}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		if (this.slot < 17) 
		{
			inventoryPanel = GameObject.Find ("Inventory Panel");
			//slotPanel = inventoryPanel.transform.FindChild ("Slot Panel").gameObject;
			inventoryPanel.transform.GetChild(this.slot).GetChild(0).GetComponent<Text> ().text = (System.Convert.ToInt32(inventoryPanel.transform.GetChild(this.slot).GetChild(0).GetComponent<Text> ().text) + 1).ToString ();
			this.amount = this.amount + 1;
		}

		petriPanel = GameObject.Find ("PetriDish");
		//slotPanel2 = petriPanel.transform.FindChild("slotPetri").gameObject;
		slot1 = petriPanel.transform.GetChild (0);
		slot2 = petriPanel.transform.GetChild (1);
		slot3 = petriPanel.transform.GetChild (2);

		this.transform.SetParent(inv.slots[slot].transform);
		this.transform.position = inv.slots[slot].transform.position;

		if (slot1.transform.childCount == 1 && slot2.transform.childCount == 0 && slot3.transform.childCount == 0) 
		{
			bacText.transform.GetChild (0).GetComponent<Text> ().text = " " + slot1.GetChild(0).name + " ";
		}
		else if (slot1.transform.childCount == 0 && slot2.transform.childCount == 1 && slot3.transform.childCount == 0)
		{
			bacText.transform.GetChild (0).GetComponent<Text> ().text = " " + slot2.GetChild(0).name + " ";
		}
		else if (slot1.transform.childCount == 0 && slot2.transform.childCount == 0 && slot3.transform.childCount == 1)
		{
			bacText.transform.GetChild (0).GetComponent<Text> ().text = " " + slot3.GetChild(0).name + " ";
		}
		else if (slot1.transform.childCount == 1 && slot2.transform.childCount == 1 && slot3.transform.childCount == 0)
		{
			bacText.transform.GetChild (0).GetComponent<Text> ().text = slot1.GetChild(0).name + " - " + slot2.GetChild(0).name;
		}
		else if (slot1.transform.childCount == 1 && slot2.transform.childCount == 0 && slot3.transform.childCount == 1)
		{
			bacText.transform.GetChild (0).GetComponent<Text> ().text = slot1.GetChild(0).name + " - " + slot3.GetChild(0).name;
		}
		else if (slot1.transform.childCount == 0 && slot2.transform.childCount == 1 && slot3.transform.childCount == 1)
		{
			bacText.transform.GetChild (0).GetComponent<Text> ().text = slot2.GetChild(0).name + " - " + slot3.GetChild(0).name;
		}
		else if (slot1.transform.childCount == 1 && slot2.transform.childCount == 1 && slot3.transform.childCount == 1)
		{
			bacText.transform.GetChild (0).GetComponent<Text> ().text = slot1.GetChild(0).name + " - " + slot2.GetChild(0).name + " - " + slot3.GetChild(0).name;
		}
		else
		{
			bacText.transform.GetChild (0).GetComponent<Text> ().text = "";
		}
		//Debug.Log (this.amount);
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
	}

	#endregion

	/*#region IPointerEnterHandler implementation

	public void OnPointerEnter (PointerEventData eventData)
	{
		tooltip.Activate (item);
	}

	#endregion

	#region IPointerExitHandler implementation

	public void OnPointerExit (PointerEventData eventData)
	{
		tooltip.Deactivate ();
	}

	#endregion*/

	#region IPointerDownHandler implementation

	public void OnPointerDown (PointerEventData eventData)
	{
		tooltip.Activate (item);
	}

	#endregion

	#region IPointerUpHandler implementation

	public void OnPointerUp (PointerEventData eventData)
	{
		tooltip.Deactivate ();
	}

	#endregion
}
