using System;
using UnityEngine;

namespace Player
{
    public class PlayerScript : MonoBehaviour
    {
        // Start is called before the first frame update
        private Rigidbody2D body;
        //private new Transform transform;

        private void Start()
        {
            body = GetComponent<Rigidbody2D>();
            //transform = GetComponent<Transform>();
            Debug.Log("Hello");
        }

        // Update is called once per frame
        private void Update()
        {
            body.velocity = new Vector2(Input.GetAxis("Horizontal")*5, Input.GetAxis("Vertical")*5);
            if (!Input.anyKey) return;
            var velocity = body.velocity;
            //transform.rotation = Quaternion.Euler(0.0f,0.0f,(float)Math.Atan2(velocity.y,velocity.x)*60);
        }
    }
}