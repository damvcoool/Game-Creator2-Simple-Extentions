using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Characters.IK;
using UnityEngine;
using GameCreator.Runtime.VisualScripting;

namespace DM_Customization.Runtime.Common
{
    [Title("Mutual Look At")]
    [Image(typeof(IconEye), ColorTheme.Type.Purple)]
    
    [Category("Mutual Look At")]
    [Description(
        "Makes the Target and Looker look at each other's Head")]

    [Serializable]
    public class SpotLookMutual : Spot
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private Character m_Looker;
        private Bone m_TargetBone = new Bone(HumanBodyBones.Head);
        [SerializeField] protected int m_Priority;

        // MEMBERS: -------------------------------------------------------------------------------

        private bool m_WasActive;
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public override string Title => $"Target and {m_Looker} look at each other's Heads.";

        // OVERRIDE METHODS: ----------------------------------------------------------------------

        public override void OnUpdate(Hotspot hotspot)
        {
            base.OnUpdate(hotspot);
            bool isActive = this.EnableInstance(hotspot);
            
            if (!this.m_WasActive && isActive)
            {
                RigLookTrack lookTrack = this.GetCharacterLook(hotspot);
                RigLookTrack lookerLookTrack = this.m_Looker.IK.GetRig<RigLookTrack>();

                lookerLookTrack?.SetTarget(new LookTrackTransform(this.m_Priority, m_TargetBone.GetTransform(hotspot.Target.Get<Character>().Animim.Animator), Vector3.zero));
                lookTrack?.SetTarget(new LookTrackTransform(this.m_Priority,m_TargetBone.GetTransform(m_Looker.Animim.Animator),Vector3.zero));
            }

            if (this.m_WasActive && !isActive)
            {
                RigLookTrack lookTrack = this.GetCharacterLook(hotspot);
                RigLookTrack lookerLookTrack = this.m_Looker.IK.GetRig<RigLookTrack>();

                lookerLookTrack?.RemoveTarget(new LookTrackTransform(this.m_Priority, m_TargetBone.GetTransform(hotspot.Target.Get<Character>().Animim.Animator), Vector3.zero));
                lookTrack?.RemoveTarget(new LookTrackTransform(this.m_Priority, m_TargetBone.GetTransform(m_Looker.Animim.Animator), Vector3.zero));
            }

            this.m_WasActive = isActive;
        }

        public override void OnDisable(Hotspot hotspot)
        {
            base.OnDisable(hotspot);
            this.m_WasActive = false;
            
            RigLookTrack lookTrack = this.GetCharacterLook(hotspot);
            RigLookTrack lookerLookTrack = this.m_Looker.IK.GetRig<RigLookTrack>();

            lookerLookTrack?.RemoveTarget(new LookTrackTransform(this.m_Priority, m_TargetBone?.GetTransform(hotspot.Target.Get<Character>().Animim.Animator), Vector3.zero));
            lookTrack?.RemoveTarget(new LookTrackTransform(this.m_Priority, m_TargetBone?.GetTransform(m_Looker.Animim.Animator), Vector3.zero));
        }

        public override void OnDestroy(Hotspot hotspot)
        {
            base.OnDestroy(hotspot);
            this.m_WasActive = false;
            
            RigLookTrack lookTrack = this.GetCharacterLook(hotspot);
            RigLookTrack lookerLookTrack = this.m_Looker.IK.GetRig<RigLookTrack>();

            lookerLookTrack?.RemoveTarget(new LookTrackTransform(this.m_Priority, m_TargetBone?.GetTransform(hotspot.Target.Get<Character>().Animim.Animator), Vector3.zero));
            lookTrack?.RemoveTarget(new LookTrackTransform(this.m_Priority, m_TargetBone?.GetTransform(m_Looker.Animim.Animator), Vector3.zero));
        }
        
        // VIRTUAL METHODS: -----------------------------------------------------------------------

        protected virtual bool EnableInstance(Hotspot hotspot)
        {
            return hotspot.IsActive;
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private RigLookTrack GetCharacterLook(Hotspot hotspot)
        {
            if (hotspot.Target == null) return null;
            
            Character character = hotspot.Target.Get<Character>();
            return character != null ? character.IK.GetRig<RigLookTrack>() : null;
        }
    }
}
