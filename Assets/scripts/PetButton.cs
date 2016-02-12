using UnityEngine;
using System.Collections;

public class PetButton : MonoBehaviour {
  public SpriteRenderer buttonUp;
  public SpriteRenderer buttonDown;
  public PetBar bar;

  /// <summary>
  /// How much the button fills on first click
  /// </summary>
  public float fillStartVal;

  /// <summary>
  /// How much the fill value increases or decreases per second.
  /// </summary>
  public float fillSpeed;

  /// <summary>
  /// Rate of decay/acceleration for the fill speed
  /// </summary>
  public float fillAccel;

  /// <summary>
  /// Current fill rate
  /// </summary>
  public float fillVal;

  /// <summary>
  /// Tracker for current fill speed
  /// </summary>
  public float currentFillSpeed;

  /// <summary>
  /// false = up, true = down
  /// </summary>
  public bool buttonState;

  /// <summary>
  /// True if the mouse is over the button
  /// </summary>
  public bool mouseIsOver;

	// Use this for initialization
	void Start () {
    fillVal = fillStartVal;
    currentFillSpeed = fillSpeed;
    buttonState = false;
	}
	
	// Update is called once per frame
	void Update () {
    // update button state
    if (Input.GetMouseButton(0) && mouseIsOver)
    {
      bar.Value += fillVal;
      currentFillSpeed += fillAccel * Time.deltaTime;
      fillVal = Mathf.Clamp(fillVal + currentFillSpeed * Time.deltaTime, 0, 1);
      buttonState = true;
    }
    else if (!Input.GetMouseButton(0) || !mouseIsOver)
    {
      // Reset defaults if left button up
      currentFillSpeed = fillSpeed;
      fillVal = fillStartVal;
      buttonState = false;
    }

    if (buttonState)
    {
      buttonDown.color = new Color(1, 1, 1, 1);
      buttonUp.color = new Color(1, 1, 1, 0);
    }
    else
    {
      buttonDown.color = new Color(1, 1, 1, 0);
      buttonUp.color = new Color(1, 1, 1, 1);
    }
	}

  void OnMouseEnter()
  {
    mouseIsOver = true;
  }

  void OnMouseExit()
  {
    mouseIsOver = false;
  }
}
