using UnityEngine;
using UnityEngine.UI;

public class Point : MonoBehaviour
{
    [SerializeField] private int weight;
    [SerializeField] private Button _button;
    [SerializeField] private Text text;
    private bool isPositive;

    public void RandomWeight()
    {
        var rnd = Random.Range(100, 400);
        weight = Mathf.RoundToInt(rnd/100)*10;
    }
}
