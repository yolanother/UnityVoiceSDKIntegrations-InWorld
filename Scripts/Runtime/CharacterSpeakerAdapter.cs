using System;
using Inworld;
using Meta.WitAi;
using Meta.WitAi.TTS.Integrations;
using Meta.WitAi.TTS.Utilities;
using UnityEditor;
using UnityEngine;

namespace VoiceSDK.Integrations.Inworld
{
    public class CharacterSpeakerAdapter : MonoBehaviour
    {
        [SerializeField] private TTSSpeaker _speaker;
        [SerializeField] private InworldInteraction _interaction;

        private void OnEnable()
        {
            _interaction.InworldEvents.OnSpeak.AddListener(_speaker.SpeakQueued);
        }

        private void OnDisable()
        {
            _interaction.InworldEvents.OnSpeak.RemoveListener(_speaker.SpeakQueued);
        }

        #if UNITY_EDITOR
        [MenuItem("GameObject/Voice SDK/Inworld/Character Speaker", false, 10)]
        private static void CreateSpeaker()
        {
            var voiceService = FindObjectOfType<VoiceService>();
            var runtimeConfig = (voiceService is Wit wit) ? wit.RuntimeConfiguration : null;
            string[] witGuids;
            var ttsWit = FindObjectOfType<TTSWit>();
            if (!ttsWit)
            {
                witGuids = AssetDatabase.FindAssets("TTSWit");
                if (witGuids.Length > 0)
                {
                    var witPath = AssetDatabase.GUIDToAssetPath(witGuids[0]);
                    ttsWit = Instantiate(AssetDatabase.LoadAssetAtPath<TTSWit>(witPath));
                    ttsWit.name = "TTSWit";
                    ttsWit.RequestSettings.configuration = runtimeConfig?.witConfiguration;
                }
            }

            TTSSpeaker speaker;
            witGuids = AssetDatabase.FindAssets("TTSSpeaker");
            if (witGuids.Length > 0)
            {
                var witPath = AssetDatabase.GUIDToAssetPath(witGuids[0]);
                speaker = Instantiate(AssetDatabase.LoadAssetAtPath<TTSSpeaker>(witPath));
                speaker.name = "TTSSpeaker";
            }
            else
            {
                var gameObject = new GameObject("Character Speaker");
                speaker = gameObject.AddComponent<TTSSpeaker>();
                var audioSource = speaker.gameObject.AddComponent<AudioSource>();
                speaker.AudioSource = audioSource;
            }

            speaker.name = "Inworld Character Speaker";

            var adapter = speaker.gameObject.AddComponent<CharacterSpeakerAdapter>();
            
            adapter._speaker = speaker;
            adapter._interaction = FindObjectOfType<InworldInteraction>();
            if (adapter._interaction)
            {
                speaker.name = adapter._interaction.name + " Speaker";
            }
            if (Selection.activeGameObject)
            {
                speaker.transform.parent = Selection.activeGameObject.transform;
            }
        }
        #endif
    }
}