using UnityEngine;

public class EnableCompositeCollider : MonoBehaviour
{
    private CompositeCollider2D compositeCollider;

    private void Start()
    {
        compositeCollider = GetComponent<CompositeCollider2D>();
        compositeCollider.enabled = true;
    }
}
