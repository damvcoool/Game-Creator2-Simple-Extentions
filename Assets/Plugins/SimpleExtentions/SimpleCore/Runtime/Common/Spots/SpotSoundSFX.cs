using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Common.Audio;
using GameCreator.Runtime.VisualScripting;

namespace SimpleExtentions.Runtime.Common
{
    [Title("Play Sound SFX")]
    [Keywords("Audio", "Sounds")]
    [Image(typeof(IconMusicNote), ColorTheme.Type.Yellow)]
    
    [Category("Audio/Play Sound SFX")]
    [Description(
        "Plays a Sound FX sound effect when the Hotspot is activated or deactivated"
    )]

    [Serializable]
    public class SpotSoundSFX : Spot
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] protected PropertyGetAudio m_OnActivate = new PropertyGetAudio();
        [SerializeField] protected PropertyGetAudio m_OnDeactivate = new PropertyGetAudio();

        [SerializeField]
        private AudioConfigSoundEffect m_AudioSettings = new AudioConfigSoundEffect();

        // MEMBERS: -------------------------------------------------------------------------------

        [NonSerialized] private bool m_WasActive;
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public override string Title => $"SFX Play {this.m_OnActivate} / {this.m_OnDeactivate}";

        // OVERRIDE METHODS: ----------------------------------------------------------------------

        public override void OnEnable(Hotspot hotspot)
        {
            base.OnEnable(hotspot);
            this.m_WasActive = false;
        }

        public override void OnDisable(Hotspot hotspot)
        {
            base.OnDisable(hotspot);
            if (ApplicationManager.IsExiting) return;
            
            if (this.m_WasActive)
            {
                Args args = new Args(hotspot.gameObject, hotspot.Target);
                AudioClip audioClip = this.m_OnDeactivate.Get(args);
                
                if (audioClip != null)
                {
                    _ = AudioManager.Instance.SoundEffect.Play(
                        audioClip,
                        this.m_AudioSettings,
                        args
                    );
                }
            }
        }

        public override void OnUpdate(Hotspot hotspot)
        {
            base.OnUpdate(hotspot);

            switch (this.m_WasActive)
            {
                case false when hotspot.IsActive:
                {
                    Args args = new Args(hotspot.gameObject, hotspot.Target);
                    AudioClip audioClip = this.m_OnActivate.Get(args);
                    if (audioClip != null)
                    {
                        _ = AudioManager.Instance.SoundEffect.Play(
                            audioClip,
                            this.m_AudioSettings,
                            args
                        );
                    }

                    break;
                }
                case true when !hotspot.IsActive:
                {
                    Args args = new Args(hotspot.gameObject, hotspot.Target);
                    AudioClip audioClip = this.m_OnDeactivate.Get(args);
                    if (audioClip != null)
                    {
                        _ = AudioManager.Instance.SoundEffect.Play(
                            audioClip,
                            this.m_AudioSettings,
                            args
                        );
                    }

                    break;
                }
            }


            this.m_WasActive = hotspot.IsActive;
        }
    }
}