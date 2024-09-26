using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameLogicController : MonoBehaviour
{

    [SerializeField] 
    ThrowDice td;
    public int sum;
    [SerializeField] 
    public Button one;
    [SerializeField] 
    public Button two;
    [SerializeField] 
    public Button three;
    [SerializeField] 
    public Button four;
    [SerializeField] 
    public Button five;
    [SerializeField] 
    public Button six;
    [SerializeField] 
    public Button seven;
    [SerializeField] 
    public Button eight;
    [SerializeField] 
    public Button nine;
    [SerializeField]
    public TMP_Text Options;
    [SerializeField]
    public TMP_Text Uitgespeeld;
    [SerializeField]
    public RectTransform  UitgespeeldPanel;


    public ArrayList available = new ArrayList();
    public List<Tuple<int, int>> legalMoves;
    public ArrayList possibleMoves = new ArrayList();
    private string options;
    private Dictionary<string, int> map = new Dictionary<string, int>();


    // Start is called before the first frame update
    void Start()
    {
        //UitgespeeldPanel.setActive(false);
        Uitgespeeld.text = "";
        for(int i = 1; i <= 10; i++){
            available.Add(i);
            Debug.Log(i);
        }

        for(int j = 1; j <= 10; j++){
            string k = "Button_" + j;
            map.Add(k, j);
            Debug.Log(k);
        }
    }

    public void winOrLoss(){
        if(options.Equals("Je Opties Zijn: \n")){
            if (available.Count == 0) Uitgespeeld.text = "Gefeliciteerd! Je hebt 'm dichtgegooid!";
            else {
                int result = 0;
                foreach(int i in available){
                    result += i;
                }
                string p = "Jouw totaal score is: " + result;
                Uitgespeeld.text = p;
            }
        } else {
            GameObject.Find("Dice Button").GetComponent<Button>().interactable = false;
        }
    }


    public void recalculateLegal(){
        sum = td.GetDice1() + td.GetDice2();
        int[] availaList = Array.ConvertAll(available.ToArray(), item => (int)item);
        legalMoves = FindAllTwoSumPairs(availaList, sum);
        options = "Je Opties Zijn: \n";
        foreach (var pair in legalMoves)
        {
            Debug.Log($"Pair: {pair}");
            string k = pair.ToString().Replace("(","").Replace(")", "");
            options += k + "\n";

        }
        if (sum <= 10 && available.Contains(sum)) options += sum;
        Options.text = options;
        winOrLoss();
    }

    public void OnButtonClick(){
        var theButton  = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        if (available.Contains(map[theButton.name])){
            if ((td.GetDice1() + td.GetDice2()) == map[theButton.name]){ 
                theButton.GetComponent<Image>().color = Color.red;
                available.Remove(map[theButton.name]);
                theButton.GetComponent<Button>().interactable = false;
                GameObject.Find("Dice Button").GetComponent<Button>().interactable = true;
                Options.text = "Nice Move!";
            }
            else{
                foreach(var pair in legalMoves){
                    if (map[theButton.name] == pair.Item1){
                        var ButtonOther = GameObject.Find("Button_" + pair.Item2);
                        theButton.GetComponent<Image>().color = Color.red;
                        ButtonOther.GetComponent<Image>().color = Color.red;
                        available.Remove(map[theButton.name]);
                        available.Remove(map[ButtonOther.name]);
                        theButton.GetComponent<Button>().interactable = false;
                        ButtonOther.GetComponent<Button>().interactable = false;
                        GameObject.Find("Dice Button").GetComponent<Button>().interactable = true;
                        Options.text = "Nice Move!";
                    } else if (map[theButton.name] == pair.Item2) {
                        var ButtonOther = GameObject.Find("Button_" + pair.Item1);
                        theButton.GetComponent<Image>().color = Color.red;
                        ButtonOther.GetComponent<Image>().color = Color.red;
                        available.Remove(map[theButton.name]);
                        available.Remove(map[ButtonOther.name]);
                        theButton.GetComponent<Button>().interactable = false;
                        ButtonOther.GetComponent<Button>().interactable = false;
                        GameObject.Find("Dice Button").GetComponent<Button>().interactable = true;
                        Options.text = "Nice Move!";

                    } else {
                        //Not a legal move!!!!
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        sum = td.GetDice1() + td.GetDice2();
    }


    public ArrayList getAvailable(){
        return available;
    }

    public int getSum(){
        return sum; 
    }

    public static List<Tuple<int, int>> FindAllTwoSumPairs(int[] nums, int target)
    {
        List<Tuple<int, int>> result = new List<Tuple<int, int>>();
        HashSet<string> seenPairs = new HashSet<string>();

        for (int i = 0; i < nums.Length; i++)
        {
            for (int j = i + 1; j < nums.Length; j++)
            {
                if (nums[i] + nums[j] == target)
                {
                    int num1 = Math.Min(nums[i], nums[j]);
                    int num2 = Math.Max(nums[i], nums[j]);
                    string pairString = $"{num1},{num2}";

                    if (!seenPairs.Contains(pairString))
                    {
                        result.Add(new Tuple<int, int>(num1, num2));
                        seenPairs.Add(pairString);
                    }
                }
            }
        }

        return result;
    }

    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
