using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInvulnerable", menuName = "CommandConsole/PlayerInvulnerable", order = 1)]
public class PlayerInvulnerableCommand : ConsoleCommand
{
    private bool _isInvulnerable;

    public override bool IsValidated(string[] input)
    {
        if (input.Length != 2) return false;
        if (base.IsValidated(input))
        {
            if (input[1] == "off")
            {
                _isInvulnerable = false;
                return true;
            }

            if (input[1] == "on")
            {
                _isInvulnerable = true;
                return true;
            }
        }

        return false;
    }

    public override void Execute()
    {
        PlayerManager.instance.IsInvincible = _isInvulnerable;
    }
}