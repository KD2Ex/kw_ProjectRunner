using UnityEngine;

public class PrefabRender : MonoBehaviour
{
    [SerializeField] RuntimeAnimatorController[] listPrefab;
    [SerializeField] Animator anim;
    [Space]
    [SerializeField] SpriteRenderer spriteRenderer;

    public void SetDataStart(int layer, Sprite sprite, LayerPosition layerPosition, int index = 0, Animator animator = null)
    {
        if (layerPosition == LayerPosition.Top)
        {
            anim.enabled = true;
            anim.runtimeAnimatorController = listPrefab[1];
            if (animator != null)
            {
                AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
                anim.Play(stateInfo.fullPathHash, 0, stateInfo.normalizedTime);
            }
            //switch (index)
            //{
            //    case 0: anim.runtimeAnimatorController = listPrefab[0]; break;
            //    case 1: anim.runtimeAnimatorController = listPrefab[1]; break;
            //}
        }
        else
        {
            anim.enabled = false;
            spriteRenderer.sprite = sprite;
        }
        spriteRenderer.sortingOrder = layer;
    }
    public SpriteRenderer GetSpriteRenderer() => spriteRenderer;
    public void SetPos(Vector2 e) => transform.position = e;
}