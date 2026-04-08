using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleExtentions.Runtime.Minimap
{
    [AddComponentMenu("Game Creator/Simple Extensions/Minimap UI")]
    [RequireComponent(typeof(RawImage))]
    public class MinimapUI : MonoBehaviour
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private MinimapCamera m_MinimapCamera;

        /// <summary>
        /// Optional prefab used for each icon overlay. Must have an <see cref="Image"/> component
        /// and a <see cref="RectTransform"/>. If left empty, no icon overlays are shown.
        /// </summary>
        [SerializeField] private GameObject m_IconPrefab;

        // MEMBERS: -------------------------------------------------------------------------------

        [NonSerialized] private RawImage m_Display;
        [NonSerialized] private readonly Dictionary<MinimapIcon, RectTransform> m_IconInstances =
            new Dictionary<MinimapIcon, RectTransform>();
        [NonSerialized] private readonly List<MinimapIcon> m_ToRemove = new List<MinimapIcon>();

        // INITIALIZERS: --------------------------------------------------------------------------

        private void Awake()
        {
            m_Display = this.GetComponent<RawImage>();
        }

        private void OnEnable()
        {
            if (m_MinimapCamera != null && m_MinimapCamera.RenderTexture != null)
                m_Display.texture = m_MinimapCamera.RenderTexture;
        }

        private void OnDisable()
        {
            foreach (var kvp in m_IconInstances)
            {
                if (kvp.Value != null) kvp.Value.gameObject.SetActive(false);
            }
        }

        // UPDATE METHODS: ------------------------------------------------------------------------

        private void LateUpdate()
        {
            if (m_MinimapCamera == null) return;

            Camera cam = m_MinimapCamera.Camera;
            if (cam == null) return;

            // Ensure the RawImage texture is always assigned (e.g. after scene load)
            if (m_Display.texture == null && m_MinimapCamera.RenderTexture != null)
                m_Display.texture = m_MinimapCamera.RenderTexture;

            if (m_IconPrefab == null) return;

            RectTransform displayRect = (RectTransform)this.transform;
            Rect rect = displayRect.rect;

            // Remove entries for destroyed MinimapIcons
            m_ToRemove.Clear();
            foreach (var kvp in m_IconInstances)
            {
                if (kvp.Key == null)
                {
                    if (kvp.Value != null) Destroy(kvp.Value.gameObject);
                    m_ToRemove.Add(kvp.Key);
                }
            }
            foreach (MinimapIcon key in m_ToRemove) m_IconInstances.Remove(key);

            IReadOnlyList<MinimapIcon> activeIcons = MinimapIcon.Icons;

            // Update or create icon instances for every registered MinimapIcon
            foreach (MinimapIcon icon in activeIcons)
            {
                if (icon == null) continue;

                if (!m_IconInstances.TryGetValue(icon, out RectTransform iconRT))
                {
                    GameObject go = Instantiate(m_IconPrefab, this.transform);
                    iconRT = go.GetComponent<RectTransform>();
                    if (iconRT == null)
                    {
                        Destroy(go);
                        continue;
                    }
                    m_IconInstances[icon] = iconRT;
                }

                // Sync appearance
                if (iconRT.TryGetComponent<Image>(out Image img))
                {
                    img.sprite = icon.Icon;
                    img.color = icon.Color;
                }

                // Map the icon's world position to a viewport position via the minimap camera,
                // then convert that to an anchored position inside the RawImage rect.
                Vector3 viewportPos = cam.WorldToViewportPoint(icon.transform.position);
                bool inFront = viewportPos.z > 0f;

                iconRT.gameObject.SetActive(inFront);

                if (inFront)
                {
                    float x = (viewportPos.x - 0.5f) * rect.width;
                    float y = (viewportPos.y - 0.5f) * rect.height;
                    iconRT.anchoredPosition = new Vector2(x, y);

                    float pixelsPerUnit = m_MinimapCamera.Camera.orthographicSize > 0f
                        ? rect.width / (m_MinimapCamera.Camera.orthographicSize * 2f)
                        : 1f;
                    float size = icon.WorldSize * pixelsPerUnit;
                    iconRT.sizeDelta = new Vector2(size, size);
                }
            }

            // Hide instances whose MinimapIcon has been unregistered (disabled, not destroyed)
            foreach (var kvp in m_IconInstances)
            {
                if (kvp.Key != null && !ContainsIcon(activeIcons, kvp.Key))
                    kvp.Value.gameObject.SetActive(false);
            }
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private static bool ContainsIcon(IReadOnlyList<MinimapIcon> list, MinimapIcon icon)
        {
            for (int i = 0; i < list.Count; i++)
                if (list[i] == icon) return true;
            return false;
        }
    }
}
