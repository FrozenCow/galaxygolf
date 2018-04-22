using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ball : MonoBehaviour {
  private new Rigidbody2D rigidbody;
  private Vector2 resetPosition;
  public void Start()
  {
    rigidbody = GetComponent<Rigidbody2D>();
  }

  public void StopRolling()
  {
    rigidbody.velocity = Vector2.zero;
    rigidbody.angularVelocity = 0.0f;
  }

  private Vector2 GetRelativeVelocity(Vector2 position, Rigidbody2D a, Rigidbody2D b)
  {
    return a.GetRelativePointVelocity(a.GetVector(position)) - b.GetRelativePointVelocity(b.GetVector(position));
  }

  private Vector2 Sum<T>(IEnumerable<T> items, Func<T, Vector2> selector)
  {
    return items.Aggregate(Vector2.zero, (total, item) => total + selector(item));
  }

  public bool IsRolling()
  {
    var velocity = colliders.Count == 0
      ? rigidbody.velocity
      : Sum(colliders.Values, collider => Sum(collider.contacts, contact => GetRelativeVelocity(contact.point, collider.rigidbody, collider.otherRigidbody)));
    return velocity.magnitude > 10.0f;
  }

  public bool IsOnGround()
  {
    return colliders.Count != 0;
  }

  public Hole GetTouchingHole()
  {
    return triggers
      .Select(trigger => trigger.GetComponent<Hole>())
      .FirstOrDefault();
  }

  public bool CanBallFallInHole()
  {
    return IsOnGround() && GetTouchingHole() != null;
  }

  Dictionary<Collider2D, Collision2D> colliders = new Dictionary<Collider2D, Collision2D>();

  private void OnCollisionEnter2D(Collision2D collision)
  {
    colliders[collision.collider] = collision;

    if (collision.gameObject.GetComponent<Hurtful>() != null)
    {
      ResetBall();
    }
  }

  private void OnCollisionStay2D(Collision2D collision)
  {
    colliders[collision.collider] = collision;
  }

  private void OnCollisionExit2D(Collision2D collision)
  {
    colliders.Remove(collision.collider);
  }

  HashSet<Collider2D> triggers = new HashSet<Collider2D>();

  private void OnTriggerEnter2D(Collider2D collider)
  {
    triggers.Add(collider);
  }

  private void OnTriggerExit2D(Collider2D collider)
  {
    triggers.Remove(collider);
  }

  public void Hit(Vector2 force)
  {
    resetPosition = GetWorldPosition();
    rigidbody.AddForceAtPosition(force, rigidbody.worldCenterOfMass, ForceMode2D.Impulse);
  }

  public void ResetBall()
  {
    rigidbody.position = resetPosition;
    StopRolling();
  }

  public bool IsOutOfBounds()
  {
    return (GetComponent<Gravity>().ClosestPlanet.transform.position - gameObject.transform.position).magnitude > 100.0f;
  }

  public Vector2 GetWorldPosition()
  {
    return rigidbody.worldCenterOfMass;
  }

}
