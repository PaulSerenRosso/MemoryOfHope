using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(fileName = "AddAllModule", menuName = "CommandConsole/AddAllModule", order = 1)]
public class AddAllModuleCommand : ConsoleCommand
{
  
        public override void Execute()
        {
            for (int i = 0; i < ConsoleData.Instance.ModuleList.Count; i++)
            {
                if (!PlayerManager.instance.obtainedModule.Contains(ConsoleData.Instance.ModuleList[i].Module))
                {
                    PlayerManager.instance.AddModule(ConsoleData.Instance.ModuleList[i].Module);  
                }
            }
        }
    
}
