using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(fileName = "AddModules", menuName = "CommandConsole/AddModules", order = 1)]
public class AddModulesCommand : ConsoleCommand
{

        private List<Module> _modules = new List<Module>();
        public override bool IsValidated(string[] input)
        {
            if (base.IsValidated(input))
            {
                if (input.Length == 1) return false;  
                _modules.Clear();
                for (int i = 1; i < input.Length; i++)
                {
                    bool isContain = false; 
                    for (int j = 0; j < ConsoleData.Instance.ModuleList.Count; j++)
                    {
                        if (ConsoleData.Instance.ModuleList[j].Name == input[i])
                        {
                            isContain = true;
                            _modules.Add(ConsoleData.Instance.ModuleList[j].Module);
                            break;
                        }
                    }
                    if (!isContain)
                    {
                        return false; 
                    }
                }

                return true;
            }

            return false;
        }

        public override void Execute()
        {
            for (int i = 0; i < _modules.Count; i++)
            {
                if (!PlayerManager.instance.obtainedModule.Contains(_modules[i]))
                {
                    PlayerManager.instance.AddModule(_modules[i]);  
                }
            }
        }
}
