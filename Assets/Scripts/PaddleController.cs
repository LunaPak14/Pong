using UnityEngine;

public class PaddleController : MonoBehaviour
{
    [SerializeField] private GameObject lpaddle;
    [SerializeField] private GameObject rpaddle;
    [SerializeField] private int paddleSpeed = 50;

    private void FixedUpdate()
    {
        float leftValue = Input.GetAxis("Left paddle");
        float rightValue = Input.GetAxis("Right paddle");
        
        Vector3 lforce = Vector3.left * (leftValue * paddleSpeed * Time.deltaTime);
        Vector3 rforce = Vector3.left * (rightValue * paddleSpeed * Time.deltaTime);
        
        Rigidbody lrb = lpaddle.GetComponent<Rigidbody>();
        lrb.velocity = lforce;

        Rigidbody rrb = rpaddle.GetComponent<Rigidbody>();
        rrb.velocity = rforce;
    }
}
