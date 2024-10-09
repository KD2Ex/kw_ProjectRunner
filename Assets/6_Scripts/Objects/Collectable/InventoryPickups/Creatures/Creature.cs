using UnityEngine;

public enum CreatureType
{
    BUBU,
    SPIDER,
    KISO,
    ZUZU,
    SUN,
}

[CreateAssetMenu(fileName = "Creature", menuName = "Scriptable Objects/Collectable/Creature/Type")]
public class Creature : ScriptableObject
{
    [field:SerializeField] public CreatureType Type { get; private set; }

}
