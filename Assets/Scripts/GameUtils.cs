using UnityEngine;


public static class gameUtils
{
  public static T GetRandomElement<T>(T[] array)
  {
    if (array == null || array.Length == 0)
    {
      return default;
    }

    int randomIndex = Random.Range(0, array.Length);

    return array[randomIndex];
  }
  public static void Shuffle<T>(T[] array)
  {
    for (int i = array.Length - 1; i > 0; i--)
    {
      int randomIndex = Random.Range(0, i + 1);

      T temp = array[i];
      array[i] = array[randomIndex];
      array[randomIndex] = temp;
    }
  }

}