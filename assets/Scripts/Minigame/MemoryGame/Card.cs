using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Sprite front;
    public Sprite back;

    public bool _matched = false;

    public int id;

    public delegate void CardClickedEvent(Card card);
    public event CardClickedEvent OnCardClicked;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetCard(int id, Sprite image)
    {
        this.id = id;
        front = image;
    }

    public void FlipCard()
    {
        _spriteRenderer.sprite = front;
    }

    public void UnflipCard()
    {
        if (!_matched)
        {
            _spriteRenderer.sprite = back;
        }
    }

    public void OnMouseDown()
    {
        if (OnCardClicked != null)
        {
            OnCardClicked(this);
        }
    }
}