using System;
using UnityEngine;
using GameCreator.Runtime.Common;

namespace SimpleExtentions.Runtime.Minimap
{
    [AddComponentMenu("Game Creator/Simple Extensions/Minimap Camera")]
    public class MinimapCamera : MonoBehaviour
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private PropertyGetGameObject m_Target = GetGameObjectPlayer.Create();
        [SerializeField] private float m_Height = 20f;
        [SerializeField] private float m_OrthographicSize = 10f;
        [SerializeField] private Vector2Int m_TextureSize = new Vector2Int(256, 256);

        // MEMBERS: -------------------------------------------------------------------------------

        [NonSerialized] private Camera m_Camera;
        [NonSerialized] private RenderTexture m_RenderTexture;
        [NonSerialized] private Args m_Args;

        // PROPERTIES: ----------------------------------------------------------------------------

        /// <summary>The RenderTexture this minimap camera renders into.</summary>
        public RenderTexture RenderTexture => m_RenderTexture;

        /// <summary>The managed overhead Camera component.</summary>
        public Camera Camera => m_Camera;

        // INITIALIZERS: --------------------------------------------------------------------------

        private void Awake()
        {
            m_Args = new Args(this.gameObject);

            m_RenderTexture = new RenderTexture(m_TextureSize.x, m_TextureSize.y, 16)
            {
                name = "MinimapRenderTexture"
            };

            m_Camera = this.GetComponent<Camera>();
            if (m_Camera == null) m_Camera = this.gameObject.AddComponent<Camera>();

            m_Camera.orthographic = true;
            m_Camera.orthographicSize = m_OrthographicSize;
            m_Camera.clearFlags = CameraClearFlags.SolidColor;
            m_Camera.backgroundColor = Color.black;
            m_Camera.targetTexture = m_RenderTexture;
            m_Camera.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        }

        private void OnDestroy()
        {
            if (m_RenderTexture == null) return;
            m_RenderTexture.Release();
            Destroy(m_RenderTexture);
        }

        // UPDATE METHODS: ------------------------------------------------------------------------

        private void LateUpdate()
        {
            GameObject target = m_Target.Get(m_Args);
            if (target == null) return;

            Vector3 position = target.transform.position;
            position.y += m_Height;
            this.transform.position = position;
        }
    }
}
