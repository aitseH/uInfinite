using UnityEngine;
using UnityEngine.Networking;

public class Projectile : NetworkBehaviour {
    [SerializeField] float speed = 1;

    [HideInInspector] public Entity target;
    [HideInInspector] public Entity caster;
    [HideInInspector] public int damage;
    [HideInInspector] public float aoeRadius;

    [ServerCallback]
    void Start() { FixedUpdate();}

    void FixedUpdate() {

        if(target != null && caster != null) {
            var goal = target.GetComponentInChildren<Collider>().bounds.center;
            transform.position = Vector3.MoveTowards(transform.position, goal, speed);
            transform.LookAt(goal);

            if(transform.position == goal) {
                if (target.hp > 0)
                    caster.DealDamageAt(target, damage, aoeRadius);

                NetworkServer.Destroy(gameObject);
            } else {
                NetworkServer.Destroy(gameObject);
            }
        }
    }
}
