using Cysharp.Threading.Tasks;
using UnityEngine;

public class CharacterEcoEvent : MonoBehaviour
{
    private MovePlayer playerCharacter;
    private GameObject playerCane;

    [SerializeField]
    private float endStopEcoTime = 2.0f;

    private int caneEcoCountMax = 1;
    private int caneEcoCount = 0;
    private int walkEcoCountMax = 1;
    private int walkEcoCount = 0;
    private int runEcoCountMax = 1;
    private int runEcoCount = 0;
    private float startStopEcoTime = 0;

    private void Awake()
    {
        Transform[] allObjects = transform.GetComponentsInChildren<Transform>();

        foreach (Transform obj in allObjects)
        {
            if (obj.name == "cane")
            {
                playerCane = obj.gameObject;
                break;
            }
        }
        playerCharacter = transform.parent.GetComponent<MovePlayer>();
    }

    private void Update()
    {
        if (playerCharacter.MoveDirection == Vector3.zero)
        {
            startStopEcoTime += Time.deltaTime;

            if (startStopEcoTime > endStopEcoTime)
            {
                startStopEcoTime = 0.0f;
                StopEvent();
            }
        }
        else
        {
            startStopEcoTime = 0.0f;
        }
    }

    public void StopEvent()
    {
        if (!playerCharacter.IsEco) return;

        playerCharacter.StopEco(playerCharacter.transform);
    }

    public async void CaneEvent()
    {
        if (!playerCharacter.IsEco) return;
        GameManager.Instance.SoundManager.PlaySound2D("Sound/caneSound", SoundType.SFX, 1);

        caneEcoCount = 0;

        while (caneEcoCount < caneEcoCountMax)
        {
            await UniTask.Delay(600);
            playerCharacter.CaneEco(playerCane.transform);
            caneEcoCount++;
        }
    }

    public async void WalkEvent()
    {
        if (!playerCharacter.IsEco) return;
     
        walkEcoCount = 0;

        while (walkEcoCount < walkEcoCountMax)
        {
            await UniTask.Delay(600);
            playerCharacter.WalkEco(playerCharacter.transform);
            walkEcoCount++;
        }
    }

    public async void RunEvent()
    {
        if (!playerCharacter.IsEco) return;
       
        runEcoCount = 0;

        while (runEcoCount < runEcoCountMax)
        {
            await UniTask.Delay(800);
            playerCharacter.RunEco(playerCharacter.transform);
            runEcoCount++;
        }
    }

    public void WalkSoundEvent()
    {
        GameManager.Instance.SoundManager.PlaySound2D("Sound/walk", SoundType.SFX, 0.2f, 1.0f);
    }

    public void RunSoundEvent()
    {
        GameManager.Instance.SoundManager.PlaySound2D("Sound/walk", SoundType.SFX, 0.2f, 1.0f);
    }
}
