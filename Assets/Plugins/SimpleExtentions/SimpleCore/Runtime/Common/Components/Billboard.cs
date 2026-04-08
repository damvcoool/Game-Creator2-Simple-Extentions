using UnityEngine;
namespace SimpleExtentions.Runtime.Common
{
    [AddComponentMenu("Game Creator/Simple Extensions/Billboard")]
    public class Billboard : MonoBehaviour
    {
        private Camera m_Camera;

        void Start()
        {
            m_Camera = Camera.main;
        }

        void Update()
        {
            if (m_Camera == null)
            {
                m_Camera = Camera.main;
                if (m_Camera == null) return;
            }
            transform.LookAt(m_Camera.transform);
            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + 180f, 0f);
        }
    }
}