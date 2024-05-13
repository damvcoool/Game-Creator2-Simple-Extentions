using System.Collections;
using UnityEngine;

namespace SimpleExtentions.Runtime.SimpleDoors
{
    public class SlidingHorizontalDoor : TSimpleDoor
    {
        private enum Direction { Left, Right}
        [SerializeField] private GameObject m_doorObject;
        [SerializeField] private Direction m_direction = Direction.Left;
        [SerializeField] private float m_openPosition = 5f;
        [SerializeField] private float m_openSpeed = 2f;

        private Vector3 initialPosition;
        private Vector3 openPositionVector;

        private void Start()
        {
            initialPosition = m_doorObject.transform.localPosition;
            if(m_direction == Direction.Left )
            {
                openPositionVector = initialPosition + Vector3.left * m_openPosition;
            }
            else if (m_direction == Direction.Right)
            {
                openPositionVector = initialPosition + Vector3.right * m_openPosition;
            }
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
