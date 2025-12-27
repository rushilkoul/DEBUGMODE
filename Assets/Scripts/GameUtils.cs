using UnityEngine;


public static class gameUtils
{
  public static T GetRandomElement<T>(T[] array)
  {
    if (array == null || array.Length == 0)
    {
      return default; // Return default value for the type
    }

    // Generate a random index between 0 (inclusive) and the array length (exclusive)
    int randomIndex = Random.Range(0, array.Length);

    // Return the element at the random index
    return array[randomIndex];
  }
  public static void Shuffle<T>(T[] array)
  {
    // Start from the end of the array and work backwards
    for (int i = array.Length - 1; i > 0; i--)
    {
      // Pick a random index from the start up to the current position (inclusive of min, exclusive of max)
      int randomIndex = Random.Range(0, i + 1);

      // Swap the element at the current index with the element at the random index
      T temp = array[i];
      array[i] = array[randomIndex];
      array[randomIndex] = temp;
    }
  }

}