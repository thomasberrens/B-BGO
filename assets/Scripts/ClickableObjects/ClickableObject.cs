using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ClickableObject : MonoBehaviour {
    [field: SerializeField] public UnityEvent<GameObject> OnClickDown { get; set; }
    [field: SerializeField] public UnityEvent<GameObject> OnExit { get; set; }
    [field: SerializeField] public UnityEvent<GameObject> OnEnter { get; set; }
    [field: SerializeField] public UnityEvent<GameObject> OnClickUp { get; set; }
    [field: SerializeField] public UnityEvent<GameObject> OnHover { get; set; }

    private void OnMouseDown() => OnClickDown?.Invoke(gameObject);
    private void OnMouseExit() => OnExit?.Invoke(gameObject);
    private void OnMouseOver() => OnHover?.Invoke(gameObject);
    private void OnMouseEnter() => OnEnter?.Invoke(gameObject);
    private void OnMouseUp() => OnClickUp?.Invoke(gameObject);
}
