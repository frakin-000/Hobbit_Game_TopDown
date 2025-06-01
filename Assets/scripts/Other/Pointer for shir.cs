using System;
using UnityEditor.Build;
using UnityEngine;

[ExecuteAlways]
public class Pointerforshir : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private new Camera camera;
    [SerializeField] private Transform pointerIconTransform;
    [SerializeField] private Transform farm;
    [SerializeField] private Transform bar;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform ork;
    [SerializeField] private Transform exit;
    private Transform currentTarget;

    private enum PointerTarget
    {
        Farm,
        Bar,
        FirePoint,
        Ork,
        Exit
    }

    private PointerTarget current = PointerTarget.Farm;
    
    
    void Update()
    {
        switch (current)
        {
            case PointerTarget.Farm:
                currentTarget = farm;
                if (Vector3.Distance(player.position, farm.position) < 2f)
                {
                    current = PointerTarget.Bar;
                    currentTarget = bar;
                }
                break;
        
            case PointerTarget.Bar:
                currentTarget = bar;
                if (Vector3.Distance(player.position, bar.position) < 2f)
                {
                    current = PointerTarget.FirePoint;
                    currentTarget = firePoint;
                }
                break;
        
            case PointerTarget.FirePoint:
                currentTarget = firePoint;
                if (Vector3.Distance(player.position, firePoint.position) < 2f)
                {
                    current = PointerTarget.Ork;
                    currentTarget = ork;
                }
                break;
            case PointerTarget.Ork:
                currentTarget = ork;
                if (Vector3.Distance(player.position, ork.position) < 2f)
                {
                    current = PointerTarget.Exit;
                    currentTarget = exit;
                }
                break;
            case PointerTarget.Exit:
                currentTarget = exit;
                break;
        }

        if (currentTarget is not null)
        {
            ChangePointer(currentTarget);
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
        pointerIconTransform.position = camera.WorldToScreenPoint(worldPosition);
    }
}
