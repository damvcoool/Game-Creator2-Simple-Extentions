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

        // MEMBERS: -------------------------------------------------------------------------------
        [NonSerialized] private Args m_Args;
        // PROPERTIES: ----------------------------------------------------------------------------
        [field: NonSerialized] public static bool IsOpen { get; private set; }
        [field: NonSerialized] public GameObject PausePrefab { get; private set; }
        [field: NonSerialized] public float TimeScale { get; private set; }
        [field: NonSerialized] public float Transition { get; private set; }
        [field: NonSerialized] public int Layer { get; private set; }

        // EVENTS. --------------------------------------------------------------------------------

        public static event Action<GameObject> EventOpen;
        public static event Action<GameObject> EventClose;
        // INITIALIZERS: --------------------------------------------------------------------------

        private void Awake()
        {
            m_Args = new Args(this.gameObject);
        }

        private void OnEnable()
        {
            float time = (float)m_PauseTime.Get(m_Args);
            float transition = (float)m_TransitionDuration.Get(m_Args);
            int layer = this.Layer = (int)m_Layer.Get(m_Args);
            IsOpen = true;
            this.OnPause(time, transition, layer);
            EventOpen?.Invoke(this.gameObject);
        }

        private void OnDisable()
        {
            float time = (float)m_ResumeTime.Get(m_Args);
            float transition = (float)m_TransitionDuration.Get(m_Args);
            int layer = this.Layer = (int)m_Layer.Get(m_Args);
            IsOpen = false;
            this.OnResume(time, transition, layer);
            EventClose?.Invoke(this.gameObject);
        }


        // PUBLIC METHODS: ------------------------------------------------------------------------
        public void OpenUI(GameObject pause)
        {
            if (pause == null) return;

            this.PausePrefab = pause;

            PauseUI obj = null;

            PauseUI[] objList = FindObjectsByType<PauseUI>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID);

            foreach (PauseUI pauseUI in objList)
            {
                if (pauseUI.PausePrefab != null && pause.name == pauseUI.PausePrefab.name)
                {
                    obj = pauseUI;
                }
            }

            if (obj == null)
            {
                GameObject go = Instantiate(PausePrefab, Vector3.zero, Quaternion.identity);
                go.GetComponent<PauseUI>().PausePrefab = pause;
            }
            else
            {
                obj.gameObject.SetActive(true);
            }
        }

        public void CloseUI(GameObject pause)
        {
            if (pause == null) return;
            PauseUI sourceUI = pause.GetComponent<PauseUI>();
            if (sourceUI == null || sourceUI.PausePrefab == null) return;
            string name = sourceUI.PausePrefab.name;
            PauseUI[] objList = FindObjectsByType<PauseUI>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID);

            foreach (PauseUI pauseUI in objList)
            {
                if (pauseUI.PausePrefab != null && name == pauseUI.PausePrefab.name)
                {
                    pauseUI.gameObject.SetActive(false);
                }
            }
        }
        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void OnPause(float timescale, float transition, int layer)
        {
            TimeManager.Instance.SetSmoothTimeScale(timescale, transition, layer);
        }

        private void OnResume(float timescale, float transition, int layer)
        {
            if (TimeManager.Instance != null)
            {
                TimeManager.Instance.SetSmoothTimeScale(timescale, transition, layer);
            }
        }
    }
}