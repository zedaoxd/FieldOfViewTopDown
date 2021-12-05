using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownFOV : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float angle;
    [SerializeField] private float radius;

    private void OnDrawGizmos()
    {
        Gizmos.color = CanSeeTarget() ? Color.green : Color.white;
        Gizmos.DrawWireSphere(transform.position, radius);
        DrawLineView();
    }

    private void DrawLineView()
    {
        var forward = transform.forward;
        var position = transform.position;
        var leftDir = Quaternion.Euler(0, angle * 0.5f, 0) * forward;
        var rightDir = Quaternion.Euler(0, -angle * 0.5f, 0) * forward;
        
        Gizmos.DrawLine(position, leftDir.normalized * radius);
        Gizmos.DrawLine(position, rightDir.normalized * radius);
    }

    private bool CanSeeTarget()
    {
        var distance = target.position - transform.position;
        if (SqrtMagnitude(distance) > Squared(radius)) return false;

        var dot = DotProduct(transform.forward, distance);
        if (dot < 0) return false;

        var cos = dot / (Magnitude(distance) * Magnitude(transform.forward));
        return (Mathf.Acos(cos) * Mathf.Rad2Deg) <= (angle * 0.5f);
    }

    private static float DotProduct(Vector3 a, Vector3 b)
    {
        return a.x * b.x + a.y * b.y + a.z * b.z;
    }

    private static float SqrtMagnitude(Vector3 vec)
    {
        return vec.x * vec.x + vec.y * vec.y + vec.z * vec.z;
    }

    private static float Magnitude(Vector3 vec)
    {
        return Mathf.Sqrt(SqrtMagnitude(vec));
    }

    private static float Squared(float number)
    {
        return number * number;
    }
}
