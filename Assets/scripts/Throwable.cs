using UnityEngine;
using System.Collections;

public class Throwable : MonoBehaviour {
  public Vector2 velocity;

  /// <summary>
  /// True if the mouse is over the button
  /// </summary>
  public bool mouseIsOver;

  /// <summary>
  /// The mouse can sometimes go too fast, this indicates if the
  /// button is still down and should still be dragged.
  /// </summary>
  private bool dragging = false;

  private Vector2 mousePosStart;
  private Vector2 mousePosEnd;
  private float duration;

  /// <summary>
  /// Indicates if the object should return to its home position after
  /// being thrown. Home position is the position in the editor on start.
  /// </summary>
  public bool returns;

  private Vector3 home;
  private bool offScreen;
  public bool returning;

  public float returnSpeed;

  public int timesThrown = 0;

	// Use this for initialization
	void Start () {
    velocity = new Vector2(0, 0);
    home = this.transform.position;
    returning = false;
	}
	
	// Update is called once per frame
	void Update () {
    Vector2 oldPos = this.transform.position;
    oldPos = oldPos + velocity * Time.deltaTime;
    this.transform.position = new Vector3(oldPos.x, oldPos.y, this.transform.position.z);

    if (!returning)
    {
      if (dragging || (Input.GetMouseButtonDown(0) && mouseIsOver))
      {
        // Drag item with mouse button
        velocity = Vector2.zero;
        dragging = true;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = this.transform.position.z;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = this.transform.position.z;
        this.transform.position = mousePos;
        duration += Time.deltaTime;

        // if this is the start, record position
        if (Input.GetMouseButtonDown(0))
        {
          mousePosStart = new Vector2(mousePos.x, mousePos.y);
        }
      }

      if (dragging && Input.GetMouseButtonUp(0))
      {
        dragging = false;

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = this.transform.position.z;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePosEnd = new Vector2(mousePos.x, mousePos.y);

        velocity = (mousePosEnd - mousePosStart) / duration;
        duration = 0;
      }
    }
    if (returning)
    {
      if ((this.transform.position - home).magnitude < 0.05)
      {
        this.transform.position = home;
        velocity = Vector2.zero;
        returning = false;
        timesThrown += 1;

        PetButton b = this.GetComponent<PetButton>();
        if (b != null)
        {
          // Reset timer for text if player threw a button
          b.timeClicked = 0;
        }
      } 
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

  void OnBecameInvisible()
  {
	  returning = true;
    StartCoroutine("returnHome");
  }

  IEnumerator returnHome()
  {
    yield return new WaitForSeconds(0.6f);

    returning = true;
    Vector3 currentLoc = this.transform.position;
    Vector2 dir = (home - currentLoc).normalized;
    velocity = dir * returnSpeed;
  }
}
