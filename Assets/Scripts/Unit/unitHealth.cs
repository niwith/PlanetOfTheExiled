using UnityEngine;
using System.Collections;

public class unitHealth : MonoBehaviour {

    public float curHealth = 100;
    public float maxHealth = 100;

    public void ApplyDamage (float damage)
    {
        curHealth -= damage;
        if(curHealth < 0)
        {
            GameObject.Destroy(gameObject);
        }
    }

    public void ApplyHealing(float healing)
    {
        curHealth += healing;
        if(curHealth < maxHealth)
        {
            curHealth = maxHealth;
        }
    }
}
