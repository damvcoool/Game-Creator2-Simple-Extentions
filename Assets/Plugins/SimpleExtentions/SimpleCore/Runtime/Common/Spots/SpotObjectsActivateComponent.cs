using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;

namespace SimpleExtentions.Runtime.Common
{
    [Title("Activate Component")]
    [Image(typeof(IconComponent), ColorTheme.Type.Yellow)]

    [Category("Game Objects/Activate Component")]
    [Description(
        "Activates a game object's Component when the Hotspot is enabled and " +
        "deactivates it when the Hotspot is disabled"
    )]

    [Serializable]
    public class SpotComponentActivate : Spot
    {
        [SerializeField] private PropertyGetGameObject m_GameObject = GetGameObjectInstance.Create();
        [SerializeField] private TypeReferenceBehaviour m_Type = new TypeReferenceBehaviour();
        public override string Title => $"Activate {this.m_Type} from {this.m_GameObject}";
        public override void OnUpdate(Hotspot hotspot)
        {
            base.OnUpdate(hotspot);

            GameObject gameObject = this.m_GameObject.Get(hotspot.Args);
            if (gameObject == null) return;

            Behaviour behaviour = gameObject.Get(this.m_Type.Type) as Behaviour;
            if (behaviour != null)
            {
                bool isActive = this.EnableInstance(hotspot);
                behaviour.enabled = isActive;
                
            }
        }

        public override void OnDisable(Hotspot hotspot)
        {
            base.OnDisable(hotspot);

            GameObject gameObject = this.m_GameObject.Get(hotspot.Args);
            if (gameObject == null) return;

            Behaviour behaviour = gameObject.Get(this.m_Type.Type) as Behaviour;
            if (behaviour != null)
            {
                bool isActive = this.EnableInstance(hotspot);
                behaviour.enabled = false;
            }
        }

        // VIRTUAL METHODS: -----------------------------------------------------------------------

        protected virtual bool EnableInstance(Hotspot hotspot)
        {
            return hotspot.IsActive;
        }
    }
}