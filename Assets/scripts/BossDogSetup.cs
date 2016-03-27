using UnityEngine;
using System.Collections;

public class BossDogSetup : MonoBehaviour, GameWinHandler {

  public GameObject bar;
  public GameObject dog;
  public GameObject textbox;

  Animator animator;
  TextBox textComponent;
  PetBar pbar;

	// Use this for initialization
	void Start () {
    textComponent = textbox.GetComponent<TextBox>();
    textComponent.setText("I AM BOSS DOG PET ALL OF ME");
    pbar = bar.GetComponent<PetBar>();
	  animator = dog.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {


	}

  public void winGame()
  {
    StartCoroutine("endGame");
  }

  IEnumerator endGame()
  {
    yield return new WaitForSeconds(2);

    Debug.Log("Game Ended, loading");
  }
}
