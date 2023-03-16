
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(ClickableObject))]
    public class HideAndSeekCharacter : MonoBehaviour
    {
        [field: SerializeField] public Image CharacterImage { get; private set; }
        [field: SerializeField] public Animator Animation { get; set; }

        public event Action<GameObject, Image> foundCharacter;

        public void FoundObject(GameObject gameObject)
        {
            Animation.SetBool("isFound", true);
            foundCharacter?.Invoke(gameObject, CharacterImage);
            
       
        }
    }
