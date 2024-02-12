using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentacaoSetas : MonoBehaviour
{
    [SerializeField] private float velocidade;
    private Rigidbody2D _rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveHorizontally(velocidade);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MoveHorizontally(velocidade * -1);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            MoveVertically(velocidade);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            MoveVertically(velocidade * -1);
        }
    }

    void MoveHorizontally(float velocity)
    {
        _rigidbody2D.velocity = new Vector2(velocity, 0);
    }

    void MoveVertically(float velocity)
    {
        Transform t = transform;
        Vector3 newPos = t.position;
        newPos.y += velocity * Time.deltaTime;

        t.position = newPos;
    }
}
