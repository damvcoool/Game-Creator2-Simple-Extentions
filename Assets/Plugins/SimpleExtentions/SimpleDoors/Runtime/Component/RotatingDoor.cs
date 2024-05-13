using System.Collections;
using UnityEngine;

namespace SimpleExtentions.Runtime.SimpleDoors
{
    public class RotatingDoor : TSimpleDoor
    {
        [SerializeField] private GameObject m_doorObject;
        [SerializeField] private float m_OpenAngle = 90f;
        [SerializeField] private float m_Speed = 2f;

        private Quaternion initialRotation;
        private Quaternion openRotation;

        private void Start()
        {
            initialRotation = m_doorObject.transform.localRotation;
            openRotation = Quaternion.Euler(0, m_OpenAngle, 0) * initialRotation;
        }

        public override void Open()
        {
            if (!isOpening && !isClosing)
            {
                isOpening = true;
                InvokeOpenStartEvent();
                StartCoroutine(OpenCoroutine());
            }
        }

        public override void Close()
        {
            if (!isOpening && !isClosing)
            {
                isClosing = true;
                InvokeCloseStartEvent();
                StartCoroutine(CloseCoroutine());
            }
        }

        private IEnumerator OpenCoroutine()
        {
            while (Quaternion.Angle(m_doorObject.transform.localRotation, openRotation) > 0.01f)
            {
                m_doorObject.transform.localRotation = Quaternion.Slerp(m_doorObject.transform.localRotation, openRotation, m_Speed * Time.deltaTime);
                yield return null;
            }

            isOpening = false;
            InvokeOpenCompleteEvent();
        }

        private IEnumerator CloseCoroutine()
        {
            while (Quaternion.Angle(m_doorObject.transform.localRotation, initialRotation) > 0.01f)
            {
                m_doorObject.transform.localRotation = Quaternion.Slerp(m_doorObject.transform.localRotation, initialRotation, m_Speed * Time.deltaTime);
                yield return null;  
            }

            isClosing = false;
            InvokeCloseCompleteEvent();
        }
    }
}