using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConsoleCommand : ScriptableObject
{
    public string CommandName;
    public string Command;
    [TextArea]
    public string CommandStructure;

    
    virtual public bool IsValidated(string[] input)
    {
        if (input[0] == Command)
            {
                return true;
            }

            return false;
    }

    abstract public void Execute();
}
/*

je créer un script ConsoleCommandData dedans 
je créer tout les class (commands) et ensuite dans ma liste je les attribues dnas une liste

*/