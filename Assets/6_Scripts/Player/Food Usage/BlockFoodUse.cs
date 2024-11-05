using System.Collections;
using UnityEngine;

public class BlockFoodUse : MonoBehaviour
{
    private FoodUseManager manager => GameManager.instance.Player.FoodUseManager;

    private float Dist => Utils.DistanceToPlayer(transform);
    
    private IEnumerator Start()
    {
        yield return new WaitUntil(() => Dist < 14f);
        manager.BlockConsuming();
        yield return new WaitUntil(() => Dist < -14f);
        manager.UnblockConsuming();
    }
}