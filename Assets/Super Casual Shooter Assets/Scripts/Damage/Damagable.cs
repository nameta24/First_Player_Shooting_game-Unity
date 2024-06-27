using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider))]
public class Damagable : MonoBehaviour
{
    [SerializeField]
    private int max_health; // the maximum health

    private float current_health; // the current health of the damagable

    [SerializeField]
    private UnityEvent OnDeath; // what happens when health finishes

    [SerializeField]
    [Range(0, 5)]
    private float still_show_damage_effect_for = 0; // even when damage is no longer received. how long should we wait before the damage effect stops.
    public AudioClip Bleed;
    public float volume = 0.2f;                                   // an example of this is how long should the player bleed, even after it has stopped taking damage.

    private float show_damage_effect_counter; // this would help us know when to stop showing damage

    private int meshDestroyedCount = 0; // count the number of times the mesh is destroyed

    // Use this for initialization
    void Start()
    {
        current_health = max_health;
    }

    // Update is called once per frame
    void Update()
    {
        if (current_health < 1)
        {

            OnDeath.Invoke();
            
        }

        handleDamageEffect();
    }

    private void handleDamageEffect()
    {
        if (show_damage_effect_counter > 0)
        {
            show_damage_effect_counter -= Time.deltaTime;
        }
    }

    public void takeDamage(float damage)
    {

        current_health -= damage;
       
        show_damage_effect_counter = still_show_damage_effect_for;
        AudioSource.PlayClipAtPoint(Bleed, transform.position, volume);
        if (current_health <= 0)
        {
            meshDestroyedCount++;
            Debug.Log("Mesh Destroyed");
            /*if (meshDestroyedCount >= 1)
            {
                // End the game (you can replace this with your game-ending logic)
                Debug.Log("Game Over - Mesh Destroyed 7 times!");

                // For example, you might want to call a function to end the game.
                nextLevel();
            }*/
        }
    }

    public void reverseDamage(int health)
    {
        current_health += health;

        current_health = Mathf.Clamp(current_health, 0, max_health);
    }

    public void destroyMesh()
    {
        Destroy(gameObject);
    }

    /*private void nextLevel()
    {
        // Implement your game-ending logic here
        // For example, you might want to show a game-over screen or return to the main menu.
        // You can use Application.Quit() to exit the application.
        SceneManager.LoadScene("Instructions");
    }*/

    public int getMaxHealth()
    {
        return max_health;
    }

    public float getCurrentHealth()
    {
        return current_health;
    }

    public float getCurrentDamageEffectCounter()
    {
        return show_damage_effect_counter;
    }

    public float getMaxDamageEffectCounter()
    {
        return still_show_damage_effect_for;
    }
}
