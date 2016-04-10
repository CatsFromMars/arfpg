using UnityEngine;
using System.Collections;

public class BossDogSetup : MonoBehaviour, GameWinHandler {

  public GameObject dog;
  public GameObject textbox;

  public GameObject button1;
  public GameObject button2;
  public GameObject button3;

  public GameObject bar1;
  public GameObject bar2;
  public GameObject bar3;

  Animator animator;
  TextBox textComponent;

  bool endActive;
  int barsDone;

	// Use this for initialization
	void Start () {
    textComponent = textbox.GetComponent<TextBox>();
    textComponent.setText("I AM BOSS DOG PET ALL OF ME");
	  animator = dog.GetComponent<Animator> ();
    endActive = false;
    barsDone = 0;
	}
	
	// Update is called once per frame
	void Update () {
    float timeClicked = getTotalTimeClicked();
    if (5 < timeClicked && timeClicked < 10)
    {
      textComponent.setText("PET ME BETTER");
    }
    if (!endActive && timeClicked > 10)
    {
      // add the helpers
      endActive = true;
      StartCoroutine("makeWinnable");
    }

	}

  IEnumerator makeWinnable()
  {
    // first make buttons 1 and 3 autohit
    button1.GetComponent<PetButton>().autoClick = true;
    textComponent.setText("*Chalk dog comes to help*");
    yield return new WaitForSeconds(3);

    button3.GetComponent<PetButton>().autoClick = true;
    textComponent.setText("*Fetch dog comes to help*");
    yield return new WaitForSeconds(3);

	animator.SetInteger (Animator.StringToHash("Pet"), 1);
    textComponent.setText("You sense Boss Dog starting to relax...");
    for (int i = 0; i < 9; i++)
    {
      setBarSpeed(1.0f - 0.1f * i);
      yield return new WaitForSeconds(1);
    }
    
  }

  public void winGame()
  {
    barsDone++;
    if (barsDone == 3)
    {
      StartCoroutine("endGame");
    }
  }

  IEnumerator endGame()
  {
	button1.GetComponent<PetButton>().autoClick = false;
	button3.GetComponent<PetButton>().autoClick = false;
	yield return StartCoroutine(finalSpeech());
    yield return new WaitForSeconds(2);

    Debug.Log("Game Ended, loading");
	GameObject loader = GameObject.Find("loader");
	loader.GetComponent<Loader>().load("end");
  }

  IEnumerator finalSpeech() 
  {
	GameObject music = GameObject.Find("Music");
	music.GetComponent<AudioSource> ().Stop ();
	animator.SetInteger (Animator.StringToHash("Pet"), 2);
	textComponent.setText("IMPOSSIBLE...");
	yield return new WaitForSeconds(3);
    textComponent.setText("N-NO HUMAN HAS EVER BEEN ABLE TO PET ME BEFORE");
	yield return new WaitForSeconds(2);
	textComponent.setText("BECAUSE OF MY THREE HEADS");
	yield return new WaitForSeconds(2);
	textComponent.setText("...");
	yield return new WaitForSeconds(2);
	textComponent.setText("THANK YOU.");
  }

  private float getTotalTimeClicked()
  {
    float sum = 0;
    sum += button1.GetComponent<PetButton>().timeClicked;
    sum += button2.GetComponent<PetButton>().timeClicked;
    sum += button3.GetComponent<PetButton>().timeClicked;
    return sum;
  }

  private void setBarSpeed(float speed)
  {
    bar1.GetComponent<PetBar>().barDecay = speed;
    bar2.GetComponent<PetBar>().barDecay = speed;
    bar3.GetComponent<PetBar>().barDecay = speed;
  }
}
