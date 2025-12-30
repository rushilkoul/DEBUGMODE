using UnityEngine;

public class LevelEndVisual : MonoBehaviour
{
    public void TriggerLevelEndVisual()
    {
        gameObject.GetComponent<Renderer>().enabled = true;
        Vector3 targetScale = new Vector3(transform.localScale.x, 5f, transform.localScale.z);
        StartCoroutine(ScaleOverTime(targetScale, 1f));
        
    }
    private System.Collections.IEnumerator ScaleOverTime(Vector3 targetScale, float duration)
    {
        Vector3 initialScale = transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(initialScale, targetScale, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = targetScale;
    }
}
