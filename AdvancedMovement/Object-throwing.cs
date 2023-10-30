using System.Collections;
using System.Collections.Genetic;
using UnityEngine;
using TmPro;

public class Throwing : MonoBehavior
{
  [Header("References")]
  public Transform cam;
  public Transform attackpoint;
  public GameObkect objectToThrow;

  [Header("Settings")]
  public int totalThrows;
  public float throwCooldown;

  [Header("Throwing"}]
  public KeyCode throwKey = KeyCode.Mouse0;
  public float throwForce;
  public float throwUpwardForce;

  bool readyToThrow;

  private void Start()
  {
    readyToThrow = true;
  }

  private void Update
  {
    if(Input.GetKeyDown(throwKey) && readyToThrow && totalThrows > 0)
    {
      Throw();
    }
    
  }

  private void Throw()
  {
    readyToThrow = false;

    GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);

    Rididbody projectileRb = projectile.GetComponent<Rigidbody>();

    Vector3 forceToAdd = cam.transform.forward * throwForce + transform.up * throwUpwardForce;
  }
  
}
vector3
