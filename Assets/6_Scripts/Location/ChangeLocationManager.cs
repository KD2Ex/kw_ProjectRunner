using UnityEngine;

public class ChangeLocationManager : MonoBehaviour
{
    [Header("Player inventories")]
    [SerializeField] private GInventory PlayerCreatures;
    [SerializeField] private GInventory PlayerFood;
    
    public void CheckCondition()
    {
        // здесь проверяется только Count, потому что спавны creatures
        // привязаны к локации (на первой локе не могут появится creatures со
        // второй и т. д.
        // еды впринципе всегда максимум 4 штуки и всегда столько и нужно, чтобы
        // перейти на некст левел
        // Спавн еды не повторяющийся, всегда может быть только один экземпляр каждой
        // еды в инвентаре у плеера
        
        if (PlayerCreatures.Items.Count == 8 && PlayerFood.Items.Count == 4)
        {

            Debug.Log("Change Location");
            // Change Location
        }
        else
        {
            Debug.Log($"Creatures: {PlayerCreatures.Items.Count}");
            Debug.Log($"Foods: {PlayerFood.Items.Count}");
        }
        
    }
}
