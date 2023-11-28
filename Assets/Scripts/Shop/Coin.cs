using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    public Image targetImage; // Assign this in the inspector or find it via GetComponent
    public Sprite[] sprites; // Array of sprites to cycle through
    public float changeInterval = 1f; // Time in seconds between sprite changes

    private void Start()
    {
        StartCoroutine(CycleSprites());
    }

    private IEnumerator CycleSprites()
    {
        int index = 0;

        while (true)
        {
            targetImage.sprite = sprites[index];
            index = (index + 1) % sprites.Length; // Loop back to the first sprite after reaching the end
            yield return new WaitForSeconds(changeInterval);
        }
    }
}
