using UnityEngine;

public class TipTrigger : MonoBehaviour
{
    [SerializeField] private string message;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TipManager.displayTipEvent?.Invoke(message);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TipManager.disableTipEvent?.Invoke();
        }
    }
}
