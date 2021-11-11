

namespace UnityEngine.UI.Extensions
{
    public class LayoutScale : LayoutGroup
    {
        public enum Layout { Horizontal, Vertical }
        public enum Anchor { None, Top, Bot, Right, Left }

        public Layout m_layout;
        public Anchor m_anchor;

        public Vector2 m_sizeMember = new Vector2(100, 100);
        public bool m_OnlyLayoutVisible;
        public float m_spacing;

        protected override void OnEnable() { base.OnEnable(); Calculate(); }
        public override void SetLayoutHorizontal()
        {
        }
        public override void SetLayoutVertical()
        {
        }
        public override void CalculateLayoutInputVertical()
        {
            Calculate();
        }
        public override void CalculateLayoutInputHorizontal()
        {
            Calculate();
        }
#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            Calculate();
        }
#endif

        private void Calculate()
        {
            m_Tracker.Clear();
            if (transform.childCount == 0)
                return;

            RectTransform rectChild = (RectTransform)transform.GetChild(0);

            int ChildrenToFormat = 0;
            if (m_OnlyLayoutVisible)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    RectTransform child = (RectTransform)transform.GetChild(i);
                    if ((child != null) && child.gameObject.activeSelf)
                    {
                        ++ChildrenToFormat;
                    }
                }
            }
            else
            {
                ChildrenToFormat = transform.childCount;
            }

            float empty = 1;
            float scale = 1;

     
            Vector3 startPosition = Vector3.zero;

            if (m_layout == Layout.Horizontal)
            {
                empty = (rectTransform.rect.width - padding.left * 2 - m_spacing * (ChildrenToFormat - 1));
                scale = empty / (ChildrenToFormat * m_sizeMember.x);

                startPosition =  rectTransform.rect.width * Vector3.left / 2;
            }
            else if (m_layout == Layout.Vertical)
            {
                empty = (rectTransform.rect.height - padding.top * 2 - m_spacing * (ChildrenToFormat - 1));
                scale = empty / (ChildrenToFormat * m_sizeMember.y);
                startPosition = -rectTransform.rect.height * Vector3.up / 2;
            }

            Vector3 margin = GetPadding(m_layout);
            Vector3 size = GetSizeMember(m_layout, m_sizeMember, scale);
            Vector3 space = GetSpacing(m_layout, m_spacing);
            Vector3 anchor = GetAnchor(m_anchor, m_sizeMember, scale);

            for (int i = 0; i < transform.childCount; i++)
            {
                RectTransform child = (RectTransform)transform.GetChild(i);
                if ((child != null) && (!m_OnlyLayoutVisible || child.gameObject.activeSelf))
                {
                    //Adding the elements to the tracker stops the user from modifying their positions via the editor.
                    m_Tracker.Add(this, child,
                    DrivenTransformProperties.Anchors |
                    DrivenTransformProperties.AnchoredPosition |
                    DrivenTransformProperties.Pivot);

                    child.sizeDelta = m_sizeMember;

                    Vector3 vPos = margin + size * (i + 0.5f) + space * i + startPosition + anchor;

                    child.anchoredPosition = vPos;

                    child.localScale = scale * Vector3.one;

                    child.anchorMin = child.anchorMax = child.pivot = new Vector2(0.5f, 0.5f);

                }
            }
        }

        private Vector3 GetPadding(Layout layout)
        {
            Vector3 padding = Vector3.zero;

            if (layout == Layout.Horizontal)
            {
                padding = this.padding.left * Vector3.right;
            }
            else if (layout == Layout.Vertical)
            {
                padding = this.padding.top * Vector3.up;
            }

            return padding;
        }

        private Vector3 GetSpacing(Layout layout , float spacing)
        {
            Vector3 space = Vector3.zero;

            if (layout == Layout.Horizontal)
            {
                space = Vector3.right;
            }
            else if (layout == Layout.Vertical)
            {
                space = Vector3.up;
            }

            return space * spacing;
        }

        private Vector3 GetSizeMember(Layout layout, Vector2 sizeMember, float scale)
        {
            Vector3 size = Vector3.zero;

            if (layout == Layout.Horizontal)
            {
                size = sizeMember.x * Vector3.right * scale;
            }
            else if (layout == Layout.Vertical)
            {
                size = sizeMember.y * Vector3.up * scale;
            }

            return size;
        }

        private Vector3 GetAnchor(Anchor anchor, Vector3 sizeMember, float scale)
        {
            Vector3 _anchor = Vector3.zero;

            switch (anchor)
            {
                case Anchor.None:
                    break;
                case Anchor.Top:

                    _anchor = (scale - 1) / 2 * sizeMember.y * Vector3.down;
                    break;
                case Anchor.Bot:

                    _anchor = (scale - 1) / 2 * sizeMember.y * Vector3.up;
                    break;
                case Anchor.Right:

                    _anchor = (scale - 1) / 2 * sizeMember.x * Vector3.left;
                    break;
                case Anchor.Left:

                    _anchor = (scale - 1) / 2 * sizeMember.x * Vector3.right;
                    break;
                default:
                    break;
            }

            return _anchor;
        }
    }
}