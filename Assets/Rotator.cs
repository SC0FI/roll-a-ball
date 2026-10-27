using System;
using System.Collections;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    private float speed = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("touched by" + collision.gameObject.name);
    }

    public void touched(GameObject asd)
    {
        if (asd == gameObject)
        {
            ParticleSystem particle = asd.GetComponentInChildren<ParticleSystem>();
            particle.Play();
            BoxCollider collider = asd.GetComponent<BoxCollider>();
            collider.enabled = false;
            MeshRenderer renderer = asd.GetComponent<MeshRenderer>();
            renderer.enabled = false;

            StartCoroutine(DisableAfterDelay(asd.gameObject));
        }
    }

    private IEnumerator DisableAfterDelay(GameObject pickup)
    {
        yield return new WaitForSeconds(1f);

        if (pickup != null)
            pickup.SetActive(false);
    }
}
