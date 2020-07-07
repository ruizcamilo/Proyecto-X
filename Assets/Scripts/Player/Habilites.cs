using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilites : MonoBehaviour
{
    public Collider2D body;
    public float invulnerabilityDuration;
    public float invulnerabilityRecovery;
    public float invulnerabilityFrame = 0.5f;
    
    private Timer _invDurationTimer;
    private Timer _invRecoveryTimer;
    private SpriteRenderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        _invDurationTimer = new Timer();
        _invDurationTimer.Duration = invulnerabilityDuration;
        _invRecoveryTimer = new Timer();
        _invRecoveryTimer.Duration = invulnerabilityRecovery;
        _renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && _invRecoveryTimer.Finished)
        {
            Debug.Log("Inmortal");
            StartCoroutine(Invulnerability());
        }
    }

    IEnumerator Invulnerability()
    {
        _invDurationTimer.Run();
        body.enabled = false;
        while (_invDurationTimer.Finished)
        {
            yield return new WaitForSecondsRealtime(invulnerabilityFrame);
            _renderer.enabled = false;
            yield return new WaitForSecondsRealtime(invulnerabilityFrame);
            _renderer.enabled = true;
            yield return null;
        }
        body.enabled = true;
        _invRecoveryTimer.Run();        
    }
}
