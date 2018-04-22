using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
  public GameObject game;
  public GameObject indicatorPrefab;
  public float forceMultiplier = 1.0f;
  private List<GameObject> indicators = new List<GameObject>();

  float timer;

  void Update()
  {
    var mousePosition3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    var mousePosition2 = new Vector2(mousePosition3.x, mousePosition3.y);
    var myRigidBody = GetComponent<Rigidbody2D>();
    var diff = mousePosition2 - myRigidBody.worldCenterOfMass;
    var force = diff * forceMultiplier;

    if (Input.GetMouseButtonDown(0))
    {
      myRigidBody.AddForceAtPosition(force, myRigidBody.worldCenterOfMass, ForceMode2D.Impulse);
    }

    Camera.main.orthographicSize += Input.mouseScrollDelta.y;
  }

  //public void UpdateIndicators(Vector2 diff)
  //{
  //  Physics2D.autoSimulation = false;
  //  GetComponent<Rigidbody2D>().simulated = false;
  //  var positions = new List<Vector2>();
  //  var simulation = GameObject.Instantiate(gameObject, null, true);
  //  simulation.GetComponent<Rigidbody2D>().simulated = true;
  //  //Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), simulation.GetComponent<CircleCollider2D>());
  //  simulation.GetComponent<Rigidbody2D>().AddForceAtPosition(diff, GetComponent<Rigidbody2D>().worldCenterOfMass, ForceMode2D.Impulse);
  //  var gravity = simulation.GetComponent<Gravity>();
  //  for (var i = 0; i < 10; i++)
  //  {
  //    Physics2D.Simulate(Time.fixedDeltaTime);
  //    Physics2D.SyncTransforms();
  //    gravity.FixedUpdate();
  //    positions.Add(new Vector2(simulation.transform.position.x, simulation.transform.position.y));
  //  }
  //  GameObject.DestroyImmediate(simulation);

  //  GetComponent<Rigidbody2D>().simulated = true;

  //  foreach (var indicator in indicators)
  //    GameObject.Destroy(indicator);
  //  foreach (var position in positions)
  //    indicators.Add(GameObject.Instantiate(indicatorPrefab, position, Quaternion.identity));

  //  Physics2D.autoSimulation = true;
  //}
}
