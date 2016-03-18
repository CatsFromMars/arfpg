using UnityEngine;
using System.Collections;

public class ChalkDogSetup : MonoBehaviour, GameWinHandler {

  public GameObject bar;
  public GameObject dog;
  public GameObject textbox;

  Animator animator;
  TextBox textComponent;
  PetBar pbar;

	// Use this for initialization
	void Start () {
    textComponent = textbox.GetComponent<TextBox>();
    textComponent.setText("Hi! I'm made of chalk!");
    pbar = bar.GetComponent<PetBar>();
	animator = dog.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

    if (pbar.isActive && pbar.currentTimeInBounds > 0.5f * pbar.timeToComplete)
    {
	  animator.SetInteger (Animator.StringToHash("Pet"), 1);
      textComponent.setText("Oh wow, your hands are getting dusty!");
    }

	if (pbar.isActive && pbar.currentTimeInBounds > 0.75f * pbar.timeToComplete)
	{
		animator.SetInteger (Animator.StringToHash("Pet"), 2);
		textComponent.setText("I think some of my chalk is rubbing off...");
	}
	}

  public void winGame()
  {
	animator.SetInteger (Animator.StringToHash("Pet"), 3);
    textComponent.setText("It's ok, it'll grow back! Great petting!");
    Destroy(bar);

    StartCoroutine("endGame");
  }

  IEnumerator endGame()
  {
    yield return new WaitForSeconds(2);

    Debug.Log("end game things here woo");
  }
}
