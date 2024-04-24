using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using SimpleExtentions.Runtime.Common;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace SimpleExtentions.Runtime.Pause
{
    [AddComponentMenu("Game Creator/UI/Pause UI")]
    [Icon(Paths.PATH + "PauseUI/Editor/GizmoPause.png")]
    public class PauseUI : MonoBehaviour
    {
#if UNITY_EDITOR

        [UnityEditor.InitializeOnEnterPlayMode]
        private static void OnEnterPlayMode()
        {
            IsOpen = false;
            EventOpen = null;
            EventClose = null;
        }
#endif
        // EXPOSED MEMBERS: -----------------------------------------------------------------------
        [SerializeField] private PropertyGetDecimal m_PauseTime = GetDecimalDecimal.Create(0f);
        [SerializeField] private PropertyGetDecimal m_ResumeTime = GetDecimalDecimal.Create(1f);
        [SerializeField] private PropertyGetDecimal m_TransitionDuration = GetDecimalDecimal.Create(0f);
        [SerializeField] private PropertyGetInteger m_Layer = GetDecimalInteger.Create(0);

        // MEMBERS: -----------------------------------------------------------------------
        private Args m_Args;
        // PROPERTIES: ----------------------------------------------------------------------------
        [field: NonSerialized] public static bool IsOpen { get; private set; }
        [field: NonSerialized] public GameObject PausePrefab { get; private set; }
        [field: NonSerialized] public float TimeScale { get; private set; }
        [field: NonSerialized] public float Transition { get; private set; }
        [field: NonSerialized] public int Layer { get; private set; }

        // EVENTS. --------------------------------------------------------------------------------

        public static event Action EventOpen;
        public static event Action EventClose;
        // INITIALIZERS: --------------------------------------------------------------------------

        private void OnEnable()
        {
            float time = (float)m_PauseTime.Get(m_Args);
            float transition = (float)m_TransitionDuration.Get(m_Args);
            int layer = this.Layer = (int)m_Layer.Get(m_Args);
            IsOpen = true;
            this.OnPause(time, transition, layer);
            EventOpen?.Invoke();
        }

        private void OnDisable()
        {
            if (m_Args == null) return;
            float time = (float)m_ResumeTime.Get(m_Args);
            float transition = (float)m_TransitionDuration.Get(m_Args);
            int layer = this.Layer = (int)m_Layer.Get(m_Args);
            IsOpen = false;
            this.OnResume(time, transition, layer);
            EventClose?.Invoke();
        }


        // PUBLIC METHODS: ------------------------------------------------------------------------
        public void OpenUI(GameObject pause)
        {
            //if (pause == null) return;

            this.PausePrefab = pause;
            if (!FindObjectOfType<PauseUI>(true))
            {
                Instantiate(PausePrefab, new Vector3(Screen.width, Screen.height, 0), Quaternion.identity);
            }
            else
            {
                FindObjectOfType<PauseUI>(true).gameObject.SetActive(true);
            }

            return;
        }
        public void CloseUI()
        {
            if (FindObjectOfType<PauseUI>())
            {
                GameObject.FindObjectOfType<PauseUI>().gameObject.SetActive(false);
            }
            return;
        }
        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void OnPause(float timescale, float transition, int layer)
        {
            TimeManager.Instance.SetSmoothTimeScale(timescale, transition, layer);
        }

        private void OnResume(float timescale, float transition, int layer)
        {
            TimeManager.Instance.SetSmoothTimeScale(timescale, transition, layer);
        }
    }
}