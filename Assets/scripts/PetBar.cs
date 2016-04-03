using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public interface GameWinHandler
{
  void winGame();
}

public class PetBar : MonoBehaviour {
  public Image fill;
  public Image rangeMin;
  public Image rangeMax;
  public GameObject gameWinHandler;

  public float targetRangeMin = 0.7f;
  public float targetRangeMax = 0.8f;

  /// <summary>
  /// Bar fill decay per second
  /// </summary>
  public float barDecay = 0.01f;

  /// <summary>
  /// Starting bar fill
  /// </summary>
  public float startVal = 0.5f;

  /// <summary>
  /// Internal width for positioning elements.
  /// </summary>
  float width;

  /// <summary>
  /// How long you need to stay in the bounds to win
  /// </summary>
  public float timeToComplete = 1f;

  /// <summary>
  /// Tracker for how long the bar's been within bounds
  /// </summary>
  public float currentTimeInBounds;

  /// <summary>
  /// If active, bar fills and depletes
  /// </summary>
  public bool isActive;

	// Use this for initialization
	void Start () {
    Value = startVal;
    width = fill.rectTransform.rect.width;
    rangeMin.rectTransform.anchoredPosition = new Vector2(getAbsolutePosition(targetRangeMin), 0);
    rangeMax.rectTransform.anchoredPosition = new Vector2(getAbsolutePosition(targetRangeMax), 0);
  }
	
	// Update is called once per frame
	void Update () {
    if (isActive)
    {
      // Update position of thing if changed
      width = fill.rectTransform.rect.width;
      rangeMin.rectTransform.anchoredPosition = new Vector2(getAbsolutePosition(targetRangeMin), 0);
      rangeMax.rectTransform.anchoredPosition = new Vector2(getAbsolutePosition(targetRangeMax), 0);

      if (targetRangeMin < Value && Value <= targetRangeMax)
      {
        currentTimeInBounds += Time.deltaTime;
      }
      else
      {
        currentTimeInBounds = 0;
      }

      if (currentTimeInBounds > timeToComplete)
      {
        gameWin();
      }

      // Decay bar value
      Value = Mathf.Clamp(Value - barDecay * Time.deltaTime, 0, 1);
    }
  }

  float getAbsolutePosition(float relPos)
  {
    return width * relPos;
  }

  /// <summary>
  /// Sets how full the bar is and automatically updates the image.
  /// </summary>
  public float Value
  {
    get
    {
      if (fill != null)
      {
        return fill.fillAmount;
      }
      else
      {
        return 0;
      }
    }
    set
    {
      if (fill != null && isActive)
      {
        fill.fillAmount = Mathf.Clamp(value, 0, 1);
      }
    }
  }

  public void gameWin()
  {
    isActive = false;
    GameWinHandler g = gameWinHandler.GetComponent<GameWinHandler>();
    g.winGame();
  }
}
