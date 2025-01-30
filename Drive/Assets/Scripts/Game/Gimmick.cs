
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gimmick : MonoBehaviour
{

    private bool isPlay = true;
    // Use this for initialization
    public void Start()
    {
        //Debug.LogError("transform :: " + transform.localRotation.y);
        StartCoroutine(Run());
    }

    IEnumerator Run()
    {

        var z = Time.deltaTime * 2f;
        if(transform.localRotation.y < 0)
        {
            z = Time.deltaTime * 2f * -1f;
        }

        while (isPlay)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + z);
            yield return new WaitForSeconds(0.1f);

            if (transform.localPosition.z < -50)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {

            var player = collision.gameObject.transform.parent.GetComponent<PlayerController>();
            if (player)
            {
                player.Damage(50);
                StartCoroutine(Explosions(player));
            }

            isPlay = false;
            var x = Random.Range(-0.5f, 0.5f);
            var y = Random.Range(1f, 1.5f);


            Rigidbody rb = GetComponent<Rigidbody>();
            Vector3 normal = collision.contacts[0].normal;
            Vector3 velocity = collision.rigidbody.velocity.normalized;
            velocity += new Vector3(x, y, normal.z * 5f);
            rb.AddForce(velocity * 10f, ForceMode.Impulse);
            StartCoroutine(Destroy());
        }
    }

    bool isDestroy = false;

    IEnumerator Explosions(PlayerController player)
    {
        // ここで必要な遅延を入れることもできます
        yield return new WaitForSeconds(0.3f);
        player.Explosions(transform);
    }


    IEnumerator Destroy()
    {

        if (isDestroy)
        {
            yield break;
        }

        isDestroy = true;

        int time = 0;
        while (time < 10)
        {
            yield return new WaitForSeconds(1f);
            time++;

        }
        Destroy(this.gameObject);
        yield return null;
    }
}
