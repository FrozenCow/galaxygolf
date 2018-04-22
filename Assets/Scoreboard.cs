using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Scoreboard : MonoBehaviour {
  public static Scoreboard Instance;

  public Dictionary<int, int> strokes = new Dictionary<int, int>();

  void Awake()
  {
    if (Instance == null)
    {
      DontDestroyOnLoad(gameObject);
      Instance = this;
    }
    else if (Instance != this)
    {
      Destroy(gameObject);
    }
  }
}
