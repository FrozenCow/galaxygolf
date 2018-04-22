using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class EndOfGame : MonoBehaviour
{
  public Text strokesLeftText;
  public Text strokesRightText;
  void Start()
  {
    var strokes = Scoreboard.Instance.strokes;
    var pairs = strokes.OrderBy(pair => pair.Key).ToList();
    var left = pairs.Take((int)Math.Ceiling(pairs.Count / 2.0f)).ToList();
    var right = pairs.Skip(left.Count).ToList();

    strokesLeftText.text = string.Join("\n", left.Select(pair => "Level " + (pair.Key + 1) + ": " + pair.Value).ToArray());
    strokesRightText.text = string.Join("\n", right.Select(pair => "Level " + (pair.Key + 1) + ": " + pair.Value).ToArray());

    AnalyticsEvent.Custom("endofgame", strokes.ToArray().ToDictionary(pair => "level" + pair.Key, pair => pair.Value as object));
  }
}
