using Inworld;
using Inworld.Data;
using Meta.WitAi;
using Meta.WitAi.CallbackHandlers;
using Meta.WitAi.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace VoiceSDK.Integrations.Inworld
{
    public class CharacterIntentHandler : SimpleIntentHandler
    {
        [Header("Inworld")]
        [SerializeField] private InworldInteraction _interaction;
        [SerializeField] private UnityEvent<string> onInteractionComplete;

        protected override void OnResponseSuccess(WitResponseNode response)
        {
            base.OnResponseSuccess(response);
            var transcription = response.GetTranscription();
            if (!string.IsNullOrEmpty(transcription))
            {
                _interaction.SendText(transcription, (r) => onInteractionComplete.Invoke(r.GetTranscription()));
            }
        }

#if UNITY_EDITOR
        [MenuItem("GameObject/Voice SDK/Inworld/Character Intent Handler", false, 10)]
        private static void CreateIntentHandler()
        {
            var handler = new GameObject("Intent Handler");
            var intentHandler = handler.AddComponent<CharacterIntentHandler>();
            if (Selection.activeGameObject)
            {
                handler.transform.parent = Selection.activeGameObject.transform;
            }

            intentHandler._interaction = handler.GetComponentInParent<InworldInteraction>();
            if (!intentHandler._interaction)
            {
                intentHandler._interaction = FindObjectOfType<InworldInteraction>();
            }
        }
        #endif
    }
}