using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class FacePlayer :  MonoBehaviour
    {
        private void Update()
        {
            Vector3 direction = LevelManager._instance.player.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
        }
    }
}