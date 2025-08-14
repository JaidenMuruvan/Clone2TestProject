using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class RamenOrder
{
    public string bowlType;
    public string noodleType;
    public string brothType;
    public string proteinType;
    public string vegetableType;
}

public class OrderManager : MonoBehaviour
{
    [Header("Ingredients")]
    public string[] bowlOptions = { "blue", "green", "pink" };
    public string[] noodleOptions = { "chukamen", "kuyoumen", "torimen" };
    public string[] brothOptions = { "chicken", "vegetable", "pork" };
    public string[] proteinOptions = { "chicken", "egg", "tofu" };
    public string[] vegetableOptions = { "bokchoy", "mushrooms", "onions" };

    [Header("UI")]
    public Text orderText;
    public Text feedbackText;
    public Text currentOrderText;

    [Header("Timer Settings")]
    public float orderTimeLimit = 30f; // seconds
    private float timer;
    public Text timerText;
    private bool timerRunning;

    [SerializeField] public RamenOrder currentOrder;
    [SerializeField] public RamenOrder playerBowl;

    void Start()
    {
        playerBowl = new RamenOrder();
        GenerateOrder();
    }

    public void GenerateOrder()
    {
        currentOrder = new RamenOrder();

        //Bowl and broth always present
        currentOrder.bowlType = bowlOptions[Random.Range(0, bowlOptions.Length)];
        currentOrder.brothType = brothOptions[Random.Range(0, brothOptions.Length)];

        //50% chance for noodles
        if (Random.value < 0.5f)
            currentOrder.noodleType = noodleOptions[Random.Range(0, noodleOptions.Length)];

        //40% chance for protein
        if (Random.value < 0.4f)
            currentOrder.proteinType = proteinOptions[Random.Range(0, proteinOptions.Length)];

        //60% chance for vegetable
        if (Random.value < 0.6f)
            currentOrder.vegetableType = vegetableOptions[Random.Range(0, vegetableOptions.Length)];

        string orderString = $"Customer Order:\n{currentOrder.bowlType} bowl\n{currentOrder.brothType} broth";

        if (!string.IsNullOrEmpty(currentOrder.noodleType))
            orderString += $"\n{currentOrder.noodleType} noodles";

        if (!string.IsNullOrEmpty(currentOrder.proteinType))
            orderString += $"\n{currentOrder.proteinType}";

        if (!string.IsNullOrEmpty(currentOrder.vegetableType))
            orderString += $"\n{currentOrder.vegetableType}";

        orderText.text = orderString;
        feedbackText.text = "";
   

        playerBowl = new RamenOrder();

        //Reset and start timer
        timer = orderTimeLimit;
        timerRunning = true;
        UpdateTimerUI();
    }

    void Update()
    {
        if (timerRunning)
        {
            timer -= Time.deltaTime;
            UpdateTimerUI();

            if (timer <= 0f)
            {
                timer = 0f;
                timerRunning = false;
                OrderFailed(); //time up
            }
        } 
    }

    void OrderFailed()
    {
        feedbackText.text = "Time's up! Customer left angry!";
        //Could play sound, deduct points, etc.
        Invoke(nameof(GenerateOrder), 2f); //Wait then new order
    }

    void UpdateTimerUI()
    {
        timerText.text = $"Time: {Mathf.Ceil(timer)}s";
    }

    public void AddIngredient(string category, string ingredient)
    {
        switch (category)
        {
            case "Bowl":
                playerBowl.bowlType = ingredient;
                break;
            case "Noodle":
                playerBowl.noodleType = ingredient;
                break;
            case "Broth":
                playerBowl.brothType = ingredient;
                break;
            case "Protein":
                playerBowl.proteinType = ingredient;
                break;
            case "Vegetable":
                playerBowl.vegetableType = ingredient;
                break;
        }
    }

    public bool CheckOrder()
    {
        bool isCorrect = true;

        //Bowl and broth always required
        if (playerBowl.bowlType != currentOrder.bowlType)
            isCorrect = false;

        if (playerBowl.brothType != currentOrder.brothType)
            isCorrect = false;

        //Noodles
        if (!string.IsNullOrEmpty(currentOrder.noodleType) &&
            playerBowl.noodleType != currentOrder.noodleType)
            isCorrect = false;

        //Protein
        if (!string.IsNullOrEmpty(currentOrder.proteinType) &&
            playerBowl.proteinType != currentOrder.proteinType)
            isCorrect = false;

        //Vegetable
        if (!string.IsNullOrEmpty(currentOrder.vegetableType) &&
            playerBowl.vegetableType != currentOrder.vegetableType)
            isCorrect = false;

        return isCorrect;
    }
    public void ServeOrder()
    {
        timerRunning = false; //Stop timer 

        bool correct = true;

        if (playerBowl.bowlType != currentOrder.bowlType) correct = false;
        if (playerBowl.noodleType != currentOrder.noodleType) correct = false;
        if (playerBowl.brothType != currentOrder.brothType) correct = false;
        if (playerBowl.proteinType != currentOrder.proteinType) correct = false;
        if (playerBowl.vegetableType != currentOrder.vegetableType) correct = false;

        if (correct)
        {
            feedbackText.text = "Correct! Customer is happy!";
        }
        else
        {
            feedbackText.text = "Wrong order! Customer is upset!";
        }

        Invoke(nameof(GenerateOrder), 2f); //Wait 2 seconds, then new order
    }
}
