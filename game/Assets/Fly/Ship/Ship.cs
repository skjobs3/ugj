namespace Fly.Ship
{
    public class Ship : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField]
        private int _DurabilityCurrent = 10000;

        [UnityEngine.SerializeField]
        private int _DurabilityMax = 10000;

        //[UnityEngine.SerializeField]
        //private float _SpeedBoost = 1.0f;

        [UnityEngine.SerializeField]
        private Fly.UI.HUD.HealthBar _HealthBar = null;

        private void Update()
        {
            if (this._HealthBar)
            {
                if (this._DurabilityMax == 0f)
                {
                    UnityEngine.GameObject.Destroy(this.gameObject);
                    return;
                }

                this._HealthBar.Value = (float)this._DurabilityCurrent / (float)this._DurabilityMax;
            }
        }

        public void Damage(int Value)
        {
            this._DurabilityCurrent -= Value;
            this._DurabilityCurrent = UnityEngine.Mathf.Clamp(this._DurabilityCurrent, 0, this._DurabilityMax);
        }
    }
}
