using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public void IsRight()
    {
        DialogController.Instance.NextQuestion();
    }

    public void IsFalse()
    {
        DialogController.Instance.Damage();
    }
}
