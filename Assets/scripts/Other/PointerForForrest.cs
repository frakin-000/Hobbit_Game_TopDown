using System;
using UnityEngine;

public class PointerForForrest : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private new Camera camera;
    [SerializeField] private Transform pointerIconTransform;
    [SerializeField] private Transform exit;
    [SerializeField] private GameObject pointerIcon;

    private void Update()
    {
        if (EnemyDie.Instance is not null && EnemyDie.Instance.IsEveryoneDead())
        {
            pointerIcon.SetActive(true);
            ChangePointer(exit);
        }
        else
        {
            pointerIcon.SetActive(false);
        }
    }

    private void ChangePointer(Transform goal)
    {
        var fromPlayerToTarget = goal.position - player.position;
        var ray = new Ray(player.position, fromPlayerToTarget);
        var planes = GeometryUtility.CalculateFrustumPlanes(camera);
        var minDistance = Mathf.Infinity;
        for (var i = 0; i < 4; i++)
        {
            if (planes[i].Raycast(ray, out var distance))
            {
                if (distance < minDistance)
                {
                    minDistance = distance;
                }
            }
        }
        minDistance = Mathf.Clamp(minDistance, 0, fromPlayerToTarget.magnitude);
        var worldPosition = ray.GetPoint(minDistance);
        //pointerIconTransform.position = camera.WorldToScreenPoint(worldPosition);
        var newPosition = camera.WorldToScreenPoint(worldPosition);
        newPosition.x = Math.Max(130, newPosition.x);
        newPosition.y = Math.Min(1300, newPosition.y);
        pointerIconTransform.position = newPosition;
    }
}
