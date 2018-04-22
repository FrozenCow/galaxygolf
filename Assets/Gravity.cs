using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gravity : MonoBehaviour
{
  float inAirTime = 0.0f;
  GameObject closestPlanet;
  public GameObject ClosestPlanet { get { return closestPlanet; } }
  public void FixedUpdate()
  {
    var myRigidBody = GetComponent<Rigidbody2D>();
    var myCircleCollider = GetComponent<CircleCollider2D>();
    var myRadius = myCircleCollider.radius;
    var rigidBodies = GameObject.FindObjectsOfType<Rigidbody2D>();
    var gravity = Vector2.zero;


    Rigidbody2D closestRigidBody = null;
    foreach (var body in rigidBodies)
      if (body.gameObject != gameObject && (closestRigidBody == null || body.Distance(myCircleCollider).distance < closestRigidBody.Distance(myCircleCollider).distance))
        closestRigidBody = body;

    if (closestRigidBody != null)
    {
      Vector2 diff = closestRigidBody.worldCenterOfMass - myRigidBody.worldCenterOfMass;
      float distanceCenterOfMass = diff.magnitude;
      var bodyRadius = 10.0f;
      float distance = closestRigidBody.Distance(myCircleCollider).distance;
      var force = Mathf.Pow(bodyRadius / (bodyRadius + distance), 2);
      gravity += diff.normalized * force * myRigidBody.gravityScale;
    }
    closestPlanet = closestRigidBody == null ? null : closestRigidBody.gameObject;

    myRigidBody.AddForceAtPosition(gravity, myRigidBody.worldCenterOfMass, ForceMode2D.Impulse);

    if (collisions.Count == 0)
      inAirTime += Time.fixedDeltaTime;
    else
      inAirTime = Mathf.Max(0.0f, Mathf.Min(inAirTime, 3.0f) - Time.fixedDeltaTime * 10.0f);

    //myRigidBody.AddForceAtPosition(myRigidBody.velocity * -0.01f, myRigidBody.worldCenterOfMass, ForceMode2D.Impulse);
  }

  HashSet<Collider2D> collisions = new HashSet<Collider2D>();

  private void OnCollisionEnter2D(Collision2D collision)
  {
    collisions.Add(collision.collider);
  }

  private void OnCollisionExit2D(Collision2D collision)
  {
    collisions.Remove(collision.collider);
  }
}
