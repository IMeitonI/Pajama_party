using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreOnline : MonoBehaviour
{
    public delegate void ScoreEvents();
    public event ScoreEvents Substraction;
    [SerializeField] AnimatorControllerOnline animatorControllerOnline;
    [SerializeField] BoomerangLogic my_Boomerang;
    public int myScore = 0;
    int previus_score, current_map;
    MapManagerOnline map_mg;

    // Start is called before the first frame update

    private void Awake()
    {
        // test_Boomerang = GetComponentInChildren<Test_boomerang>();
        // my_Boomerang.killEvent += AddScore;
    }
    private void OnEnable()
    {
        animatorControllerOnline.Fall += SubsScore;
    }
    private void OnDisable()
    {
        animatorControllerOnline.Fall -= SubsScore;
    }
    private void Start()
    {
        map_mg = GameObject.Find("Manager").GetComponent<MapManagerOnline>();
    }

    public void AddScore()
    {
        if (current_map == map_mg.current_map)
        {
            if (previus_score == myScore - 1) return;
            myScore += 1;
        }
        else
        {
            current_map = map_mg.current_map;
            previus_score = myScore;
            AddScore();
        }

    }
    public void SubsScore()
    {
        if (myScore == 0) return;
        else if (current_map == map_mg.current_map)
        {
            if (previus_score == myScore + 1) return;
            myScore -= 1;
        }
        else
        {
            current_map = map_mg.current_map;
            previus_score = myScore;
            SubsScore();
        }
        if (Substraction != null) Substraction();
    }
}