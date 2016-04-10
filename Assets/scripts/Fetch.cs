using UnityEngine;

using System.Collections;

public class Fetch : MonoBehaviour {
  public GameObject button;

  public bool goFetch;

  Vector3 startPos;

  public float speed = 1;

  float pos;

	// Use this for initialization
	void Start () {
    startPos = this.transform.position;
    pos = 0;
  }
	
	// Update is called once per frame
	void Update () {
    if (button.GetComponent<Throwable>().returning)
    {
      goFetch = true;
    }
    else
    {
      goFetch = false;
      pos = 0;
    }

	  if (goFetch)
    {
      // lerp towards target, if at 1 stick to target
      pos += speed * Time.deltaTime;
      this.transform.position = Vector3.Lerp(startPos, button.transform.position, pos);
    }
	}
}
