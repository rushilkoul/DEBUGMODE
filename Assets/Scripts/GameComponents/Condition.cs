using UnityEngine;
using TMPro;

public class Condition : MonoBehaviour
{
  [SerializeField] private string comp1;
  [SerializeField] private string comp2;
  [SerializeField] private string opr;
  [SerializeField] private TMP_Text textGui;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    setText(comp1 + opr + comp2);
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void setText(string txt)
  {
    textGui.text = txt;
  }
}
