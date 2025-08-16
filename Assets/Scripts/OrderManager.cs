using System.Collections;
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

    [Header("Boiling")]
    public GameObject broth;

    [Header("Money")]
    public int playerMoney = 0;
    public Text moneyText;
    private float orderStartTime;

    [Header("Costs")]
    public int bowlCost = 5;
    public int noodleCost = 3;
    public int brothCost = 4;
    public int proteinCost = 6;
    public int vegetableCost = 2;

    [Header("Order Reward")]
    public int orderReward = 20;


    [Header("UI")]
    public Text orderText;
    public Text feedbackText;
    public Text currentBowlText;
    public GameObject serveBtn;

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
        UpdateMoneyUI();
    }

    public void UpdateMoneyUI()
    {
        if (playerMoney < 0)
        {
            playerMoney = 0;
            EndGame();
        }

        moneyText.text = $"Money: ${playerMoney}";
    }

    public void EndGame()
    {
        Debug.Log("Game Over! You ran out of money.");
        feedbackText.text = "Game Over! You ran out of money.";

        foreach (DragDrop ingredient in FindObjectsOfType<DragDrop>())
        {
            ingredient.enabled = false;
        }

        timerRunning = false;
    }
    public void GenerateOrder()
    {
        orderStartTime = Time.time;
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
        StartCoroutine(DelayTimer());
    }

    IEnumerator DelayTimer()
    {
        yield return new WaitForSeconds(2f);

        timer = orderTimeLimit;
        timerRunning = true;
        UpdateTimerUI();
    }
    void Update()
    {
        if (timerRunning)
        {
            timer -= Time.deltaTime;
            //StartCoroutine(BrothBoiling());
            UpdateTimerUI();

            if (timer <= 0f)
            {
                timer = 0f;
                timerRunning = false;
                OrderFailed(); //time up
            }
        } 
    }

    public IEnumerator BrothBoiling()
    {
        broth.SetActive(false);

        yield return new WaitForSeconds(2f);

        broth.SetActive(true);
    }

    void OrderFailed()
    {
        feedbackText.text = "Time's up! Customer left angry!";
        
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
                playerMoney -= bowlCost;
                break;
            case "Noodle":
                playerBowl.noodleType = ingredient;
                playerMoney -= noodleCost;
                break;
            case "Broth":
                playerBowl.brothType = ingredient;
                playerMoney -= brothCost;
                break;
            case "Protein":
                playerBowl.proteinType = ingredient;
                playerMoney -= proteinCost;
                break;
            case "Vegetable":
                playerBowl.vegetableType = ingredient;
                playerMoney -= vegetableCost;
                break;
        }

        UpdateMoneyUI();
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

    private int CalculateReward(RamenOrder order)
    {
        int reward = 0;

        reward += 5; //Base reward, reward for only bowl and broth

        if (!string.IsNullOrEmpty(order.noodleType))
            reward += 3;
            Debug.Log(reward);

        if (!string.IsNullOrEmpty(order.proteinType))
            reward += 6;
            Debug.Log(reward);


        if (!string.IsNullOrEmpty(order.vegetableType))
            reward += 2;
            Debug.Log(reward);


        return reward;
    }
    public void ServeOrder()
    {
        timerRunning = false; //Stop timer 
        StartCoroutine(GhostBtn());

        bool correct = true;

        if (playerBowl.bowlType != currentOrder.bowlType) correct = false;
        if (playerBowl.noodleType != currentOrder.noodleType) correct = false;
        if (playerBowl.brothType != currentOrder.brothType) correct = false;
        if (playerBowl.proteinType != currentOrder.proteinType) correct = false;
        if (playerBowl.vegetableType != currentOrder.vegetableType) correct = false;

        if (correct)
        {
            int reward = CalculateReward(currentOrder);
            float timeTaken = Time.time - orderStartTime;
            int stars = CalculateStars(timeTaken);

            playerMoney += reward;
            feedbackText.text = $"Correct! Customer is happy! (+${reward}) | x{stars} stars";
            UpdateMoneyUI();
        }
        else
        {
            feedbackText.text = "Wrong order! Customer is upset!";
        }

        

        Invoke(nameof(GenerateOrder), 2f); //Wait 2 seconds, then new order
    }

    private int CalculateStars(float timeTaken)
    {
        if (timeTaken <= 20f)
            return 3;
        else if (timeTaken <= 45f)
            return 2;
        else if (timeTaken <= 55f)
            return 1;
        else
            return 0;
    }

    IEnumerator GhostBtn()
    {
        serveBtn.SetActive(false);

        yield return new WaitForSeconds(4f);

        serveBtn.SetActive(true);

    }

    public void UpdateCurrentBowlText()
    {
        string display = "Last added to bowl:\n";

        if (!string.IsNullOrEmpty(playerBowl.bowlType))
            display += $"{playerBowl.bowlType} bowl\n";

        if (!string.IsNullOrEmpty(playerBowl.brothType))
            display += $"{playerBowl.brothType} broth\n";

        if (!string.IsNullOrEmpty(playerBowl.noodleType))
            display += $"{playerBowl.noodleType} noodles\n";

        if (!string.IsNullOrEmpty(playerBowl.proteinType))
            display += $"{playerBowl.proteinType}\n";

        if (!string.IsNullOrEmpty(playerBowl.vegetableType))
            display += $"{playerBowl.vegetableType}\n";

        currentBowlText.text = display;
    }
}
