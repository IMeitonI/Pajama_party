using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour {
    GameObject[] points = new GameObject[5];
    [SerializeField] GameObject reference;
    [SerializeField] Sprite paw;
    [SerializeField] Sprite circle;
    [SerializeField] Score scoreScript;
    [SerializeField] GameObject winner, buttoms;
    [SerializeField] ParticleSystem confetti;
   
    private void Start() {
        scoreScript.Substraction += SubstractionScore;
        for (int i = 0; i < points.Length; i++) {
            points[i] = Instantiate(reference, transform);
        }
    }
    private void Update() {
        if (gameObject) {
            for (int i = 0; i < scoreScript.myScore; i++) {
                if(scoreScript.myScore >= 6) {
                    for (int j = 0; j < 5; j++) {
                        points[j].GetComponent<Image>().sprite = paw;
                        points[j].GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 1);
                    }
                    break;
                }
                points[i].GetComponent<Image>().sprite = paw;
                points[i].GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 1);
            }
            if (scoreScript.myScore >= 5) {
                Map_Manager.winner = true;
                winner.SetActive(true);
                confetti.Play();
                buttoms.SetActive(true);
            }
        }
    }

    public void SubstractionScore() {
        points[scoreScript.myScore].GetComponent<Image>().sprite = circle;
        points[scoreScript.myScore].GetComponent<RectTransform>().localScale = new Vector3(2.1f, 2f, 2);
    }

}
