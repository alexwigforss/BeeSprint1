using UnityEngine;

public class HideCursor : MonoBehaviour
{
	bool isMouseHidden = true;
	void Start()
	{
		// Hide the cursor
		Cursor.visible = false;
		// Optionally lock the cursor to the center of the screen
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
	{
		// Press Escape to unlock and show the cursor
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (isMouseHidden)
			{
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
				isMouseHidden = false;
			}
            else
            {
				Cursor.visible = false;
				Cursor.lockState = CursorLockMode.Locked;
				isMouseHidden = true;
			}
		}
	}
}