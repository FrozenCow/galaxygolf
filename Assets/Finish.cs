using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Finish: MonoBehaviour
{
  private Game game;
  public Text strokesText;
  private void OnEnable()
  {
    game = GetComponentInParent<Game>();
    game.ball.gameObject.SetActive(false);
    strokesText.text = game.strokes.Count == 1
      ? "Hole in one!"
      : game.strokes.Count + " strokes";
  }
  private void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      game.FinishLevel();
    }
  }
  private void OnDisable()
  {
    game.ball.gameObject.SetActive(true);
  }
}
