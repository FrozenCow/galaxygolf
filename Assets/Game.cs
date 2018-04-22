using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {
  public class Stroke
  {
    public Vector2 ballPosition;
    public Vector2 mousePosition;
  }
  public List<Stroke> strokes = new List<Stroke>();

  public Ball ball;
  public GameObject watching;
  public GameObject controlling;
  public GameObject finish;

  public GameObject initialState;

  private GameObject currentState;

  public void SwitchState(GameObject state)
  {
    if (currentState != null)
      currentState.SetActive(false);
    currentState = state;
    if (currentState != null)
      currentState.SetActive(true);
  }

  public void FinishLevel()
  {
    AnalyticsEvent.LevelComplete(SceneManager.GetActiveScene().buildIndex, new Dictionary<string, object>()
    {
      { "strokes", strokes.Count }
    });
    Scoreboard.Instance.strokes[SceneManager.GetActiveScene().buildIndex] = strokes.Count;
    NextLevel();
  }

  public void NextLevel()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
  }

  public void PreviousLevel()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
  }

  public void ReloadLevel()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }

  private void Update()
  {
    bool shift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
    if (!shift && Input.GetKeyDown(KeyCode.R) && strokes.Count > 0)
    {
      ball.ResetBall();
    }
    if (shift && Input.GetKeyDown(KeyCode.N))
    {
      NextLevel();
    }
    if (shift && Input.GetKeyDown(KeyCode.R))
    {
      ReloadLevel();
    }
    if (shift && Input.GetKeyDown(KeyCode.P))
    {
      PreviousLevel();
    }
  }

  void Start ()
  {
    AnalyticsEvent.LevelStart(SceneManager.GetActiveScene().buildIndex);
    SwitchState(initialState == null ? controlling : initialState);
  }
}
