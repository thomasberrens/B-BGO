
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(ClickableObject))]
    public class HideAndSeekCharacter : MonoBehaviour
    {
        [field: SerializeField] public Image CharacterImage { get; private set; }

        public event Action<GameObject, Image> foundCharacter; 

        private void Awake()
        {
        }

        public void FoundObject(GameObject gameObject)
        {
            Debug.Log("FOUND");
            foundCharacter?.Invoke(gameObject, CharacterImage);
        }
    }
