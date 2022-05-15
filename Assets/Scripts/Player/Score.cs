using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {
    public delegate void ScoreEvents();
    public event ScoreEvents Substraction;
    [SerializeField] AnimatorController animatorController;
    [SerializeField] AnimatorController animatorControllerOnline;
    [SerializeField] Test_boomerang test_Boomerang;
    public int myScore =0;
    int previus_score, current_map;
    Map_Manager map_mg;
    
    // Start is called before the first frame update
  
    private void Awake() {
       // test_Boomerang = GetComponentInChildren<Test_boomerang>();
        test_Boomerang.Score += AddScore;
        animatorController.Fall += SubsScore;
    }
    private void Start()
    {
        map_mg = GameObject.Find("Manager").GetComponent<Map_Manager>();
    }

    public void AddScore() {
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
    public void SubsScore() {
        if (myScore == 0) return;
        myScore -= 1;
        if (Substraction != null) Substraction();
    }
}
