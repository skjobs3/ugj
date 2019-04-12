namespace Fly
{
    namespace UI
    {
        public class HealthBar : UnityEngine.MonoBehaviour
        {
            [UnityEngine.SerializeField]
            private UnityEngine.RectTransform _Current = null;

            [UnityEngine.SerializeField]
            private UnityEngine.RectTransform _Total = null;

            [UnityEngine.SerializeField]
            private float _Value;

            public float Value
            {
                get
                {
                    return this._Value;
                }
                set
                {
                    this._Value = UnityEngine.Mathf.Clamp01(value);

                    UnityEngine.Vector2 Size = this._Current.sizeDelta;
                    Size.x = this._Total.rect.width * this._Value;

                    this._Current.sizeDelta = Size;

                }
            }

            public void Start()
            {
                this.Value = this._Value;
            }
        }
    }
}