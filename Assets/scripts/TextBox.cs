using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextBox : MonoBehaviour {

  TextMesh text;
  Transform box;

  /// <summary>
  /// Text lines to display in the object
  /// </summary>
  public List<string> displayText;

  /// <summary>
  /// Must have size equal to displayText. Determines how long to
  /// wait before going to the next display string
  /// </summary>
  public List<int> timings;

  /// <summary>
  /// Indicates if this text box is skippable
  /// </summary>
  public bool skippable;

  /// <summary>
  /// If true, object deletes itself when done running through text
  /// </summary>
  public bool deleteOnFinish;

  /// <summary>
  /// Set to true to automatically start on scene load.
  /// Probably want to wait before doing this really, so generally should be false.
  /// Can be empty if box is skippable
  /// </summary>
  public bool autoplay;

  /// <summary>
  /// Internal counter to track which string is being displayed.
  /// </summary>
  int idx;

  void Awake()
  {
    box = transform.FindChild("box");
    text = transform.Find("text").GetComponent<TextMesh>();

    // Reformat string so line breaks show up
    for (int i = 0; i < displayText.Count; i++)
    {
      displayText[i] = displayText[i].Replace("\\n", "\n");
      displayText[i] = displayText[i].Replace("\\t", "\t");
    }

    setText(idx);
    idx = 0;
  }

  // Use this for initialization
  void Start () {
    if (autoplay)
      startScript();
	}
	
	// Update is called once per frame
	void Update () {
	  if (skippable && Input.GetKeyDown(KeyCode.Space))
    {
      idx += 1;
      if (idx == displayText.Count)
      {
        setText("");
        if (deleteOnFinish)
        {
          Destroy(this.gameObject);
        }
      }
      else {
        setText(idx);
      }
    }
	}

  /// <summary>
  /// Begins playback of the text strings in the text box
  /// </summary>
  public void startScript()
  {
    StartCoroutine("runScript");
  }

  IEnumerator runScript()
  {
    // text starts at 0 already, so just go 
    for ( ; idx < displayText.Count; idx++)
    {
      setText(idx);

      yield return new WaitForSeconds(timings[idx]);
    }

    setText("");
    if (deleteOnFinish)
    {
      Destroy(this.gameObject);
    }
  }

  /// <summary>
  /// Set arbitrary string to text box
  /// </summary>
  /// <param name="t"></param>
  public void setText(string t)
  {
    text.text = t;
  }

  /// <summary>
  /// Sets the text to a string contained in the list of string to display.
  /// </summary>
  /// <param name="i"></param>
  public void setText(int i)
  {
    if (i >= 0 && i < displayText.Count)
    {
      text.text = displayText[i];
    }
  }
}
