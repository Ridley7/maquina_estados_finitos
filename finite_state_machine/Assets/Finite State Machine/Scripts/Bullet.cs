using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 60f;
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private int damage = 2;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<ITank>() != null)
        {
            other.GetComponent<ITank>().ReceiveDamage(damage);
        }

        Destroy(gameObject);
    }
}
