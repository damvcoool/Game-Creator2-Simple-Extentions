using TMPro;
using UnityEngine;
using UnityEngine.UI;
using GameCreator.Runtime.Common;


namespace SimpleExtentions.Runtime.Common.UnityUI
{
    public class TextPropertyString : MonoBehaviour
    {
        private string m_TextUI;
        private Text m_Text;
        private TMP_Text m_TMP;
        private bool m_Legacy;
        [SerializeField] private PropertyGetString m_PropertyString = new PropertyGetString();

        // MEMBERS: -------------------------------------------------------------------------------

        private Args m_Args;

        // INIT METHODS: --------------------------------------------------------------------------
        private void Awake()
        {
            m_TextUI = this.m_PropertyString.Get(m_Args);
            if (gameObject.GetComponent<Text>())
            {
                m_Text = gameObject.GetComponent<Text>();
                m_Text.text = m_TextUI;
                m_Legacy = true;
            }
            if (gameObject.GetComponent<TMP_Text>())
            {
                m_TMP = gameObject.GetComponent<TMP_Text>();
                m_TMP.text = m_TextUI;
                m_Legacy = false;
            }
        }
        private void LateUpdate()
        {
            if (!Application.isPlaying) return;

            if (this.m_TextUI != m_PropertyString.Get(m_Args))
            {
                this.SetValueFromProperty();
            }
        }

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public static TextPropertyString CreateFromTMP(GameObject gameObject)
        {
            TextPropertyString textPropertyString = gameObject.gameObject.AddComponent<TextPropertyString>();
            TMP_Text text = gameObject.gameObject.AddComponent<TextMeshProUGUI>();
            textPropertyString.m_PropertyString = new PropertyGetString();

            return textPropertyString;
        }
        public static TextPropertyString CreateFromLegacy(GameObject gameObject)
        {
            TextPropertyString textPropertyString = gameObject.gameObject.AddComponent<TextPropertyString>();
            Text text = gameObject.gameObject.AddComponent<Text>();
            textPropertyString.m_PropertyString = new PropertyGetString();

            return textPropertyString;
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void SetValueFromProperty()
        {
            this.m_TextUI = this.m_PropertyString.Get(m_Args);
            if (m_Legacy) m_Text.text = m_TextUI;
            if (!m_Legacy) m_TMP.text = m_TextUI;

        }
    }
}