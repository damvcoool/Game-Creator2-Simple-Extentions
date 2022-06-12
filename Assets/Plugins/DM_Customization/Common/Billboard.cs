using UnityEngine;
namespace DM_Game.Runtime
{
    public class Billboard : MonoBehaviour
    {
        private Camera m_Camera;

        void Start()
        {
            m_Camera = Camera.main;
        }

        void Update()
        {
            transform.LookAt(m_Camera.transform);
            transform.rotation = Quaternion.Euler(0f,transform.rotation.eulerAngles.y + 180f,0f);
        }
    }
}