using UnityEngine;
using System.Collections;

public class ChalkDogSetup : MonoBehaviour, GameWinHandler {

  public GameObject bar;
  public GameObject dog;
  public GameObject textbox;

  TextBox textComponent;
  PetBar pbar;

	// Use this for initialization
	void Start () {
    textComponent = textbox.GetComponent<TextBox>();
    textComponent.setText("Hi! I'm made of chalk!");
    pbar = bar.GetComponent<PetBar>();
	}
	
	// Update is called once per frame
	void Update () {
    if (pbar.isActive && pbar.currentTimeInBounds > 0.5f * pbar.timeToComplete)
    {
      textComponent.setText("Oh wow, your hands are getting dusty!");
    }
	}

  public void winGame()
  {
    textComponent.setText("Wow! Good Pet!");
    Destroy(bar);

    StartCoroutine("endGame");
  }

  IEnumerator endGame()
  {
    yield return new WaitForSeconds(2);

    Debug.Log("Game Ended, loading");
    GameObject loader = GameObject.Find("loader");
    loader.GetComponent<Loader>().load("fetch_dog");
  }
}
