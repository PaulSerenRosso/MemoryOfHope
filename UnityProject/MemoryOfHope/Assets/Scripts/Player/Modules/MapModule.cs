using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapModule : Module
{
    public override void LinkModule()
        {
            Debug.Log("Linking Inputs for Map Module");
    
            PlayerController.instance.playerActions.Player.OpenCloseMap.performed += context => InputPressed(context);
            PlayerController.instance.playerActions.Player.OpenCloseMap.canceled += context => InputReleased(context);
        }
    
        public override void InputPressed(InputAction.CallbackContext ctx)
        {
            inputPressed = ctx.performed;
        }
    
        public override void InputReleased(InputAction.CallbackContext ctx)
        {
            inputPressed = ctx.performed;
            Release();
        }
        
        public override bool Conditions()
        {
            if (!base.Conditions())
            {
                return false;
            }

            return true;
        }
        
        public override void Execute()
        {
            Debug.Log("Executing Map Module");

            if (!isPerformed)
            {
                StartCoroutine(WatchingMap());
            }

        }

        IEnumerator WatchingMap()
        {
            isPerformed = true;
            UIInstance.instance.OpeningMap();

            yield return new WaitWhile(() => UIInstance.instance.map.activeSelf);

            yield return new WaitForSeconds(0.25f);
            isPerformed = false;
        }

        public override void Release()
        {
            
        }
}
