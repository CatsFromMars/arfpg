using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour {
  private SpriteRenderer sprite;

  /// <summary>
  /// Fade in / out speed
  /// </summary>
  public float fadeSpeed;

  private string toLoad;
  private bool loading = false;
  private bool loaded = false;
  private float alpha = 0;

	// Use this for initialization
	void Start () {
    DontDestroyOnLoad(this);
    sprite = this.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	  if (loading)
    {
      if (!loaded && alpha < 1)
      { 
        alpha += fadeSpeed * Time.deltaTime;
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
      }
      if (!loaded && alpha > 1)
      {
        SceneManager.LoadScene(toLoad);
        loaded = true;
      }
      if (loaded && alpha > 0)
      {
        alpha -= fadeSpeed * Time.deltaTime;
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
      }
      if (loaded && alpha <= 0)
      {
        loading = false;
      }
    }

	}

  /// <summary>
  /// Fades out current scene, then loads next scene while faded,
  /// and then loads the scene, and fades back in
  /// technically this starts a coroutine that does the actual fade.
  /// </summary>
  /// <param name="scene">Scene to load</param>
  public void load(string scene)
  {
    toLoad = scene;
    loading = true;
    loaded = false;
  }
}
