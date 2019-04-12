namespace Fly.Ship.Modules.Projectile
{
    public class Projectile : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField]
        private float _Speed = 100f;

        [UnityEngine.SerializeField]
        private int _Damage = 100;

        private void Start()
        {
            UnityEngine.Rigidbody2D RigidBody = this.GetComponent<UnityEngine.Rigidbody2D>();
            if (!RigidBody)
            {
                return;
            }

            RigidBody.velocity = this.transform.right * this._Speed;
        }

        private void OnBecameInvisible()
        {
            UnityEngine.GameObject.Destroy(this.gameObject);
        }
    }
}
