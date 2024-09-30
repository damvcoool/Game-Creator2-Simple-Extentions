using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace SimpleExtentions.Runtime.Pause
{
    [Serializable]
    public class EventOnPauseUIOpen : GameCreator.Runtime.VisualScripting.Event
    {

        [SerializeField] private PropertyGetGameObject m_PauseUI;
        [NonSerialized] private PauseUI m_Cache;

        protected override void OnEnable(Trigger trigger)
        {
            base.OnEnable(trigger);

            this.m_Cache = this.m_PauseUI.Get<PauseUI>(this.Self);
            if (this.m_Cache == null) return;

            PauseUI.EventOpen -= this.OnActivate;
            PauseUI.EventOpen += this.OnActivate;
        }

        protected override void OnDisable(Trigger trigger)
        {
            base.OnDisable(trigger);

            if (this.m_Cache == null) return;
            PauseUI.EventOpen -= this.OnActivate;
        }

        private void OnActivate(GameObject gameObject)
        {
            PauseUI po = gameObject.GetComponent<PauseUI>();
            if (this.m_Cache == null || po.PausePrefab == null) return;
            if (m_Cache.PausePrefab.name == po.PausePrefab.name)
            {
                _ = this.m_Trigger.Execute(this.Self);
            }
        }
    }
}