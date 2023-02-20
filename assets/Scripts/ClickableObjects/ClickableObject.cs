using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ClickableObject : MonoBehaviour {
    
    [SerializeField] private UnityEvent<GameObject> onMouseDown = new UnityEvent<GameObject>();

    private Action<GameObject> onMouseExit;

    private void Start()
    {
        
    }

    private void OnMouseDown()
    {
        // This code will be executed when the object is clicked
        Debug.Log("Object Clicked!");
        onMouseDown.Invoke(gameObject);
    }

    private void OnMouseExit()
    {
        Debug.Log("Mouse Exit");
        
        onMouseExit?.Invoke(gameObject);
    }
    
    private void OnMouseOver()
    {
        Debug.Log("Mouse Over");
    }
    
    private void OnMouseEnter()
    {
        Debug.Log("Mouse Enter");
    }

    private void OnMouseUp()
    {
        Debug.Log("Mouse Up");
    }
}
