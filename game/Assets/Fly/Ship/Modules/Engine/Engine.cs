namespace Fly.Ship.Modules
{
    public class Engine : Fly.Ship.Modules.Module
    {
        [UnityEngine.SerializeField]
        private UnityEngine.ParticleSystem _Fire = null;

        [UnityEngine.SerializeField]
        private float _Power = 0f;

        public float Power
        {
            get
            {
                return this._Power;
            }
            set
            {
                this._Power = UnityEngine.Mathf.Clamp01(value);

                if (this._Fire)
                {
                    const float BaseRate = 100f;

                    UnityEngine.ParticleSystem.MainModule MainParticle = this._Fire.main;
                    MainParticle.startLifetimeMultiplier = this._Power;

                    UnityEngine.ParticleSystem.EmissionModule Emission = this._Fire.emission;
                    Emission.rateOverTimeMultiplier = BaseRate * this._Power;
                }
            }
        }

        public void Start()
        {
            this.Power = this._Power;
        }
    }
}