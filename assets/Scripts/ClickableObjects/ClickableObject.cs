using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ClickableObject : MonoBehaviour {
    
    [SerializeField] private UnityEvent<GameObject> onMouseDown = new UnityEvent<GameObject>();
    [SerializeField] private UnityEvent<GameObject> onMouseExit = new UnityEvent<GameObject>();
    [SerializeField] private UnityEvent<GameObject> onMouseEnter = new UnityEvent<GameObject>();
    [SerializeField] private UnityEvent<GameObject> onMouseUp = new UnityEvent<GameObject>();
    [SerializeField] private UnityEvent<GameObject> onMouseOver = new UnityEvent<GameObject>();
    [SerializeField] private ScoreSystem scoreSystem;
    
    private void OnMouseDown() 
    {
        scoreSystem.ReceivedClick(gameObject);
        onMouseDown?.Invoke(gameObject); 
    }
    
    private void OnMouseExit() => onMouseExit?.Invoke(gameObject);
    private void OnMouseOver() => onMouseOver?.Invoke(gameObject);
    private void OnMouseEnter() => onMouseEnter?.Invoke(gameObject);
    private void OnMouseUp() => onMouseUp?.Invoke(gameObject);
}
