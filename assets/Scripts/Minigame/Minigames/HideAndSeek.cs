using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class HideAndSeek : MonoBehaviour{
    [field: SerializeField] public List<HideAndSeekCharacter> CharactersToFind { get; private set; }
    [field: SerializeField] public Image CheckMark { get; private set; }
    private ScoreSystem ScoreSystem = new ScoreSystem();

    private int foundCharacters = 0;
    
    private void Awake()
    {
        ScoreSystem._maxScore = CharactersToFind.Count;
        ScoreSystem.maxScoreReached += OnWin;
        CharactersToFind.ForEach(character => character.foundCharacter += OnFoundCharacter);
    }
    
    private void OnWin()
    {
        Debug.Log("GAME WON HERE IS A CODE");
    }
    
    private void OnFoundCharacter(GameObject foundCharacter, Image characterImage)
    {
        foundCharacter.SetActive(false);

        // add CheckMark as child for characterImage
        Instantiate(CheckMark.gameObject, characterImage.transform, false);
        
       foundCharacters++;
       
       ScoreSystem.AddScore(1);
    }
}
