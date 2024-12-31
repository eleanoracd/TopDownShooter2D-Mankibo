using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{

    private void OnEnable()
    {
        InputManager.Instance.OnShoot += HandleShoot;
    }

    private void OnDisable()
    {
        InputManager.Instance.OnShoot -= HandleShoot;
    }

    private void Update()
    {
        HandleLookAt();
    }

    private void HandleLookAt()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(InputManager.Instance.MousePosition);
        mouseWorldPosition.z = 0;

        Vector2 lookDirection = (mouseWorldPosition - transform.position).normalized;

        float angle = (Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg) + 90 ;
        transform.rotation = Quaternion.Euler(0, 0 , angle);

    }

    private void HandleShoot()
    {
        Debug.Log("Shoot!");
    }
}
