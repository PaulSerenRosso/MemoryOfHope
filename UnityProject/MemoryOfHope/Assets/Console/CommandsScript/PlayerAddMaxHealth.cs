using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(fileName = "PlayerAddMaxHealth", menuName = "CommandConsole/PlayerAddMaxHealth", order = 1)]
public class PlayerAddMaxHealth : ConsoleCommand
{
   
        private int _maxHealthFactor;

        public override bool IsValidated(string[] input)
        {
            if (input.Length != 2) return false;
            if (base.IsValidated(input))
            {
                if (int.TryParse(input[1], out _maxHealthFactor) != null)
                {
                    return true;
                }
            }

            return false;
        }
        public override void Execute()
        {
            if (_maxHealthFactor == 0)
            {
                int lostHp = PlayerManager.instance.maxHealth - PlayerManager.instance.health;
                UIInstance.instance.DisplayHealth();
                PlayerManager.instance.Heal(lostHp);
            }
            for (int i = 0; i < _maxHealthFactor; i++)
            {
                  PlayerManager.instance.maxHealth += 4;
                            int lostHp = PlayerManager.instance.maxHealth - PlayerManager.instance.health;
                            UIInstance.instance.GetHeart();
                            UIInstance.instance.DisplayHealth();
                            PlayerManager.instance.Heal(lostHp);
            }
          
        }
    
}
