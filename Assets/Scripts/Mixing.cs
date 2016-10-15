using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.IO;
using LitJson;

public class Mixing : MonoBehaviour {

	public Canvas popup;
	public Canvas LogbookPanel;
	public Button mixingButton;
	public Button okButton;
	public Button resetButton;
	public Button logbookButton;
	GameObject bacTextBox;
	GameObject resultBox;
	GameObject productsLog;
	String bacText;
	ProductDatabase productDatabase;
	private JsonData productData;

	// Use this for initialization
	void Start () 
	{
		bacTextBox = GameObject.Find ("Bacteria Text");
		resultBox = GameObject.Find ("Result Panel");
		productsLog = GameObject.Find ("resultImages");

		productDatabase = GetComponent<ProductDatabase> ();
		popup = popup.GetComponent<Canvas> ();
		mixingButton = mixingButton.GetComponent<Button> (); 
		okButton = okButton.GetComponent<Button> ();
		resetButton = resetButton.GetComponent<Button> ();
		logbookButton = logbookButton.GetComponent<Button> ();
		popup.enabled = false;
		LogbookPanel.enabled = false;
	}
	
	public void MixingPress()
	{
		popup.enabled = true;
		mixingButton.enabled = false;
		bacText = bacTextBox.transform.GetChild (0).GetComponent<Text> ().text;
		//Debug.Log (bacText.Length);

		Transform resultImage = resultBox.transform.GetChild (0);
		if (resultImage.transform.childCount != 0){
			Destroy (resultImage.transform.GetChild(0).gameObject);
		}

		int dashCounter = 0;
		foreach (char c in bacText) {
			if (c == '-') dashCounter++;
		}

		if (bacText.Contains ("Lactobacillus") && bacText.Contains ("Streptococcus Thermophilus") && bacText.Contains ("Bifidobacterium")) 
		{
			resultBox.transform.GetChild (1).GetComponent<Text> ().text = "Probiotic yogurt";
			GameObject product = (GameObject)Instantiate (Resources.Load("Prefabs/Products/probiotic_yogurt"));
			product.transform.SetParent (resultImage.transform);
			product.transform.position = resultImage.transform.position;

			productDatabase.WriteInJson (1);
			productsLog.transform.GetChild (1).gameObject.SetActive (true);

		} else if (bacText.Contains ("Lactobacillus") && bacText.Contains ("Streptococcus Thermophilus") && bacText.Length == 58) 
		{
			resultBox.transform.GetChild (1).GetComponent<Text> ().text = "Yogurt";
			GameObject product = (GameObject)Instantiate (Resources.Load("Prefabs/Products/yogurt"));
			product.transform.SetParent (resultImage.transform);
			product.transform.position = resultImage.transform.position;

			productDatabase.WriteInJson (0);
			productsLog.transform.GetChild (0).gameObject.SetActive (true);

		} else if (bacText.Contains ("Lactobacillus") && bacText.Contains ("Streptococcus Thermophilus") && bacText.Length == 71) 
		{
			resultBox.transform.GetChild (1).GetComponent<Text> ().text = "Yogurt";
			GameObject product = (GameObject)Instantiate (Resources.Load("Prefabs/Products/yogurt"));
			product.transform.SetParent (resultImage.transform);
			product.transform.position = resultImage.transform.position;

			productDatabase.WriteInJson (1);
			productsLog.transform.GetChild (0).gameObject.SetActive (true);

		} else if (bacText.Contains ("Lactobacillus") && bacText.Contains ("Streptococcus Thermophilus") && bacText.Contains ("Penicillium")) 
		{
			resultBox.transform.GetChild (1).GetComponent<Text> ().text = "Blue Cheese";
			GameObject product = (GameObject)Instantiate (Resources.Load("Prefabs/Products/blue_cheese"));
			product.transform.SetParent (resultImage.transform);
			product.transform.position = resultImage.transform.position;

			productDatabase.WriteInJson (2);
			productsLog.transform.GetChild (2).gameObject.SetActive (true);

		} else if (bacText.Contains ("Lactobacillus") && bacText.Contains ("Streptococcus Thermophilus") && bacText.Contains ("Propionibacterium Shermanii")) 
		{
			resultBox.transform.GetChild (1).GetComponent<Text> ().text = "Swiss Cheese";
			GameObject product = (GameObject)Instantiate (Resources.Load("Prefabs/Products/swiss_cheese"));
			product.transform.SetParent (resultImage.transform);
			product.transform.position = resultImage.transform.position;

			productDatabase.WriteInJson (3);
			productsLog.transform.GetChild (3).gameObject.SetActive (true);

		} else if (bacText.Contains ("Lactobacillus") && bacText.Contains ("S.cerevisiae") && bacText.Length == 43) 
		{
			resultBox.transform.GetChild (1).GetComponent<Text> ().text = "Sourdough Bread";
			GameObject product = (GameObject)Instantiate (Resources.Load("Prefabs/Products/bread"));
			product.transform.SetParent (resultImage.transform);
			product.transform.position = resultImage.transform.position;

			productDatabase.WriteInJson (4);
			productsLog.transform.GetChild (4).gameObject.SetActive (true);

		} else if (bacText.Contains ("Lactobacillus") && bacText.Contains ("Bifidobacterium") && bacText.Contains ("B.subtilis")) 
		{
			resultBox.transform.GetChild (1).GetComponent<Text> ().text = "Probiotic";
			GameObject product = (GameObject)Instantiate (Resources.Load("Prefabs/Products/probiotic"));
			product.transform.SetParent (resultImage.transform);
			product.transform.position = resultImage.transform.position;

			productDatabase.WriteInJson (5);
			productsLog.transform.GetChild (5).gameObject.SetActive (true);

		} else if (bacText.Contains ("E.coli") && bacText.Contains ("G.xylinus") && bacText.Length == 27) 
		{
			resultBox.transform.GetChild (1).GetComponent<Text> ().text = "Cellulose Overcoat";
			GameObject product = (GameObject)Instantiate (Resources.Load("Prefabs/Products/coat"));
			product.transform.SetParent (resultImage.transform);
			product.transform.position = resultImage.transform.position;

			productDatabase.WriteInJson (7);
			productsLog.transform.GetChild (7).gameObject.SetActive (true);

		} else if (bacText.Contains ("E.coli") && bacText.Contains ("G.xylinus") && bacText.Length == 30) 
		{
			resultBox.transform.GetChild (1).GetComponent<Text> ().text = "Cellulose Overcoat";
			GameObject product = (GameObject)Instantiate (Resources.Load("Prefabs/Products/coat"));
			product.transform.SetParent (resultImage.transform);
			product.transform.position = resultImage.transform.position;

			productDatabase.WriteInJson (7);
			productsLog.transform.GetChild (7).gameObject.SetActive (true);

		} else if (bacText.Contains ("E.coli") && bacText.Contains ("Bifidobacterium") && bacText.Contains ("Bacteroides")) 
		{
			resultBox.transform.GetChild (1).GetComponent<Text> ().text = "Gut Flora";
			GameObject product = (GameObject)Instantiate (Resources.Load("Prefabs/Products/gut_flora"));
			product.transform.SetParent (resultImage.transform);
			product.transform.position = resultImage.transform.position;

			productDatabase.WriteInJson (6);
			productsLog.transform.GetChild (6).gameObject.SetActive (true);

		} else if (bacText.Contains ("Lactobacillus") && bacText.Contains ("Bifidobacterium") && bacText.Contains ("Bacteroides")) 
		{
			resultBox.transform.GetChild (1).GetComponent<Text> ().text = "Gut Flora";
			GameObject product = (GameObject)Instantiate (Resources.Load("Prefabs/Products/gut_flora"));
			product.transform.SetParent (resultImage.transform);
			product.transform.position = resultImage.transform.position;

			productDatabase.WriteInJson (6);
			productsLog.transform.GetChild (6).gameObject.SetActive (true);

		} else if (bacText.Contains ("E.coli") && bacText.Contains ("G.xylinus") && bacText.Contains ("Dinoflagellate Luciferase")) 
		{
			resultBox.transform.GetChild (1).GetComponent<Text> ().text = "Glowing Cellulose Bow";
			GameObject product = (GameObject)Instantiate (Resources.Load("Prefabs/Products/glowing_bow"));
			product.transform.SetParent (resultImage.transform);
			product.transform.position = resultImage.transform.position;

			productDatabase.WriteInJson (8);
			productsLog.transform.GetChild (8).gameObject.SetActive (true);

		} else if (bacText.Contains ("E.coli") && bacText.Contains ("G.xylinus") && bacText.Contains ("Nitrobacter")) 
		{
			resultBox.transform.GetChild (1).GetComponent<Text> ().text = "Aquarium Water Filter";
			GameObject product = (GameObject)Instantiate (Resources.Load("Prefabs/Products/aquarium"));
			product.transform.SetParent (resultImage.transform);
			product.transform.position = resultImage.transform.position;

			productDatabase.WriteInJson (9);
			productsLog.transform.GetChild (9).gameObject.SetActive (true);

		} else if (bacText.Contains ("Penicillium") && bacText.Contains ("G.xylinus") && bacText.Contains ("E.coli")) 
		{
			resultBox.transform.GetChild (1).GetComponent<Text> ().text = "Antobiotics";
			GameObject product = (GameObject)Instantiate (Resources.Load("Prefabs/Products/antibiotics"));
			product.transform.SetParent (resultImage.transform);
			product.transform.position = resultImage.transform.position;

			productDatabase.WriteInJson (10);
			productsLog.transform.GetChild (10).gameObject.SetActive (true);

		} else if (bacText.Contains ("Nitrobacter") && bacText.Contains ("Cyanobacteria") && bacText.Contains ("B.subtilis")) 
		{
			resultBox.transform.GetChild (1).GetComponent<Text> ().text = "Aquaponic Ecosystem";
			GameObject product = (GameObject)Instantiate (Resources.Load("Prefabs/Products/aquaponics"));
			product.transform.SetParent (resultImage.transform);
			product.transform.position = resultImage.transform.position;

			productDatabase.WriteInJson (11);
			productsLog.transform.GetChild (11).gameObject.SetActive (true);

		} else if (bacText.Contains ("Cyanobacteria") && bacText.Contains ("E.coli") && bacText.Length == 38) 
		{
			resultBox.transform.GetChild (1).GetComponent<Text> ().text = "Biofuels";
			GameObject product = (GameObject)Instantiate (Resources.Load("Prefabs/Products/biofuel"));
			product.transform.SetParent (resultImage.transform);
			product.transform.position = resultImage.transform.position;

			productDatabase.WriteInJson (12);
			productsLog.transform.GetChild (12).gameObject.SetActive (true);

		} else if (bacText.Contains ("Pseudomonas Denitrificans") && bacText.Length == 81) 
		{
			resultBox.transform.GetChild (1).GetComponent<Text> ().text = "Vitamin B12";
			GameObject product = (GameObject)Instantiate (Resources.Load("Prefabs/Products/vitamin_b12"));
			product.transform.SetParent (resultImage.transform);
			product.transform.position = resultImage.transform.position;

			productDatabase.WriteInJson (13);
			productsLog.transform.GetChild (13).gameObject.SetActive (true);

		} else if (bacText.Contains ("Dinoflagellate Luciferase") && bacText.Contains ("Cyanobacteria") && bacText.Contains ("M.magnetotacticum")) 
		{
			resultBox.transform.GetChild (1).GetComponent<Text> ().text = "Magnetic Infinity Lamp";
			GameObject product = (GameObject)Instantiate (Resources.Load("Prefabs/Products/lava_lamp"));
			product.transform.SetParent (resultImage.transform);
			product.transform.position = resultImage.transform.position;

			productDatabase.WriteInJson (14);
			productsLog.transform.GetChild (14).gameObject.SetActive (true);

		} else if (bacText.Contains ("Dinoflagellate Luciferase") && bacText.Length == 81) 
		{
			resultBox.transform.GetChild (1).GetComponent<Text> ().text = "Lamp";
			GameObject product = (GameObject)Instantiate (Resources.Load("Prefabs/Products/lamp"));
			product.transform.SetParent (resultImage.transform);
			product.transform.position = resultImage.transform.position;

			productDatabase.WriteInJson (18);
			productsLog.transform.GetChild (18).gameObject.SetActive (true);

		} else if (bacText.Contains ("E.coli") && bacText.Contains ("M.magnetotacticum") && bacText.Length == 46) 
		{
			resultBox.transform.GetChild (1).GetComponent<Text> ().text = "Nanomagnets";
			GameObject product = (GameObject)Instantiate (Resources.Load("Prefabs/Products/magnets"));
			product.transform.SetParent (resultImage.transform);
			product.transform.position = resultImage.transform.position;

			productDatabase.WriteInJson (15);
			productsLog.transform.GetChild (15).gameObject.SetActive (true);

		} else if (bacText.Contains ("S.cerevisiae") && bacText.Length == 42) 
		{
			resultBox.transform.GetChild (1).GetComponent<Text> ().text = "Artemisinin";
			GameObject product = (GameObject)Instantiate (Resources.Load("Prefabs/Products/artemisinin"));
			product.transform.SetParent (resultImage.transform);
			product.transform.position = resultImage.transform.position;

			productDatabase.WriteInJson (16);
			productsLog.transform.GetChild (16).gameObject.SetActive (true);

		} else if (bacText.Contains ("E.coli") && bacText.Contains ("S.cerevisiae") && bacText.Contains ("B.subtilis")) 
		{
			resultBox.transform.GetChild (17).GetComponent<Text> ().text = "iGEM Top 3 Chassis";
			GameObject product = (GameObject)Instantiate (Resources.Load("Prefabs/Products/top_chassis"));
			product.transform.SetParent (resultImage.transform);
			product.transform.position = resultImage.transform.position;

			productDatabase.WriteInJson (17);
			productsLog.transform.GetChild (17).gameObject.SetActive (true);

		} else if (dashCounter != 2) 
		{
			resultBox.transform.GetChild (1).GetComponent<Text> ().text = "Please put 3 species in the Petri dish!";
			GameObject product = (GameObject)Instantiate (Resources.Load("Prefabs/Products/pink_cross"));
			product.transform.SetParent (resultImage.transform);
			product.transform.position = resultImage.transform.position;

		} else 
		{
			resultBox.transform.GetChild (1).GetComponent<Text> ().text = "Your co-culture was not successful, please try again!";
			GameObject product = (GameObject)Instantiate (Resources.Load("Prefabs/Products/pink_cross"));
			product.transform.SetParent (resultImage.transform);
			product.transform.position = resultImage.transform.position;
		}
	}

	public void okPress()
	{
		popup.enabled = false;
		LogbookPanel.enabled = false;
		mixingButton.enabled = true;
		logbookButton.enabled = true;
	}

	public void resetPress()
	{
		SceneManager.LoadScene ("test");
	}

	public void menuPress(){
		SceneManager.LoadScene ("Menu");
	}

	public void logbookPress(){
		LogbookPanel.enabled = true;
		logbookButton.enabled = false;
	}
}