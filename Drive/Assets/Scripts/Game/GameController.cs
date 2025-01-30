using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    [SerializeField]
    public PlayerController player = null;
    [SerializeField]
    public Text score = null;

    [SerializeField]
    public Scrollbar scrollbar = null;
    [SerializeField]
    public ParticleSystem explosions = null;


    private Goal goal = null;

    private bool gameOver = false;
    private bool isResult = false;


    private int[,] _data = null;

    float distance = 0f;
    int count = 0;

    int maxBody = 100;

    void Start()
    {
        Setup();
    }

    public void Setup(){

        //_data = _csvDownroad.readCSVData (CSVDOWNROAD_PATH);
        goal = transform.Find("Goal").GetComponent<Goal>();

        player.SetUp(this);
        maxBody = player.Maxbody;


        StartCoroutine(__UpdateContoroller());

    }

    /// <summary>
    /// 毎秒アップデートするよう
    /// </summary>
    /// <returns>The contoroller.</returns>
    private IEnumerator __UpdateContoroller()
    {
        while (true)
        {
            score.text = string.Format("{0}m", (int)distance);
            distance += player.Speed;
            player.UpdateDistance(player.Speed);

            yield return new WaitForSeconds(0.01f);

            if (gameOver)
            {
                if (!isResult)
                {
                    isResult = true;
                    player.enabled = false;
                    yield return new WaitForSeconds(1f);
                    DataManager.nowGetScore = (int)distance;
                    NextScene();
                    yield break;
                }
            }
            if (goal.IsGoal())
            {
                isResult = true;
                player.enabled = false;
                player.Goal();
                yield return StartCoroutine(Goal());
                yield break;
            }
        }
    }

    private IEnumerator Goal() {
        float count = 0f;
        while (true)
        {
            score.text = string.Format("{0}m", (int)distance);
            distance += player.Speed;
            player.UpdateDistance(player.Speed);

            yield return new WaitForSeconds(0.01f);
            if(count >= 3)
            {
                DataManager.nowGetScore = (int)distance;
                NextScene();
                yield break;
            }
            count += 0.01f;
        }
    }

    private void NextScene()
    {
        Debug.LogError("NextScene");
        SceneManager.LoadScene("Result");
        SceneMoveManager.Instance.Transfer("Result");
    }

    /// <summary>
    ///　現在距離
    /// </summary>
    /// <value>The distance.</value>
    public float Distance{
        get { return distance; }
    }

    public void DamageBody(int value)
    {
        explosions.Play();
        maxBody = maxBody - value;
        Debug.Log((maxBody) / 100f);

        scrollbar.size = (float)maxBody / (float)player.Maxbody;

        if (maxBody <= 0)
        {
            gameOver = true;
        }
    }

    public void Explosions(Transform tm)
    {
        explosions.transform.localPosition = tm.localPosition;

    }

}
