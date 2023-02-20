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

    private void OnMouseDown()
    {
        // This code will be executed when the object is clicked
        Debug.Log("Object Clicked!");
        onMouseDown?.Invoke(gameObject);
    }

    private void OnMouseExit()
    {
        Debug.Log("Mouse Exit");
        
        onMouseExit?.Invoke(gameObject);
    }
    
    private void OnMouseOver()
    {
        Debug.Log("Mouse Over");
        onMouseOver?.Invoke(gameObject);
    }
    
    private void OnMouseEnter()
    {
        Debug.Log("Mouse Enter");
        onMouseEnter?.Invoke(gameObject);
    }

    private void OnMouseUp()
    {
        Debug.Log("Mouse Up");
        onMouseUp?.Invoke(gameObject);
    }
}
