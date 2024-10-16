using UnityEngine;

public class KeypadCollisionCheck : MonoBehaviour
{
    [SerializeField]
    private MovePlayer playerCharacter;
    [SerializeField]
    private FollowCamera followCamera;
    [SerializeField]
    private GameObject KeypadUI;

    // 처음 키패드랑 상호작용했을때 체크용
    private bool oneCheckInteraciton = false;

    public bool OneCheckInteraciton
    {
        get { return oneCheckInteraciton; }
        set { oneCheckInteraciton = value; }
    }

    // 키패드 안에 Player태그가 들어왔을 때 작동
    // 키패드랑 상호작용 가능, 키패드용 UI 작동
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!oneCheckInteraciton)
            {
                KeypadUI.SetActive(true);
            }
            else
            {
                KeypadUI.SetActive(false);
            }
          
            if (playerCharacter.IsInteraction)
            {
                oneCheckInteraciton = true;
                playerCharacter.IsKeyPadSight = true;
                followCamera.IsTargetKeypad = true;
                playerCharacter.VisibleMousePointer(true);
                playerCharacter.SavePlayerPos();
                playerCharacter.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
      
    }

    // 충돌 범위를 벗어났을때 UI비활성화
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            KeypadUI.SetActive(false);
        }
    }
}
