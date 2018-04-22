using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {
  public float InitialAngularVelocity = 0;
  public void Start()
  {
    GetComponent<Rigidbody2D>().angularVelocity = InitialAngularVelocity;
  }
}
