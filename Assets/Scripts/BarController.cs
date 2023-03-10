using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour
{
    [SerializeField] private Slider food, water, happiness;

    [SerializeField] private TMP_Text _text;
    public int WaterValue => (int)water.value;
    public int FoodValue => (int)food.value;
    public int HappinessValue => (int)happiness.value;

    public void StartResetSlider()
    {
        
        food.value = StartRandomValue();
        water.value = StartRandomValue();
        happiness.value = StartRandomValue();
        while (water.value == food.value || water.value == happiness.value)
        {
            water.value = StartRandomValue();
        }

        while (happiness.value == food.value || happiness.value == water.value)
        {
            happiness.value = StartRandomValue();
        }
 
    }

    private int StartRandomValue()
    {
        var rnd = Random.Range(-31, 31);
        return Mathf.RoundToInt(rnd/10)*10;
    }

    public void SetFoodValue(int value)
    {
        food.value += value;
        Debug.Log($"Food = {food.value}");
    }
    public void SetWaterValue(int value)
    {
        water.value += value;
        Debug.Log($"Water = {water.value}");
    }
    public void SetHappinessValue(int value)
    {
        happiness.value += value;
        Debug.Log($"Happiness = {happiness.value}");
    }

    public void SetNumberMoves(int value)
    {
        _text.text = $"Осталось ходов: {value} ";
    }
}
