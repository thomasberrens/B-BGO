
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(ClickableObject))]
    public class HideAndSeekCharacter : MonoBehaviour
    {
        [field: SerializeField] public Image CharacterImage { get; private set; }

        public event Action<GameObject, Image> foundCharacter;

        public void FoundObject(GameObject gameObject)
        {
            foundCharacter?.Invoke(gameObject, CharacterImage);
        }
    }
