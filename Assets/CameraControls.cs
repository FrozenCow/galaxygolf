using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
  private bool isDragging = false;
  private Vector3 lastMousePosition;

  public GameObject arrow;
  public Game game;
  private void Start()
  {
    if (game == null) game = GetComponentInParent<Game>();
  }
  void Update()
  {
    var mousePosition = Input.mousePosition;
    var mouseDeltaPosition = Input.GetMouseButtonDown(1) || !Input.GetMouseButton(1)
      ? Vector3.zero
      : lastMousePosition - mousePosition;
    var newCameraPosition = Camera.main.ScreenToWorldPoint(Camera.main.WorldToScreenPoint(Camera.main.transform.position) + mouseDeltaPosition);
    Camera.main.transform.position = newCameraPosition;
    lastMousePosition = mousePosition;

    Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - Input.mouseScrollDelta.y, 1.0f, 20.0f);

    var ballWorldPosition = game.ball.transform.position;
    var ballViewportPosition = Camera.main.WorldToViewportPoint(ballWorldPosition);
    var viewportRect = new Rect(0, 0, 1, 1);
    bool isBallVisible = viewportRect.Contains(ballViewportPosition);
    arrow.SetActive(!isBallVisible);
    if (!isBallVisible)
    {
      var difference = ballWorldPosition - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f));
      var direction = difference.ToVector2().normalized;

      var canvas = GetComponentInChildren<Canvas>();
      var radius = Mathf.Min(canvas.pixelRect.width, canvas.pixelRect.height) * 0.5f;
      var pointerPosition = direction * radius;
      var arrowRectTransform = arrow.GetComponent<RectTransform>();
      arrowRectTransform.anchoredPosition = pointerPosition;
      arrowRectTransform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x));
    }
  }
}
