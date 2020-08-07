using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button start;
    public InputField X;
    public InputField Y;
    public InputField rule;
    public Toggle random;
    public Toggle step;

    void Start()
    {
        start.onClick.AddListener(GenerateTile);
    }

    void GenerateTile()
    {
        GetComponent<Automaton>().SetVariables(step.isOn, random.isOn, int.Parse(X.text), int.Parse(Y.text), int.Parse(rule.text));
    }
}
