using UnityEngine;
using System.Collections;

public class FetchDogSetup : MonoBehaviour, GameWinHandler {

  public GameObject bar;
  public GameObject dog;
  public GameObject textbox;
  public GameObject button;

  Animator animator;
  TextBox textComponent;
  PetBar pbar;
  PetButton pbutton;
  Throwable buttonThrower;

	// Use this for initialization
	void Start () {
    textComponent = textbox.GetComponent<TextBox>();
    textComponent.setText("Play fetch with me!");
    pbar = bar.GetComponent<PetBar>();
    pbutton = button.GetComponent<PetButton>();
    buttonThrower = button.GetComponent<Throwable>();
	animator = dog.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

	if (buttonThrower.returning) {
		animator.SetInteger (Animator.StringToHash("Pet"), 1);
	}
	else animator.SetInteger (Animator.StringToHash("Pet"), 0);
    
	if (!pbar.isActive && pbutton.timeClicked >= 3)
    {
      textComponent.setText("No pets! Play fetch instead!");
    }

    if (buttonThrower.timesThrown >= 3)
    {
      textComponent.setText("I'm tired. Give pets?");
      pbar.isActive = true;
    }
	}

  public void winGame()
  {
    textComponent.setText("zzzzzz");
    Destroy(bar);
    Destroy(button);

    StartCoroutine("endGame");
  }

  IEnumerator endGame()
  {
    yield return new WaitForSeconds(2);

    Debug.Log("end game things here woo");
	GameObject loader = GameObject.Find("loader");
	loader.GetComponent<Loader>().load("boss_dog");
  }
}
