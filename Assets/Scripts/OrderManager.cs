using System.Collections;
using System.Collections.Generic;
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

[System.Serializable]
public class Recipe
{
    public string recipeName;
    public List<string> ingredients;
    public int reward;
}

//Enum to distinguish between recipe orders and random orders
public enum OrderType
{
    Recipe,
    Random
}

public class OrderManager : MonoBehaviour
{
    [Header("Ingredients")]
    public string[] bowlOptions = { "Blue Bowl", "Green Bowl", "Pink Bowl" };
    public string[] noodleOptions = { "Chukamen", "Kuyou-men", "Tori-men" };
    public string[] brothOptions = { "Chicken Broth", "Vegetable Broth", "Pork Bone Broth" };
    public string[] proteinOptions = { "Chicken", "Boiled Egg", "Tofu" };
    public string[] vegetableOptions = { "Bok Choy", "Shiitake Mushrooms", "Scallions" };

    [Header("Boiling")]
    public GameObject brothChicken;
    public GameObject brothPork;
    public GameObject brothVeg;

    public GameObject brothChickenCover;
    public GameObject brothPorkCover;
    public GameObject brothVegCover;

    public GameObject chickenBtn;
    public GameObject porkBtn;
    public GameObject vegBtn;

    [Header("Money")]
    public int playerMoney = 0;
    public Text moneyText;
    private float orderStartTime;

    [Header("Costs")]
    public int bowlCost = 1;
    public int noodleCost = 1;
    public int brothCost = 2;
    public int proteinCost = 2;
    public int vegetableCost = 1;

    [Header("UI")]
    public Text orderText;
    public Text feedbackText;
    public Text currentBowlText;
    public GameObject serveBtn;
    public GameObject bowlGreen;
    public GameObject bowlBlue;
    public GameObject bowlPink;
    public GameObject oneStar;
    public GameObject twoStar;
    public GameObject threeStar;


    [Header("Timer Settings")]
    public float orderTimeLimit = 30f;
    private float timer;
    public Text timerText;
    private bool timerRunning;

    [SerializeField] public RamenOrder currentOrder;
    [SerializeField] public RamenOrder playerBowl;

    public List<Recipe> standardRecipes = new List<Recipe>();

    private int orderCount = 0;
    private Recipe currentRecipe;
    private bool isRecipeOrder = false;

    void Start()
    {
        playerBowl = new RamenOrder();

        //Setup of standard recipes
        Recipe recipe1 = new Recipe
        {
            recipeName = "Misco Chicken Ramen",
            ingredients = new List<string> { "Blue Bowl", "Chicken Broth", "Chukamen", "Bok Choy", "Chicken", "Scallions" },
            reward = 10
        };
        Recipe recipe2 = new Recipe
        {
            recipeName = "Vegetable Shio Ramen (Vegetarian)",
            ingredients = new List<string> { "Green Bowl", "Vegetable Broth", "Tori-men", "Bok Choy", "Tofu", "Scallions" },
            reward = 5
        };
        Recipe recipe3 = new Recipe
        {
            recipeName = "Tonkotsu Egg Ramen",
            ingredients = new List<string> { "Pink Bowl", "Pork Bone Broth", "Tori-men", "Bok Choy", "Boiled Egg", "Shiitake Mushrooms" },
            reward = 8
        };

        standardRecipes.Add(recipe1);
        standardRecipes.Add(recipe2);
        standardRecipes.Add(recipe3);

        GenerateNextOrder();
        UpdateMoneyUI();
    }

    public void GenerateNextOrder()
    {
        orderCount++;

        //First two orders are always recipes (tutorial)
        if (orderCount == 1)
        {
            isRecipeOrder = true;
            currentRecipe = standardRecipes[0];
            GenerateRecipeOrder(currentRecipe);
        }
        else if (orderCount == 2)
        {
            isRecipeOrder = true;
            currentRecipe = standardRecipes[1];
            GenerateRecipeOrder(currentRecipe);
        }
        else
        {
            //Randomly choose recipe or random order
            int rand = Random.Range(0, 2);
            if (rand == 0)
            {
                isRecipeOrder = true;
                currentRecipe = standardRecipes[Random.Range(0, standardRecipes.Count)];
                GenerateRecipeOrder(currentRecipe);
            }
            else
            {
                isRecipeOrder = false;
                GenerateRandomOrder();
            }
        }
    }

    private void GenerateRecipeOrder(Recipe recipe)
    {
        orderStartTime = Time.time;
        currentOrder = new RamenOrder();

        
        foreach (string ing in recipe.ingredients)
        {
            if (ing == "Blue Bowl" || ing == "Green Bowl" || ing == "Pink Bowl")
                currentOrder.bowlType = ing;
            else if (ing.EndsWith("h"))
                currentOrder.brothType = ing;
            else if (System.Array.Exists(noodleOptions, x => x == ing))
                currentOrder.noodleType = ing;
            else if (System.Array.Exists(proteinOptions, x => x == ing))
                currentOrder.proteinType = ing;
            else if (System.Array.Exists(vegetableOptions, x => x == ing))
                currentOrder.vegetableType = ing;
        }

        //Display order
        DisplayOrderText(currentOrder);
        feedbackText.text = "";

        playerBowl = new RamenOrder();
        StartCoroutine(DelayTimer());
    }

    public void GenerateRandomOrder()
    {
        orderStartTime = Time.time;
        currentOrder = new RamenOrder();

        currentOrder.bowlType = bowlOptions[Random.Range(0, bowlOptions.Length)];
        currentOrder.brothType = brothOptions[Random.Range(0, brothOptions.Length)];

        if (Random.value < 0.5f)
            currentOrder.noodleType = noodleOptions[Random.Range(0, noodleOptions.Length)];

        if (Random.value < 0.4f)
            currentOrder.proteinType = proteinOptions[Random.Range(0, proteinOptions.Length)];

        if (Random.value < 0.6f)
            currentOrder.vegetableType = vegetableOptions[Random.Range(0, vegetableOptions.Length)];

        DisplayOrderText(currentOrder);
        feedbackText.text = "";

        playerBowl = new RamenOrder();
        StartCoroutine(DelayTimer());
    }

    private void DisplayOrderText(RamenOrder order)
    {
        string orderString = $"Customer Order:\n{order.bowlType} \n{order.brothType}";

        if (!string.IsNullOrEmpty(order.noodleType))
            orderString += $"\n{order.noodleType} Noodles";
        if (!string.IsNullOrEmpty(order.proteinType))
            orderString += $"\n{order.proteinType}";
        if (!string.IsNullOrEmpty(order.vegetableType))
            orderString += $"\n{order.vegetableType}";

        orderText.text = orderString;
    }

    IEnumerator DelayTimer()
    {
        yield return new WaitForSeconds(2f);

        timer = orderTimeLimit;
        timerRunning = true;
        oneStar.SetActive(false);
        twoStar.SetActive(false);
        threeStar.SetActive(false);
        UpdateTimerUI();

        brothChicken.SetActive(false);
        brothPork.SetActive(false);
        brothVeg.SetActive(false);

        chickenBtn.SetActive(true);
        porkBtn.SetActive(true);
        vegBtn.SetActive(true);
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
                OrderFailed();
            }
        }
    }

    void OrderFailed()
    {
        feedbackText.text = "Time's up! Customer left angry!";
        Invoke(nameof(GenerateNextOrder), 5f);
    }

    public void BoilBrothChicken() 
    { 
        brothChickenCover.SetActive(true); 
        StartCoroutine(BrothBoilingChicken()); 
        chickenBtn.SetActive(false); 
    }
    public IEnumerator BrothBoilingChicken() 
    { 
        yield return new WaitForSeconds(3f); 
        brothChicken.SetActive(true); 
        brothChickenCover.SetActive(false); 
    }
    public void BoilBrothPork() 
    { 
        brothPorkCover.SetActive(true); 
        StartCoroutine(BrothBoilingPork()); 
        porkBtn.SetActive(false); 
    }
    public IEnumerator BrothBoilingPork() 
    { yield return new WaitForSeconds(3f); 
        brothPork.SetActive(true); 
        brothPorkCover.SetActive(false); 
    }
    public void BoilBrothVeg() 
    { 
        brothVegCover.SetActive(true); 
        StartCoroutine(BrothBoilingVeg()); 
        vegBtn.SetActive(false); 
    }
    public IEnumerator BrothBoilingVeg() 
    { yield return new WaitForSeconds(3f); 
        brothVeg.SetActive(true); 
        brothVegCover.SetActive(false); 
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

    public void ServeOrder()
    {
        timerRunning = false;

        bowlGreen.SetActive(false);
        bowlBlue.SetActive(false);
        bowlPink.SetActive(false);

        bool correct = CheckOrder();

        if (correct)
        {
            int reward = CalculateReward(currentOrder);
            float timeTaken = Time.time - orderStartTime;
            int stars = CalculateStars(timeTaken);

            playerMoney += reward;
            feedbackText.text = $"Correct! Customer is happy! (+${reward}) x{stars} stars";
            UpdateMoneyUI();
        }
        else
        {
            feedbackText.text = "Wrong order! Customer is upset!";
        }

        Invoke(nameof(GenerateNextOrder), 5f);
    }

    private int CalculateReward(RamenOrder order)
    {
        int reward = 3; //base reward

        if (!string.IsNullOrEmpty(order.noodleType)) reward += 2;
        if (!string.IsNullOrEmpty(order.proteinType)) reward += 3;
        if (!string.IsNullOrEmpty(order.vegetableType)) reward += 2;

        return reward;
    }

    private int CalculateStars(float timeTaken)
    {
        if (timeTaken <= 20f)
        {
            oneStar.SetActive(true);
            twoStar.SetActive(true);
            threeStar.SetActive(true);
            return 3;
        }
        else if (timeTaken <= 45f)
        {
            oneStar.SetActive(true);
            twoStar.SetActive(true);
            return 2;
        }
        else if (timeTaken <= 55f)
        {
            oneStar.SetActive(true);
            return 1;
        }
        else return 0;
    }

    public bool CheckOrder()
    {
        bool isCorrect = true;

        if (playerBowl.bowlType != currentOrder.bowlType) isCorrect = false;
        if (playerBowl.brothType != currentOrder.brothType) isCorrect = false;
        if (!string.IsNullOrEmpty(currentOrder.noodleType) && playerBowl.noodleType != currentOrder.noodleType) isCorrect = false;
        if (!string.IsNullOrEmpty(currentOrder.proteinType) && playerBowl.proteinType != currentOrder.proteinType) isCorrect = false;
        if (!string.IsNullOrEmpty(currentOrder.vegetableType) && playerBowl.vegetableType != currentOrder.vegetableType) isCorrect = false;

        return isCorrect;
    }

    public void UpdateMoneyUI()
    {
        if (playerMoney < 0)
        {
            playerMoney = 0;
            EndGame();
        }

        moneyText.text = $"${playerMoney}";
    }

    public void EndGame()
    {
        feedbackText.text = "Game Over! You ran out of money.";

        foreach (DragDrop ingredient in FindObjectsOfType<DragDrop>())
        {
            ingredient.enabled = false;
        }

        timerRunning = false;
    }

    void UpdateTimerUI()
    {
        timerText.text = $"{Mathf.Ceil(timer)}s";
    }

    public void UpdateCurrentBowlText()
    {
        string display = "Last added to bowl:\n";

        if (!string.IsNullOrEmpty(playerBowl.bowlType)) display += $"{playerBowl.bowlType}\n";
        if (!string.IsNullOrEmpty(playerBowl.brothType)) display += $"{playerBowl.brothType}\n";
        if (!string.IsNullOrEmpty(playerBowl.noodleType)) display += $"{playerBowl.noodleType} Noodles\n";
        if (!string.IsNullOrEmpty(playerBowl.proteinType)) display += $"{playerBowl.proteinType}\n";
        if (!string.IsNullOrEmpty(playerBowl.vegetableType)) display += $"{playerBowl.vegetableType}\n";

        currentBowlText.text = display;
    }
}
