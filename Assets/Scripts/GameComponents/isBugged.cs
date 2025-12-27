using UnityEngine;

public class isBugged : MonoBehaviour
{
  [SerializeField] private Transform buggedIndicatorVisual;
  [SerializeField] private BugArrSO bugArrSO;
  [SerializeField] private GameObject bugListGO;
  [SerializeField] private int maxBugs = 2;
  [SerializeField, Range(0, 2)] private float scaleFactor = 1f;

  private GameManager GameManagerInstance;
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    GameManagerInstance = GameManager.Instance;
    int maxBugsIter = maxBugs;
    int totalNoBugs = bugListGO.transform.childCount;

    if (maxBugs < 1 || maxBugs > totalNoBugs)
    {
      maxBugsIter = totalNoBugs;
    }

    Transform[] bugArr = new Transform[totalNoBugs];

    for (int i = 0; i < totalNoBugs; i++)
    {
      Transform bugTransform = bugListGO.transform.GetChild(i);
      bugTransform.gameObject.SetActive(false);

      bugArr[i] = bugTransform;
    }

    gameUtils.Shuffle(bugArr);

    for (int i = 0; i < maxBugsIter; i++)
    {
      Transform bugTransform = bugArr[i];
      bugTransform.gameObject.SetActive(true);

      foreach (Transform child in bugTransform)
      {
        Destroy(child.gameObject);
      }

      Transform randomBugTransform = gameUtils.GetRandomElement(bugArrSO.bugTransformArr);
      GameObject randomBugGO = randomBugTransform.gameObject;

      Instantiate(randomBugGO, bugTransform);
      bugTransform.localScale *= scaleFactor;

      Debug.Log(randomBugTransform);
    }
  }


  // Update is called once per frame
  void Update()
  {
    if (GameManagerInstance.getIsBugged(transform))
    {
      buggedIndicatorVisual.gameObject.SetActive(true);
    }
    else
    {
      buggedIndicatorVisual.gameObject.SetActive(false);
    }
  }
}
