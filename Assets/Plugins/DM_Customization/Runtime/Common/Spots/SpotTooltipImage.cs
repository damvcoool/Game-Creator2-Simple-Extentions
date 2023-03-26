using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using UnityEngine.UI;
using GameCreator.Runtime.VisualScripting;

namespace DM_Customization.Runtime.Common
{
    [Title("Show Tooltip Image")]
    [Image(typeof(IconString), ColorTheme.Type.Blue)]
    
    [Category("UI/Show Tooltip Image")]
    [Description(
        "Displays a text in a world-space canvas when the Hotspot is enabled and hides it " +
        "when is disabled. If no Prefab is provided, a default UI is displayed"
    )]

    [Serializable]
    public class SpotTooltipImage : Spot
    {
        private const float CANVAS_WIDTH = 600f;
        private const float CANVAS_HEIGHT = 300f;

        private const float SIZE_X = 2f;
        private const float SIZE_Y = 1f;

        private const int PADDING = 50;

        private static readonly Color COLOR_BACKGROUND = new Color(0f, 0f, 0f, 0.5f);

        // EXPOSED MEMBERS: -----------------------------------------------------------------------
        
        [SerializeField] protected PropertyGetSprite m_Image = GetSpriteInstance.Create();
        [SerializeField] protected Vector3 m_Offset = Vector3.zero;
        [SerializeField] protected Space m_Space = Space.Self;
        
        [Space] [SerializeField] protected GameObject m_Prefab;

        // MEMBERS: -------------------------------------------------------------------------------
        
        [NonSerialized] private GameObject m_Tooltip;
        [NonSerialized] private ImageToolTip m_TooltipImage;
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public override string Title => $"Show " + this.m_Image;

        // OVERRIDE METHODS: ----------------------------------------------------------------------

        public override void OnUpdate(Hotspot hotspot)
        {
            base.OnUpdate(hotspot);

            GameObject instance = this.RequireInstance(hotspot);
            if (instance == null) return;

            Vector3 offset = this.m_Space switch
            {
                Space.World => this.m_Offset,
                Space.Self => hotspot.transform.TransformDirection(this.m_Offset),
                _ => throw new ArgumentOutOfRangeException()
            };
            
            instance.transform.SetPositionAndRotation(
                hotspot.Position + offset,
                ShortcutMainCamera.Transform.rotation
            );

            bool isActive = this.EnableInstance(hotspot);
            instance.SetActive(isActive);
        }

        public override void OnDisable(Hotspot hotspot)
        {
            base.OnDisable(hotspot);
            if (this.m_Tooltip != null) this.m_Tooltip.SetActive(false);
        }

        public override void OnDestroy(Hotspot hotspot)
        {
            base.OnDestroy(hotspot);
            if (this.m_Tooltip != null)
            {
                UnityEngine.Object.Destroy(this.m_Tooltip);
            }
        }
        
        // VIRTUAL METHODS: -----------------------------------------------------------------------

        protected virtual bool EnableInstance(Hotspot hotspot)
        {
            return hotspot.IsActive;
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private GameObject RequireInstance(Hotspot hotspot)
        {
            if (this.m_Tooltip == null)
            {
                if (this.m_Prefab != null)
                {
                    this.m_Tooltip = UnityEngine.Object.Instantiate(
                        this.m_Prefab,
                        hotspot.Position + hotspot.transform.TransformDirection(this.m_Offset),
                        hotspot.Rotation
                    );

                    this.m_TooltipImage = this.m_Tooltip.GetComponentInChildren<ImageToolTip>();
                }
                else
                {
                    this.m_Tooltip = new GameObject("Tooltip");

                    this.m_Tooltip.transform.SetPositionAndRotation(
                        hotspot.Position + hotspot.transform.TransformDirection(this.m_Offset),
                        ShortcutMainCamera.Transform.rotation
                    );
                    
                    Canvas canvas = this.m_Tooltip.AddComponent<Canvas>();
                    this.m_Tooltip.AddComponent<CanvasScaler>();
                    
                    canvas.renderMode = RenderMode.WorldSpace;
                    canvas.worldCamera = ShortcutMainCamera.Get<Camera>();

                    RectTransform canvasTransform = this.m_Tooltip.Get<RectTransform>();
                    canvasTransform.sizeDelta = new Vector2(CANVAS_WIDTH, CANVAS_HEIGHT);
                    canvasTransform.localScale = new Vector3(
                        SIZE_X / CANVAS_WIDTH,
                        SIZE_Y / CANVAS_HEIGHT,
                        1f
                    );

                    RectTransform background = this.ConfigureBackground(canvasTransform);
                    this.ConfigureImage(background);
                }

                this.m_Tooltip.hideFlags = HideFlags.HideAndDontSave;
                
                Args args = new Args(hotspot.gameObject, hotspot.Target);
                this.m_TooltipImage.sprite = this.m_Image.Get(args);
            }

            return this.m_Tooltip;
        }

        private RectTransform ConfigureBackground(RectTransform parent)
        {
            GameObject gameObject = new GameObject("Background");
            
            Image image = gameObject.AddComponent<Image>();
            image.color = COLOR_BACKGROUND;

            VerticalLayoutGroup layoutGroup = gameObject.AddComponent<VerticalLayoutGroup>();
            layoutGroup.padding = new RectOffset(PADDING, PADDING, PADDING, PADDING);
            layoutGroup.childAlignment = TextAnchor.MiddleCenter;
            layoutGroup.childControlWidth = true;
            layoutGroup.childControlHeight = true;
            layoutGroup.childScaleWidth = true;
            layoutGroup.childScaleHeight = true;
            layoutGroup.childForceExpandWidth = true;
            layoutGroup.childForceExpandHeight = true;

            ContentSizeFitter sizeFitter = gameObject.AddComponent<ContentSizeFitter>();
            sizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            sizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            RectTransformUtils.SetAndCenterToParent(rectTransform, parent);

            return rectTransform;
        }
        
        private GameObject ConfigureImage(RectTransform parent)
        {
            GameObject gameObject = new GameObject("Image");
            this.m_TooltipImage = gameObject.AddComponent<ImageToolTip>();

            RectTransform imageTransform = gameObject.GetComponent<RectTransform>();
            RectTransformUtils.SetAndCenterToParent(imageTransform, parent);
            
            return gameObject;
        }
    }
}