namespace Fly
{
    public class Manager : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField]
        protected Fly.UI.HUD.ProgressBar _ProgressBar;

        [UnityEngine.SerializeField]
        protected Fly.UI.HUD.HealthBar _HealthBar;

        [UnityEngine.SerializeField]
        protected Fly.Ship.Instances.Ship _Ship;

        protected void Update()
        {
            //this._Ship
        }
    }
}