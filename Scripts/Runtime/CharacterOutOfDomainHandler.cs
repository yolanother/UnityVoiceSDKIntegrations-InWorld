using Inworld;
using Meta.WitAi;
using Meta.WitAi.CallbackHandlers;
using Meta.WitAi.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace VoiceSDK.Integrations.Inworld
{
    public class CharacterOutOfDomainHandler : WitResponseHandler
    {
        [SerializeField] private InworldInteraction _interaction;
        [SerializeField] private UnityEvent<string> _onOutOfDomain = new UnityEvent<string>();

        #if UNITY_EDITOR
        [MenuItem("GameObject/Voice SDK/Inworld/Character Out of Domain Handler", false, 10)]
        private static void CreateComponent(MenuCommand menuCommand)
        {
            var go = new GameObject("Character Out of Domain Handler");
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            var handler = go.AddComponent<CharacterOutOfDomainHandler>();
            handler._interaction = FindObjectOfType<InworldInteraction>();
            if (Selection.activeGameObject)
            {
                handler.transform.parent = Selection.activeGameObject.transform;
            }
            Selection.activeObject = go;
        }
        #endif

        protected override string OnValidateResponse(WitResponseNode response, bool isEarlyResponse)
        {
            if (response == null)
            {
                return "Response is null";
            }
            if (response["intents"].Count > 0)
            {
                return "Intents found";
            }
            return string.Empty;
        }
        protected override void OnResponseInvalid(WitResponseNode response, string error) {}
        protected override void OnResponseSuccess(WitResponseNode response)
        {
            var transcription = response.GetTranscription();
            _onOutOfDomain?.Invoke(transcription);
            _interaction.SendText(transcription);
        }
    }
}