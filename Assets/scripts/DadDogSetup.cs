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
    textComponent.displayText.Clear();
    textComponent.displayText.Add("Mmm...You have petted well.");
		textComponent.displayText.Add("But you still have much to learn, young pettawon.");
		textComponent.displayText.Add("Go forth and fufill your sacred mission...");
		textComponent.displayText.Add("PET ALL DOGS!");
    textComponent.skippable = true;
    textComponent.reset();
    textComponent.setText(0);

		Destroy(bar);
		gameGUI.SetActive (false);
		StartCoroutine("endGame");
	}

	IEnumerator monologue() {
		gameGUI.SetActive (false);
    textComponent.displayText.Clear();
		textComponent.displayText.Add("Welcome to the World of ARFPG.");
		textComponent.displayText.Add("You have probably never petted a dog before in your life.");
		textComponent.displayText.Add("Fear not, for I shall teach you.");
		textComponent.displayText.Add("Are you ready?");
		textComponent.displayText.Add("To pet a dog, you must click the PET buton in rapid succession.");
    textComponent.skippable = true;
    textComponent.reset();
    textComponent.setText(0);

    while (!textComponent.done)
    {
      yield return new WaitForSeconds(0.1f);
    }

		textComponent.setText("To pet a dog, you must click the PET buton in rapid succession.");
    textComponent.skippable = false;
    gameGUI.SetActive(true);
    pbar.isActive = true;
	}

	IEnumerator endGame()
	{
		animator.SetInteger (Animator.StringToHash("Pet"), 0);

    while (!textComponent.done)
    {
      yield return new WaitForSeconds(0.1f);
    }

    Debug.Log("Game Ended, loading");
		GameObject loader = GameObject.Find("loader");
		loader.GetComponent<Loader>().load("chalk_dog");
	}
}
