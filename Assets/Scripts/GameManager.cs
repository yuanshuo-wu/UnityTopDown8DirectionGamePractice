using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.UI;

public sealed class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    //[SerializeField] private GameObject gameOverUI;
    //[SerializeField] private Text scoreText;
    //[SerializeField] private Text livesText;

    public Player player;
    public GameObject slimeMonsterSpawn;
    public GameObject skeletonMonsterSpawn;
    public Boss boss;

    private int score;
    private int lives;

    public int Score => score;
    public int Lives => lives;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        //player = FindObjectOfType<Player>();
        //slimeMonster = FindObjectOfType<Monster>();
        //skeletonMonster = FindObjectOfType<AnimatedMonster>();
        //boss = FindObjectOfType<Boss>();

        NewGame();
    }

    private void Update()
    {
        if (lives <= 0 && Input.GetKeyDown(KeyCode.Return))
        {
            NewGame();
        }

        if (score >= 20)
        {
            SkeletonSpawning();
        }

        if (score >= 40)
        {
            BossSpawning();
        }

    }


    private void NewGame()
    {
        //gameOverUI.SetActive(false);

        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        slimeMonsterSpawn.gameObject.SetActive(true);

        Respawn();
    }

    private void Respawn()
    {
        Vector3 position = player.transform.position;
        position.x = 0f;
        player.transform.position = position;
        player.gameObject.SetActive(true);
    }

    private void GameOver()
    {
        //gameOverUI.SetActive(true);
        slimeMonsterSpawn.gameObject.SetActive(false);
        skeletonMonsterSpawn.gameObject.SetActive(false);
        boss.gameObject.SetActive(false);
    }

    private void SetScore(int score)
    {
        this.score = score;
        //scoreText.text = score.ToString().PadLeft(4, '0');
    }

    private void SetLives(int lives)
    {
        this.lives = Mathf.Max(lives, 0);
        //livesText.text = this.lives.ToString();
    }

    private void SkeletonSpawning() 
    {
        skeletonMonsterSpawn.gameObject.SetActive(true);
    }

    private void BossSpawning()
    {
        slimeMonsterSpawn.gameObject.SetActive(false);
        skeletonMonsterSpawn.gameObject.SetActive(false);
        boss.gameObject.SetActive(true);

    }


    public void OnPlayerKilled(Player player)
    {
        SetLives(lives - 1);

        player.gameObject.SetActive(false);

        if (lives > 0)
        {
            Invoke(nameof(NewRound), 1f);
        }
        else
        {
            GameOver();
        }
    }

    public void OnSlimeKilled(Monster slime)
    {
        slime.gameObject.SetActive(false);

        SetScore(score + slime.score);
        Debug.Log("Current Score: " + score);

    }

    public void OnSkeletonKilled(AnimatedMonster skeleton)
    {
        skeleton.gameObject.SetActive(false);

        SetScore(score + skeleton.score);
        Debug.Log("Current Score: " + score);

    }


    //public void OnBoundaryReached()
    //{
    //    if (invaders.gameObject.activeSelf)
    //    {
    //        invaders.gameObject.SetActive(false);

    //        OnPlayerKilled(player);
    //    }
    //}

}