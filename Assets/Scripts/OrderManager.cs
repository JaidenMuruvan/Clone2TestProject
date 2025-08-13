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
    [Header("Possible Ingredients")]
    public string[] bowlOptions = { "blue", "green", "pink" };
    public string[] noodleOptions = { "chukamen", "kuyoumen", "torimen" };
    public string[] brothOptions = { "chicken", "vegetable", "pork" };
    public string[] proteinOptions = { "chicken", "egg", "tofu" };
    public string[] vegetableOptions = { "bokchoy", "mushrooms", "onions" };

    [Header("UI References")]
    public Text orderText;
    public Text feedbackText;

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
        currentOrder.bowlType = bowlOptions[Random.Range(0, bowlOptions.Length)];
        currentOrder.noodleType = noodleOptions[Random.Range(0, noodleOptions.Length)];
        currentOrder.brothType = brothOptions[Random.Range(0, brothOptions.Length)];
        currentOrder.proteinType = proteinOptions[Random.Range(0, proteinOptions.Length)];
        currentOrder.vegetableType = vegetableOptions[Random.Range(0, vegetableOptions.Length)];

        orderText.text = $"Customer Order:\n" +
                         $"{currentOrder.bowlType} bowl\n" +
                         $"{currentOrder.noodleType} noodles\n" +
                         $"{currentOrder.brothType} broth\n" +
                         $"{currentOrder.proteinType}\n" +
                         $"{currentOrder.vegetableType}";

        feedbackText.text = "";
        playerBowl = new RamenOrder();
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

    public void ServeOrder()
    {
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

        Invoke(nameof(GenerateOrder), 2f); // Wait 2 seconds, then new order
    }
}
