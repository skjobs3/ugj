namespace Fly.Space.Enemy
{
    public class Warp : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField]
        private float _Speed = 10f;

        void Start()
        {
            UnityEngine.Rigidbody2D RigidBody = this.GetComponent<UnityEngine.Rigidbody2D>();

            if (RigidBody)
            {
                RigidBody.velocity = UnityEngine.Vector2.right * this._Speed;
            }
        }

        void OnCollisionEnter2D(UnityEngine.Collision2D Collision)
        {
            UnityEngine.GameObject GameObject = Collision.gameObject;

            while (GameObject.transform.parent != null)
            {
                GameObject = GameObject.transform.parent.gameObject;
            }

            //

            Fly.Ship.Instances.Ship Ship = GameObject.GetComponent<Fly.Ship.Instances.Ship>();

            if (Ship == null)
            {
                return;
            }

            Ship.Damage(Ship.Durability);
        }
    }
}

