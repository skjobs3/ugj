namespace Fly
{
    namespace UI.World
    {
        public class ProgressBar : UnityEngine.MonoBehaviour
        {
            [UnityEngine.SerializeField]
            private UnityEngine.Transform _Current = null;

            [UnityEngine.SerializeField]
            private UnityEngine.Transform _Total = null;

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

                    UnityEngine.Vector3 Size = this._Current.localScale;
                    Size.y = this._Total.localScale.y * this._Value;

                    this._Current.transform.localScale = Size;

                }
            }

            public void Start()
            {
                this.Value = this._Value;
            }

            public void Update()
            {
                this.Value = this._Value;
            }
        }
    }
}