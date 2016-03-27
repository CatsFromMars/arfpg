using UnityEngine;
using System.Collections;

public class DadDogSetup : MonoBehaviour, GameWinHandler {

	public GameObject bar;
	public GameObject dog;
	public GameObject textbox;
	public GameObject gameGUI;

	Animator animator;
	TextBox textComponent;
	PetBar pbar;

	// Use this for initialization
	void Start () {
		textComponent = textbox.GetComponent<TextBox>();
		//textComponent.setText("");
		pbar = bar.GetComponent<PetBar>();
		animator = dog.GetComponent<Animator> ();
		gameGUI = GameObject.Find ("Game");
		StartCoroutine (monologue ());
	}

	// Update is called once per frame
	void Update () {
		if (pbar.isActive && pbar.currentTimeInBounds > 0.3f * pbar.timeToComplete)
		{
			textComponent.setText("You must not pet too much or too little!");
		}
	}

	public void winGame()
	{
		animator.SetInteger (Animator.StringToHash("Pet"), 1);
		textComponent.setText("Mmm...You have petted well.");
		Destroy(bar);
		gameGUI.SetActive (false);
		StartCoroutine("endGame");
	}

	IEnumerator monologue() {
		gameGUI.SetActive (false);
		textComponent.setText("Welcome to the World of ARFPG.");
		yield return new WaitForSeconds(4);
		textComponent.setText("You have probably never petted a dog before in your life.");
		yield return new WaitForSeconds(3);
		textComponent.setText("Fear not, for I shall teach you.");
		yield return new WaitForSeconds(2);
		textComponent.setText("Are you ready?");
		yield return new WaitForSeconds(2);
		textComponent.setText("To pet a dog, you must click the PET buton in rapid succession.");
		gameGUI.SetActive (true);
	}

	IEnumerator endGame()
	{
		yield return new WaitForSeconds(2);
		animator.SetInteger (Animator.StringToHash("Pet"), 0);
		textComponent.setText("But you still have much to learn, young pettawon.");
		yield return new WaitForSeconds(4);
		textComponent.setText("Go forth and fufill your sacred mission...");
		yield return new WaitForSeconds(4);
		textComponent.setText("PET ALL DOGS!");
		yield return new WaitForSeconds(2);
		Debug.Log("Game Ended, loading");
		GameObject loader = GameObject.Find("loader");
		loader.GetComponent<Loader>().load("chalk_dog");
	}
}
