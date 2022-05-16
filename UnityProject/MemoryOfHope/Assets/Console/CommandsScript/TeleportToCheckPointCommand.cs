using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(fileName = "TeleportToCheckPoint", menuName = "CommandConsole/TeleportToCheckPoint", order = 1)]
public class TeleportToCheckPointCommand : ConsoleCommand
{
    
        private CheckPoint _checkPoint;

        public override bool IsValidated(string[] input)
        {
            if (input.Length != 2) return false;
            {
                if (base.IsValidated(input))
                {
                    for (int i = 0; i < ConsoleData.Instance.CheckPointList.Count; i++)
                    {
                        if (ConsoleData.Instance.CheckPointList[i].Name == input[1])
                        {
                            _checkPoint = ConsoleData.Instance.CheckPointList[i].Point;
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public override void Execute()
        {
            PlayerManager.instance.transform.position = _checkPoint.SpawnPosition.position;
        }
    
}
