using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseConsoleCommand : MonoBehaviour
{
    public string CommandName;

    virtual public bool IsValidated(string[] input)
    {
        if (input[0] == CommandName)
        {
            return true; 
        }

        return false; 
    }
 
    abstract public void Execute();

}
