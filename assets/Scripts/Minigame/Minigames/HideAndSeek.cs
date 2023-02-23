using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HideAndSeek : MonoBehaviour{
    [field: SerializeField] public List<HideAndSeekCharacter> CharactersToFind { get; private set; }
    [field: SerializeField] public Image CheckMark { get; private set; }

    private int foundCharacters = 0;
    
    private void Awake()
    {
        CharactersToFind.ForEach(character => character.foundCharacter += OnFoundCharacter);
    }

    private void OnFoundCharacter(GameObject foundCharacter, Image characterImage)
    {
        foundCharacter.SetActive(false);

        // add CheckMark as child for characterImage
        Instantiate(CheckMark.gameObject, characterImage.transform, false);
        
       foundCharacters++;
       
       
       if (foundCharacters >= CharactersToFind.Count)
       {
           // TODO: trigger on minigame win event
           Debug.Log("Game won.");
       }
    }
}
