namespace Fly
{
    namespace UI.HUD
    {
        public class ProgressBar : UnityEngine.MonoBehaviour
        {
            [UnityEngine.SerializeField]
            private UnityEngine.RectTransform _Path = null;

            [UnityEngine.SerializeField]
            private UnityEngine.RectTransform _Ship = null;

            [UnityEngine.SerializeField]
            private UnityEngine.RectTransform _Enemy = null;

            [UnityEngine.SerializeField]
            private float _EnemyProgress = 0;

            public float EnemyProgress
            {
                get
                {
                    return this._EnemyProgress;
                }
                set
                {
                    this._EnemyProgress = UnityEngine.Mathf.Clamp01(value);

                    UnityEngine.Vector2 Position = this._Enemy.anchoredPosition;
                    Position.x = this._Path.rect.size.x * this._EnemyProgress;
                    this._Enemy.anchoredPosition = Position;
                }
            }

            [UnityEngine.SerializeField]
            private float _ShipProgress = 0;

            public float ShipProgress
            {
                get
                {
                    return this._ShipProgress;
                }
                set
                {
                    this._ShipProgress = UnityEngine.Mathf.Clamp01(value);

                    UnityEngine.Vector2 Position = this._Ship.anchoredPosition;
                    Position.x = this._Path.rect.size.x * this._ShipProgress;
                    this._Ship.anchoredPosition = Position;
                }
            }
        }
    }
}