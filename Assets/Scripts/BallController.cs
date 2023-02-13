using System;
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
    
    private AudioSource pong;
    
    public GameObject modifier2;

    private void Awake()
    {
        modifier2.gameObject.SetActive(false);
        _rigidbody = GetComponent<Rigidbody>();
        pong = GetComponent<AudioSource>();
    }

    void Start()
    {
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
        pong.pitch = 1;
    }

    void Again()
    {
        _rigidbody.AddForce(new Vector3(0.5f,0,0.5f) * speed * ballstart, ForceMode.Force);
    }

    private void Update()
    {
        vel = _rigidbody.velocity;
    }

    private void ChangeColor(TextMeshProUGUI score, int iscore)
    {
        switch (iscore)
        {
            case 0:
                score.color = Color.white;
                break;
            case 1:
                score.color = new Color(1, 227/255f, 227/255f);
                break;
            case 2:
                score.color = new Color(1, 212/255f, 212/255f);
                break;
            case 3:
                score.color = new Color(252/255f, 174/255f, 174/255f);
                break;
            case 4:
                score.color = new Color(1, 138/255f, 138/255f);
                break;
            case 5:
                score.color = new Color(1, 92/255f, 92/255f);
                break;
            case 6:
                score.color = new Color(1, 46/255f, 46/255f);
                break;
            case 7:
                score.color = new Color(1, 0, 0);
                break;
            case 8:
                score.color = new Color(209/255f, 0, 0);
                break;
            case 9:
                score.color = new Color(163/255f, 0, 0);
                break;
            case 10:
                score.color = new Color(117/255f, 0, 0);
                break;
        }
    }

    private void ChangeText()
    {
        if (score1 == pointsToWin)
        {
            win.text = $"Game Over,\n Left Paddle Wins";
            Debug.Log("Game Over, Left Paddle Wins");
        }
        else if (score2 == pointsToWin)
        {
            win.text = $"Game Over,\n Right Paddle Wins";
            Debug.Log("Game Over, Right Paddle Wins");
            win.gameObject.SetActive(true);
        }
        else
            return;
        win.gameObject.SetActive(true);
        score2 = 0;
        score1 = 0;
        won = true;
        pong.pitch = 1;
    }

    private void Modifier2()
    {
        if (score1 + score2 == 4)
            modifier2.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("modif"))
        {
            Debug.Log("Speed changed!");
            _rigidbody.velocity *= 1.2f;
            vel = _rigidbody.velocity;
        }
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
                ChangeText();
                ChangeColor(scorep1, score1);
            }
            else
            {
                score2++;
                ballstart = -1;
                Debug.Log("Player 2 scored! The score is: " + score1 + " - " + score2);
                ChangeText();
                ChangeColor(scorep2, score2);
            }

            Modifier2();
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
                pong.Play();
                pong.pitch++;
                _rigidbody.velocity *= 1.2f;
                vel = _rigidbody.velocity;
            }
        }
    }
}
