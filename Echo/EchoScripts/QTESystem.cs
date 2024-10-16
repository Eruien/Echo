using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

public class QTESystem : MonoBehaviour, INotifyPropertyChanged
{
    [SerializeField]
    private CutSceneTrigger cutSceneTrigger;
    [SerializeField]
    private float progressbar = 0.0f;
    [SerializeField]
    private float progressbarInitialValue = 50.0f;
    [SerializeField]
    private float progressbarComplete = 100.0f;
    [SerializeField]
    private float progressbarFail = 0.0f;
    [SerializeField]
    private float QTEReduceRate = 2.5f;
    [SerializeField]
    private float QTERecoveryRate = 5.0f;

    [SerializeField]
    private bool QTEStartTrigger = false;

    private bool IsQTEStart = false;

    public event PropertyChangedEventHandler PropertyChanged;

    public float Progressbar
    {
        get { return progressbar; }
        set 
        {
            progressbar = value;
            OnPropertyChanged("Progressbar");
        }
    }

    private void Awake()
    {
        Progressbar = progressbarInitialValue;
    }

    private void Update()
    {
        if (QTEStartTrigger)
        {
            QTEStart();
            QTEStartTrigger = false;
        }

        if (IsQTEStart)
        {
            CheckQTE();
        }
        else
        {
            Progressbar = progressbarInitialValue;
        }
    }

    public void QTEStart()
    {
        GameManager.Instance.UIManager.ShowUI<QTEUI>("UI/QTE UI");
        IsQTEStart = true;
    }

    void CheckQTE()
    {
        Progressbar = progressbar - (QTEReduceRate * Time.deltaTime);
        
        if (progressbar >= progressbarComplete)
        {
            Progressbar = progressbarInitialValue;
            IsQTEStart = false;
         
            cutSceneTrigger.FinishCutScene();
            GameManager.Instance.UIManager.CloaseCurrentUI();
        }
        else if (progressbar <= progressbarFail)
        {
            Progressbar = progressbarInitialValue;
            IsQTEStart = false;
            
            cutSceneTrigger.TrueEndingScene();
            GameManager.Instance.UIManager.CloaseCurrentUI();
        }
    }

    public void OnQTEPush(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Progressbar += QTERecoveryRate;
        }
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
