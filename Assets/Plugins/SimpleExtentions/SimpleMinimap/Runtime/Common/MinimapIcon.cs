using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleExtentions.Runtime.Minimap
{
    [AddComponentMenu("Game Creator/Simple Extensions/Minimap Icon")]
    public class MinimapIcon : MonoBehaviour
    {
        // STATIC REGISTRY: -----------------------------------------------------------------------

        private static readonly List<MinimapIcon> s_Icons = new List<MinimapIcon>();

        /// <summary>All currently active <see cref="MinimapIcon"/> instances in the scene.</summary>
        public static IReadOnlyList<MinimapIcon> Icons => s_Icons;

        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private Sprite m_Icon;
        [SerializeField] private Color m_Color = Color.white;

        /// <summary>World-space diameter of the icon quad displayed on the minimap (in Unity units).</summary>
        [SerializeField] private float m_WorldSize = 1f;

        // PROPERTIES: ----------------------------------------------------------------------------

        public Sprite Icon => m_Icon;
        public Color Color => m_Color;
        public float WorldSize => m_WorldSize;

        // INITIALIZERS: --------------------------------------------------------------------------

        private void OnEnable() => s_Icons.Add(this);

        private void OnDisable() => s_Icons.Remove(this);
    }
}
