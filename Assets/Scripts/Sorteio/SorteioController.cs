using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SorteioController : MonoBehaviour
{
    [SerializeField] public static GameObject instanciaSorteioController;
    private string[] namesSorteio;
    private int[] statusSorteio;
    private int winnerSorteio;
    private static SorteioController _instanciaSorteioController;
    [SerializeField] private Text textNamesSorteio01;
    [SerializeField] private Text textNamesSorteio02;
    [SerializeField] private Text textNamesSorteio03;
    [SerializeField] private Text textNamesSorteio04;
    [SerializeField] private Button buttonSorteio;
    [SerializeField] private Text textFeed;
    public static SorteioController InstanciaSorteioController {
        get {
            if(_instanciaSorteioController == null) {
                _instanciaSorteioController = instanciaSorteioController.GetComponent<SorteioController>();
            }
            return _instanciaSorteioController;
        }
    }

    void Awake() {
        instanciaSorteioController = FindObjectOfType<SorteioController>().gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {

        InitSorteio();
    }

    private void InitSorteio() {
        namesSorteio = LoadData.Load();
        InitStatusSorteio();
        UpdateNamesSorteioUI();
        UpdateMessageFeed("Sorteio começou vain");
        winnerSorteio = ChooseWinner();
    }

    private void InitStatusSorteio() {
        statusSorteio = new int[namesSorteio.Length];
        for(int i = 0;i < namesSorteio.Length;i++) {
            statusSorteio[i] = 0;
        }
    }

    private void UpdateNamesSorteioUI() {
        string[] newNames = new string[4];
        int num = 0;
        for(int i = 0;i < statusSorteio.Length;i++) {
            if(statusSorteio[i] != -1) {
                newNames[(int)(num / 20)] += namesSorteio[i] + "\n";
                num++;
            }
        }

        textNamesSorteio01.text = newNames[0];
        textNamesSorteio02.text = newNames[1];
        textNamesSorteio03.text = newNames[2];
        textNamesSorteio04.text = newNames[3];

        if(IsSorteioFinished(num)) {
            buttonSorteio.enabled = false;
            UpdateMessageFeed(namesSorteio[winnerSorteio] + " GANHOU O SORTEIO VAIN GARALHOR");
        }
    }

    private bool IsSorteioFinished(int num) {
        if(num > 1) {
            return false;
        }
        return true;
    }

    private void UpdateMessageFeed(string msg) {
        textFeed.text = msg;
    }

    private void ChooseRandomLoser() {
        List<int> currentContestants = new List<int>();
        for(int i = 0;i < namesSorteio.Length;i++) {
            if(statusSorteio[i] == 0) {
                currentContestants.Add(i);
            }
        }

        int nextLoser = Random.Range(0, currentContestants.Count);
        statusSorteio[currentContestants[nextLoser]] = -1;

        UpdateMessageFeed(namesSorteio[currentContestants[nextLoser]] + " foi degustado");
    }

    private int ChooseWinner() {
        int winner = Random.Range(0, namesSorteio.Length);
        statusSorteio[winner] = 1;
        //Debug.Log(namesSorteio[winner]);
        return winner;
    }

    public void NextStep() {
        //Debug.Log("porra meu");
        ChooseRandomLoser();
        UpdateNamesSorteioUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
