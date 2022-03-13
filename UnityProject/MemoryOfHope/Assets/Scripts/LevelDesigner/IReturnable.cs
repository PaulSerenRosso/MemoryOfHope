using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReturnable 
{
  public bool IsReturnLaser { get; set; }
  
  public bool IsActiveReturnable { get; set; }
  void Returnable(LaserMachine laser, RaycastHit hit);

  void Cancel(LaserMachine laser);

  void StartReturnable(LaserMachine laser, RaycastHit hit);

}
