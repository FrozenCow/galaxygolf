using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelIndicator : MonoBehaviour
{
  float startTime;
  void Start()
  {
    startTime = Time.time;
    GetComponentInChildren<Text>().text = "Level " + (SceneManager.GetActiveScene().buildIndex + 1);
  }

  private void Update()
  {
    float timeElapsed = Time.time - startTime;
    float maxTime = 1;
    float progress = Mathf.Clamp(1 - (timeElapsed - 2) / maxTime, 0.0f, 1.0f);

    GetComponent<CanvasGroup>().alpha = progress;
  }
}
