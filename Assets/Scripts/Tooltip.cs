using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {
	private Item item;
	private string data;
	private GameObject tooltip;

	void Start()
	{
		tooltip = GameObject.Find ("Tooltip");
		tooltip.SetActive (false);
	}

	void Update()  
	{
		if (tooltip.activeSelf) 
		{
			tooltip.transform.position = Input.mousePosition;
		}  
	}

	public void Activate(Item item)
	{
		this.item = item;
		ConstructDataString ();
		tooltip.SetActive (true);
	}

	public void Deactivate()
	{
		tooltip.SetActive (false);
	}

	public void ConstructDataString()
	{
		data = "<color=#D3BF8FFF><b>" + item.Title + "</b></color>\n\n<color=#9FB9C8FF><b>Description:\n\n</b>" + item.Description + "</color>"
			+ "<b>\n\nEnvironment:\n\n</b>" + item.Environment + "<b>\n\nFun Fact:\n\n</b>" + item.Fact;
		tooltip.transform.GetChild (0).GetComponent<Text> ().text = data;
	}
}
