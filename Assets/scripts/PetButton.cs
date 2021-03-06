﻿using UnityEngine;
using System.Collections;


public enum AcceptedInput
{
  LEFT,
  RIGHT,
  BOTH,
  ALTERNATE
}

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
  /// Fill Decay speed
  /// </summary>
  public float fillDecaySpeed = -100;

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

  /// <summary>
  /// Indicates how the button should react to different mouse buttons
  /// </summary>
  public AcceptedInput inputMode = AcceptedInput.LEFT;

  /// <summary>
  /// Tracks amount of time the button is down.
  /// </summary>
  public float timeClicked = 0;

  bool acceptsLeft;
  bool acceptsRight;
  bool alternates;
  bool nextClickIsLeft;

  /// <summary>
  /// Automatically triggers a click every other frame if active
  /// </summary>
  public bool autoClick;
  public float autoClickInterval;
  float elapsedAutoTime;

	// Use this for initialization
	void Start () {
    fillVal = fillStartVal;
    currentFillSpeed = fillSpeed;
    buttonState = false;
    elapsedAutoTime = 0;

    changeInputMode(inputMode);
	}
	
	// Update is called once per frame
	void Update () {
    if (autoClick)
    {
      elapsedAutoTime += Time.deltaTime;

      if (autoClick && elapsedAutoTime > autoClickInterval)
      {
        bar.Value += fillStartVal;
        buttonState = true;
        elapsedAutoTime = 0;
      }
      else if (elapsedAutoTime > autoClickInterval * 0.5)
      {
        buttonState = false;
      }
    }
    else {
      // conditions for button down start
      // mouse button hit down inside the button or button is being held down
      if (buttonState || (acceptsLeft && mouseIsOver && Input.GetMouseButtonDown(0)))
      {
        // Add initial value to first click and only first click
        if (Input.GetMouseButtonDown(0))
        {
          fillVal = fillStartVal;
        }

        timeClicked += Time.deltaTime;
        buttonState = true;
        if (nextClickIsLeft || (acceptsLeft && acceptsRight && !alternates))
        {
          bar.Value += fillVal;
          currentFillSpeed += fillAccel * Time.deltaTime;
          fillVal = Mathf.Clamp(fillVal + currentFillSpeed * Time.deltaTime, 0, 1);
          if (alternates)
          {
            nextClickIsLeft = false;
          }
        }
      }
      else if (buttonState || (acceptsRight && mouseIsOver && Input.GetMouseButtonDown(1)))
      {
        // Add initial value to first click and only first click
        if (Input.GetMouseButtonDown(1))
        {
          fillVal = fillStartVal;
        }

        timeClicked += Time.deltaTime;
        buttonState = true;
        if (!nextClickIsLeft || (acceptsLeft && acceptsRight && !alternates))
        {
          bar.Value += fillVal;
          currentFillSpeed += fillAccel * Time.deltaTime;
          fillVal = Mathf.Clamp(fillVal + currentFillSpeed * Time.deltaTime, 0, 1);
          if (alternates)
          {
            nextClickIsLeft = true;
          }
        }
      }

      //if (acceptsRight && Input.GetMouseButton(1) && mouseIsOver)
      //{
      //  timeClicked += Time.deltaTime;
      //  if (!nextClickIsLeft || (acceptsLeft && acceptsRight && !alternates))
      //  {
      //    bar.Value += fillVal;
      //    currentFillSpeed += fillAccel * Time.deltaTime;
      //    fillVal = Mathf.Clamp(fillVal + currentFillSpeed * Time.deltaTime, 0, 1);
      //    buttonState = true;
      //    if (alternates)
      //    {
      //      nextClickIsLeft = true;
      //   }
      //  }
      //}

      if (!mouseIsOver || (acceptsLeft && Input.GetMouseButtonUp(0)) || (acceptsRight && Input.GetMouseButtonUp(1)))
      {
        buttonState = false;
        currentFillSpeed = fillSpeed;
        fillVal = Mathf.Clamp(fillVal - fillDecaySpeed * Time.deltaTime, 0, 1);
        bar.Value += fillVal;
      }
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

  public void changeInputMode(AcceptedInput i)
  {
    switch (i)
    {
      case AcceptedInput.LEFT:
        acceptsLeft = true;
        acceptsRight = false;
        nextClickIsLeft = true;
        alternates = false;
        break;
      case AcceptedInput.RIGHT:
        acceptsLeft = false;
        acceptsRight = true;
        nextClickIsLeft = false;
        alternates = false;
        break;
      case AcceptedInput.BOTH:
        acceptsLeft = true;
        acceptsRight = true;
        alternates = false;
        nextClickIsLeft = true;
        break;
      case AcceptedInput.ALTERNATE:
        acceptsLeft = true;
        acceptsRight = true;
        alternates = true;
        nextClickIsLeft = true;
        break;
    }
  }
}
