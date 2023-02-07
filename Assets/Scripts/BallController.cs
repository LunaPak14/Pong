using TMPro;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private int ballstart = 1; //where the ball goes after scoring a point/starting the game.
    //If ballstart == 1; the ball starts moving in the right side.
    //If ballstart == -1; the ball starts moving in the left side.
    private float speed = 300f;
    private Rigidbody _rigidbody;
    private Vector3 vel;
    
    [SerializeField] private int pointsToWin = 11;

    private bool won = false;

    [SerializeField] private TextMeshProUGUI scorep1;
    [SerializeField] private TextMeshProUGUI scorep2;
    [SerializeField] private TextMeshProUGUI win;
    private int score1 = 00;
    private int score2 = 00;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Init();
        win.gameObject.SetActive(false);
        Again();
    }

    void Init()
    {
        speed = 300f;
        transform.position = Vector3.zero;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    void Again()
    {
        _rigidbody.AddForce(new Vector3(0.5f,0,0.5f) * speed * ballstart, ForceMode.Force);
    }

    private void Update()
    {
        vel = _rigidbody.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            if (collision.gameObject.name == "RightGoal")
            {
                score1++;
                ballstart = 1;
                Debug.Log("Player 1 scored! The score is: " + score1 + " - " + score2);
                if (score1 == pointsToWin)
                {
                    win.text = $"Game Over,\n Left Paddle Wins";
                    Debug.Log("Game Over, Left Paddle Wins");
                    win.gameObject.SetActive(true);
                    score2 = 0;
                    score1 = 0;
                    won = true;
                }
            }
            else
            {
                score2++;
                ballstart = -1;
                Debug.Log("Player 2 scored! The score is: " + score1 + " - " + score2);
                if (score2 == pointsToWin)
                {
                    win.text = $"Game Over,\n Right Paddle Wins";
                    Debug.Log("Game Over, Right Paddle Wins");
                    win.gameObject.SetActive(true);
                    score2 = 0;
                    score1 = 0;
                    won = true;
                }
            }

            scorep1.text = score1 < 10 ? ("0" + score1) : score1.ToString();
            scorep2.text = score2 < 10 ? ("0" + score2) : score2.ToString();
            Init();
            
            if (!won)
                Again();
        }
        else 
        {
            var dir = Vector3.Reflect(vel.normalized, collision.contacts[0].normal);
            _rigidbody.velocity = dir * vel.magnitude;
            
            if (collision.gameObject.CompareTag("Paddle"))
            {
                _rigidbody.velocity *= 1.2f;
                vel = _rigidbody.velocity;
            }
        }
    }
}
