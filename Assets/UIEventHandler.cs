using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIEventHandler : MonoBehaviour, IPointerClickHandler
{
	public void OnPointerClick(PointerEventData eventData)
	{
		GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;
		Debug.Log("UI Element Clicked: "
			+ eventData.pointerCurrentRaycast.gameObject.name);
		Debug.Log("");
		// Add your custom logic here
		Image clickedImage = clickedObject.GetComponent<Image>();
		if (clickedImage != null)
		{
			Sprite clickedSprite = clickedImage.sprite;
			Debug.Log("Clicked Sprite: " + clickedSprite.name);
		}
		else
		{
			Debug.Log("No Image component found on the clicked object.");
		}
	}
}
