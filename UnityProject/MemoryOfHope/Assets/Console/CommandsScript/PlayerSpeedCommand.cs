using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSpeed", menuName = "CommandConsole/PlayerSpeed", order = 1)]
public class PlayerSpeedCommand : ConsoleCommand
{
    private float _speedFactor;

    public override bool IsValidated(string[] input)
    {
        if (input.Length != 2) return false;
        if (base.IsValidated(input))
        {
            if (float.TryParse(input[1], out _speedFactor) != null)
            {
                if (_speedFactor != 0)
                {
                       return true;
                }
             
            }
        }

        return false;
    }

    public override void Execute()
    {
        ConsoleData.Instance.Move.defaultSpeedMovment = ConsoleData.Instance.DefaultSpeed * _speedFactor;
    }
}