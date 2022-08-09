using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTankController : MonoBehaviour, ITank
{
    [SerializeField] private GameObject tankTurret;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPos;
    [SerializeField] private int health = 10;

    public int CurrentHealth { get; set; }

    private float currentSpeed;
    private float targetSpeed;
    private float rotationSpeed = 150f;

    private float maxForwardSpeed = 15f;
    private float maxBackwardSpeed = -10f;

    private Transform bulletSpawnPosition;
    private Transform torreta;
    private Camera camera;

    private float shootRate = 3f;
    private float elapdsedTime;

    // Start is called before the first frame update
    void Start()
    {
        torreta = tankTurret.transform;
        camera = Camera.main;
        bulletSpawnPosition = bulletSpawnPos;
        CurrentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        MoveTank();
        RotateTorret();
        FireBullet();
        
    }

    private void FireBullet()
    {
        elapdsedTime += Time.deltaTime;

        if(elapdsedTime >= shootRate)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(bulletPrefab, bulletSpawnPosition.position, bulletSpawnPosition.rotation);
                elapdsedTime = 0;
            }
        }
    }

    private void RotateTorret()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if(playerPlane.Raycast(ray, out float hit))
        {
            Vector3 hitPos = ray.GetPoint(hit);
            Quaternion newRotation = Quaternion.LookRotation(hitPos - transform.position);
            torreta.rotation = Quaternion.Slerp(torreta.rotation, newRotation, Time.deltaTime * 10f);
        }
    }

    private void MoveTank()
    {
        if (Input.GetKey(KeyCode.W))
        {
            targetSpeed = maxForwardSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            targetSpeed = maxBackwardSpeed;
        }
        else
        {
            targetSpeed = 0f;
        }   

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0f, -rotationSpeed * Time.deltaTime, 0.0f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0.0f);
        }

        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * 10f);

        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime); 
    }

    public void ReceiveDamage(int damage)
    {
        CurrentHealth -= damage;

        if(CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
