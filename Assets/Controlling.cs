using UnityEngine;

public class Controlling: MonoBehaviour
{
  public float forceMultiplier = 3;
  public GameObject arrow;
  public GameObject strokesContainer;
  public GameObject strokeArrowPrefab;
  private Game game;
  private Ball Ball { get { return game.ball; } }
  private bool isAiming = false;

  private void Start()
  {
    game = GetComponentInParent<Game>();
  }

  private void UpdateArrow(Transform transform, Vector2 ballPosition, Vector2 mousePosition)
  {
    var diff = mousePosition - ballPosition;
    transform.SetPositionAndRotation(
      (ballPosition + mousePosition) * 0.5f,
      Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(diff.y, diff.x))
      );
    transform.localScale = new Vector3(
      diff.magnitude / 128 * 100,
      diff.magnitude / 128 * 100,
      1.0f);
  }

  private void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      isAiming = true;
    }

    if (!isAiming) return;

    var mousePosition3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    var mousePosition = new Vector2(mousePosition3.x, mousePosition3.y);
    var ballPosition = Ball.GetWorldPosition();

    var force = (mousePosition - ballPosition) * forceMultiplier;

    UpdateArrow(arrow.transform, ballPosition, mousePosition);
    arrow.SetActive(true);

    if (Input.GetMouseButtonUp(0))
    {
      var stroke = new Game.Stroke
      {
        ballPosition = Ball.GetWorldPosition(),
        mousePosition = mousePosition
      };

      var strokeArrow = GameObject.Instantiate(strokeArrowPrefab, strokesContainer.transform);
      UpdateArrow(strokeArrow.transform, ballPosition, mousePosition);

      foreach (var renderer in strokesContainer.GetComponentsInChildren<SpriteRenderer>())
        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, renderer.color.a * 0.5f);

      Ball.Hit(force);
      game.strokes.Add(stroke);
      game.SwitchState(game.watching);
      arrow.SetActive(false);
      isAiming = false;
    }

  }
}
