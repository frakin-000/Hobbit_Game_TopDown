using UnityEngine;

public class StartDialog : MonoBehaviour
{
    public GameObject Dialog;
    private bool IsDialog;

    private void Start()
    {
        Dialog.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Dialog.SetActive(true);
            IsDialog = true;
        }
    }

    public bool DialogCondition()
    {
        return IsDialog;
    }

    public void EndDialog()
    {
        Dialog.SetActive(false);
        IsDialog = false;
    }
}
