using UnityEngine;

public class FollowParentPos : MonoBehaviour
{
    [SerializeField]
    Vector3 offset;

    private void Awake()
    {
        transform.position = transform.parent.position + offset;
    }
}
