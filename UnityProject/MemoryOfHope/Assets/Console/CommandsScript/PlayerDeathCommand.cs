using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDeath", menuName = "CommandConsole/PlayerDeath", order = 1)]
public class PlayerDeathCommand : ConsoleCommand
{
    public override void Execute()
    {
        PlayerManager.instance.Death();
    }
}