using System.Collections;
using UnityEngine;

namespace SimpleExtentions.Runtime.SimpleDoors
{
    public class SlidingVerticalDoor : TSimpleDoor
    {
        [SerializeField] private GameObject m_doorObject;
        [SerializeField] private float m_openPosition = 5f;
        [SerializeField] private float m_openSpeed = 2f;

        private Vector3 initialPosition;
        private Vector3 openPositionVector;

        private void Start()
        {
            initialPosition = m_doorObject.transform.localPosition;
            openPositionVector = initialPosition + Vector3.up * m_openPosition;
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
            while (Vector3.Distance(m_doorObject.transform.localPosition, openPositionVector) > 0.01f)
            {
                m_doorObject.transform.localPosition = Vector3.Lerp(m_doorObject.transform.localPosition, openPositionVector, m_openSpeed * Time.deltaTime);
                yield return null;
            }

            isOpening = false;
            InvokeOpenCompleteEvent();
        }

        private IEnumerator CloseCoroutine()
        {
            while (Vector3.Distance(m_doorObject.transform.localPosition, initialPosition) > 0.01f)
            {
                m_doorObject.transform.localPosition = Vector3.Lerp(m_doorObject.transform.localPosition, initialPosition, m_openSpeed * Time.deltaTime);
                yield return null;
            }

            isClosing = false;
            InvokeCloseCompleteEvent();
        }
    }
}