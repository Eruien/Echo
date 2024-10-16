using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject targetPlayer;
    [SerializeField]
    private GameObject targetKeypad;
    [SerializeField]
    private Vector3 keypadOffset;

    private MovePlayer playerCharacter;
    private GameObject playerHead;
    private GameObject target;
    private Camera cameraComponent;

    private bool isTargetKeypad;

    public bool IsTargetKeypad
    {
        get { return isTargetKeypad; }
        set { isTargetKeypad = value; }
    }

    [SerializeField]
    private float cameraFOV = 60.0f;

    public float CameraFOV
    {
        get { return cameraFOV; }
        set { cameraFOV = value; }
    }

    void Awake()
    {
        cameraComponent = GetComponent<Camera>();
        cameraComponent.fieldOfView = CameraFOV;
        playerCharacter = targetPlayer.GetComponent<MovePlayer>();
        FindChildBone(playerCharacter.transform, "head");
        target = targetPlayer;
        playerCharacter.VisibleMousePointer(true);
    }

    void Update()
    {
        // 카메라 FOV SET되면 설정
        cameraComponent.fieldOfView = CameraFOV;

        if (isTargetKeypad)
        {
            target = targetKeypad;
            transform.position = target.transform.position + target.transform.forward * keypadOffset.x;
            transform.LookAt(target.transform);
        }
        else
        {
            target = targetPlayer;
            transform.position = playerHead.transform.position;
        }
    }

    // 계층에서 자식이 여러개일 경우 찾는 용
    void FindChildBone(Transform node, string boneName)
    {
        if (node.name == boneName)
        {
            playerHead = node.gameObject;
           
            return;
        }

        foreach (Transform child in node)
        {
            FindChildBone(child, boneName);
        }
    }
}
