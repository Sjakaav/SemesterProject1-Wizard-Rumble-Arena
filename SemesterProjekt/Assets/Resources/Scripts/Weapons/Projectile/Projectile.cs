using System;
using Player;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;


// Add a thrust force to push an object in its current forward
public class Projectile : MonoBehaviour
{   
    [Header("Projectile stats")]
    public float thrust = 0.1f;
    public float rotation;

    [Header("References")] 
    private Rigidbody2D _rb;
    private Renderer _mRenderer;

    private bool _seen;
    private double _projectileDamage = 0;
    private GameObject _parent;

    public GameObject Parent
    {
        get => _parent;
        set
        {
            if (_parent != value)
            {
                _parent = value;
            }
        }
    }
    

    void Start()
    {
        
        _rb = GetComponent<Rigidbody2D>();
        _mRenderer = GetComponent<SpriteRenderer>();
        _rb.velocity = transform.right * thrust;
    }

    void Update()
    {
        transform.Rotate(0, 0, rotation * Time.deltaTime);
    }

    private void LateUpdate()
    {
        if (_mRenderer.isVisible)
        {
            _seen = true;
        }

        if (_seen && !_mRenderer.isVisible)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerController player = hitInfo.gameObject.GetComponent<PlayerController>();
        var rb = hitInfo.GetComponent<Rigidbody2D>();
        if (player != null && _projectileDamage > 0 && hitInfo.gameObject != Parent)
        {
            player.TakeDamage(_projectileDamage);
        }

        Destroy(gameObject);
    }

    public double GetDamage(double x)
    {
        switch (gameObject.name)
        {
            case "Manabullet(Clone)":
                _projectileDamage = 10.0;
                break;
            case "Fireball(Clone)":
                _projectileDamage = 15.0;
                break;
            case "Iceball(Clone)":
                _projectileDamage = 25.0;
                break;
            case "Pistol(Clone)":
                _projectileDamage = 10.0;
                break;
            case "Rifle(Clone)":
                _projectileDamage = 7.0;
                break;
            case "HeavyPistol(Clone)":
                _projectileDamage = 20.0;
                break;
        }
        

        _projectileDamage =  CalculateDamage(x, _projectileDamage);

        return _projectileDamage;

    }

    private double CalculateDamage(double amountOfShotsX, double projectileDamageA)
    {
        const double b = 0.9;
        var gOfx = Math.Truncate((Math.Pow(b, amountOfShotsX) * projectileDamageA) * 100) / 100;
        var fOfx = Math.Truncate((-Math.Log(amountOfShotsX, gOfx) + projectileDamageA) * 100) / 100;

        return fOfx;

    }
    
    
}