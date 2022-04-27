using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CharacterDialogueProfil", menuName = "Event/GameEvent/Dialogue/CharacterDialogueProfil", order = 1)]
public class CharacterDialogueProfil : ScriptableObject
{
    public string FrenchName;
    public string EnglishName;
    public CharacterDialogueProfilEnum Character;
}
