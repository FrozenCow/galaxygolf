using UnityEngine;

public class Watching: MonoBehaviour
{
  private Game game;
  private Ball Ball { get { return game.ball; } }
  private void Start()
  {
    game = GetComponentInParent<Game>();
  }
  private void Update()
  {
    if (Ball.CanBallFallInHole())
    {
      Ball.StopRolling();
      game.SwitchState(game.finish);
    }
    else if (Ball.IsOnGround() && !Ball.IsRolling())
    {
      Ball.StopRolling();
      game.SwitchState(game.controlling);
    }
    else if (Ball.IsOutOfBounds())
    {
      Ball.ResetBall();
    }
  }
}
