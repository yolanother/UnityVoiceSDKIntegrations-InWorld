# Voice SDK + Inworld SDK Integration
This package contains the integration of the Voice SDK with the Inworld SDK. It is a sample project that shows how to use the Voice SDK with the Inworld SDK.

## Prerequisites
- [Voice SDK v50+](https://developer.oculus.com/downloads/package/oculus-voice-sdk/) or Wit Unity v49+ (see [Wit Unity](https://github.com/wit-ai/wit-unity))
- [Inworld SDK Lite](https://github.com/yolanother/UnityInworldLight)
- [Inworld API](https://github.com/yolanother/inworldapi)

## Setup
1. Import the Voice SDK or Wit-Unity package into your Unity project. NOTE: Wit-Unity is bleedign edge. If v50+ Voice SDK is out you should use it instead as it is more stable.
2. Import the Inworld SDK Lite package into your Unity project.
3. Import the Voice SDK + Inworld SDK Integration package into your Unity project.
4. If you haven't already create a WitConfig
5. Add an AppVoiceExperience or Wit component to your scene and assign its wit config value.
6. Add an inworld character or interaction to your scene by right clicking in the hierarchy and selecting `Inworld Lite > Character` or `Inworld > Scene Interaction`.
7. Right click in the Hierarchy view and select `Voice SDK > Inworld > Character Speaker`
8. If you want Inworld to handle any unrecognized voice commands you will need to add a Character Out of Domain Handler. You can do this by right clicking in the Hierarchy view and selecting `Voice SDK > Inworld > Character Out of Domain Handler`
9. If you want specific intents to be routed to an Inworld character you will need to add a Character Intent Handler. You can do this by right clicking in the Hierarchy view and selecting `Voice SDK > Inworld > Character Intent Handler`