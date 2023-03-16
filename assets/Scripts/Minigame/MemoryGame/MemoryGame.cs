using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MemoryGame : MonoBehaviour
{
    public int rows = 2;
    public int columns = 4;
    public float offsetX = 2f;
    public float offsetY = 2.5f;
    public GameObject cardPrefab;
    public Sprite[] cardFaces;

    private Card _firstCard;
    private Card _secondCard;

    private int _score = 0;

    private void Start()
    {
        Vector3 startPos = new Vector3(-columns / 2 * offsetX + offsetX / 2, -rows / 2 * offsetY + offsetY / 2, 0f);

        List<int> numbers = new List<int>();

        for (int i = 0; i < rows * columns; i++)
        {
            numbers.Add(i);
        }

        numbers.Shuffle();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                GameObject card = Instantiate(cardPrefab, new Vector3(startPos.x + j * offsetX, startPos.y + i * offsetY, 0f), Quaternion.identity);
                Card cardScript = card.GetComponent<Card>();

                int index = i * columns + j;
                int id = numbers[index];
                cardScript.SetCard(id, cardFaces[id]);

                cardScript.OnCardClicked += HandleCardClicked;
            }
        }
    }
    

    private void HandleCardClicked(Card card)
    {
        if (_secondCard != null || card._matched) // if there's already a second card selected or the card has already been matched, return
            return;

        if (_firstCard == null)
        {
            _firstCard = card;
            _firstCard.FlipCard();
        }
        else
        {
            _secondCard = card;
            _secondCard.FlipCard();

            if (_secondCard.id == _firstCard.id)
            {
                // Match found
                _firstCard._matched = true;
                _secondCard._matched = true;
                _firstCard = null;
                _secondCard = null;
                Destroy(GetComponent<GameObject>());

                RemoveMatchedCards();
                _score++;
                Debug.Log("Score: " + _score);
            }
            else
            {
                // No match, flip the cards back over
                StartCoroutine(FlipCardsBackOver());
            }
        }
    }
    
    private IEnumerator FlipCardsBackOver()
    {
        yield return new WaitForSeconds(1f);

        _firstCard.UnflipCard();
        _secondCard.UnflipCard();

        _firstCard = null;
        _secondCard = null;
    }

    private void RemoveMatchedCards()
    {
        foreach (var card in FindObjectsOfType<Card>())
        {
            if (card._matched)
            {
                Destroy(card.gameObject);
            }
        }
    }
    
    private IEnumerator CheckCards()
    {
        yield return new WaitForSeconds(1f);

        if (_firstCard.id == _secondCard.id)
        {
            Destroy(_firstCard.gameObject);
            Destroy(_secondCard.gameObject);
            _score++;
            if (_score == (rows * columns) / 2)
            {
                Debug.Log("You won!");
            }
        }
        else
        {
            _firstCard.UnflipCard();
            _secondCard.UnflipCard();
        }

        _firstCard = null;
        _secondCard = null;
    }
}

public static class ListExtensions
{
    public static void Shuffle<T>(this List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }
}