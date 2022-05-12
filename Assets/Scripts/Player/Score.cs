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
    // Start is called before the first frame update
  
    private void Awake() {
       // test_Boomerang = GetComponentInChildren<Test_boomerang>();
        test_Boomerang.Score += AddScore;
        animatorController.Fall += SubsScore;
    }

    public void AddScore() {
        myScore += 1;
    }
    public void SubsScore() {
        if (myScore == 0) return;
        myScore -= 1;
        if (Substraction != null) Substraction();
    }
}
