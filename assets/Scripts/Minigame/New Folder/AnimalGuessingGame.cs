using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AnimalGuessingGame : MonoBehaviour
{
    [SerializeField] TMP_Text promptText;
    [SerializeField] private Button buttonA;
    [SerializeField] private Button buttonB;
    [SerializeField] private Button buttonC;
    [SerializeField] private Button buttonD;
    [SerializeField] private Button buttonE;
    [SerializeField] private List<AudioClip> animalSounds = new List<AudioClip>();
    
    private readonly List<string> _animalArray = new List<string>(){"Eekhoorn", "Egel", "Vogel", "Muis", "Konijn"};
    private List<string> _temporaryAnimalArray = new List<string>();
    private AudioSource _audioSource;
    private string _correctAnimal;
    private int _randomAnimal;
    private int _correctGuesses;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _temporaryAnimalArray = _animalArray.ToList();
        promptText.text = "Welk dieren geluid is dit?";
        
        GenerateNextAnswser();
        // Set the button labels to display the animal options
        buttonA.GetComponentInChildren<TMP_Text>().text = _animalArray[0];
        buttonB.GetComponentInChildren<TMP_Text>().text = _animalArray[1];
        buttonC.GetComponentInChildren<TMP_Text>().text = _animalArray[2];
        buttonD.GetComponentInChildren<TMP_Text>().text = _animalArray[3];
        buttonE.GetComponentInChildren<TMP_Text>().text = _animalArray[4];

        // Set the click listeners to call the CheckAnswer function with the appropriate animal option
        buttonA.onClick.AddListener(() => CheckAnswer(_animalArray[0], _randomAnimal));
        buttonB.onClick.AddListener(() => CheckAnswer(_animalArray[1], _randomAnimal));
        buttonC.onClick.AddListener(() => CheckAnswer(_animalArray[2], _randomAnimal));
        buttonD.onClick.AddListener(() => CheckAnswer(_animalArray[3], _randomAnimal));
        buttonE.onClick.AddListener(() => CheckAnswer(_animalArray[4], _randomAnimal));
    }

    void GenerateNextAnswser()
    {
        // Randomly choose one of the animals to be the correct answer
        _randomAnimal = Random.Range(0, _temporaryAnimalArray.Count);

        switch (_randomAnimal)
        {
            case 0:
                _correctAnimal = _temporaryAnimalArray[0];
                _audioSource.clip = animalSounds[0];
                break;
            case 1:
                _correctAnimal = _temporaryAnimalArray[1];
                _audioSource.clip = animalSounds[1];
                break;
            case 2:
                _correctAnimal = _temporaryAnimalArray[2];
                _audioSource.clip = animalSounds[2];
                break;
            case 3:
                _correctAnimal = _temporaryAnimalArray[3];
                _audioSource.clip = animalSounds[3];
                break;
            case 4:
                _correctAnimal = _temporaryAnimalArray[4];
                _audioSource.clip = animalSounds[4];
                break;
            default:
                Debug.Log("Index out of bounds");
                break;
        }
        _audioSource.Play();
    }

    void CheckAnswer(string guess, int animalIndex)
    {
        if (guess == _correctAnimal)
        {
            promptText.text = "Correct!";
            _audioSource.Stop();
            _temporaryAnimalArray.Remove(guess);
            animalSounds.RemoveAt(animalIndex);
            _correctGuesses++;

            if (_correctGuesses == _animalArray.Count) FinishGame();
            else GenerateNextAnswser();
        }
        else
        {
            promptText.text = "Incorrect. Try again.";
            _audioSource.Play();
        }
    }

    void FinishGame()
    {
        promptText.text = "Goedzo! Je hebt alle dieren geraden";
        Debug.Log("finished");
    }

    public void PlaySoundAgain()
    {
        _audioSource.Play();
    }
}