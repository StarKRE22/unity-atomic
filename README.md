> [!IMPORTANT]
> For a more better Unity development experience, I recommend using the [Odin Inspector](https://assetstore.unity.com/packages/tools/utilities/odin-inspector-and-serializer-89041) asset.


Unity Atomic - Atomic Extensions for Unity
===
Created by Igor Gulkin (@StarKRE22)

What is Unity Atomic?
---
The atomic approach is an object-oriented approach for game object development, representing a game object as a composition of atomic data and functions. An atomic object provides a common interface for interacting with the properties and functions of various entities, and also has the ability to change the structure of an object at runtime.

Release Notes, see [unity-atomic/releases](https://github.com/StarKRE22/unity-atomic/releases)

Getting started
---

Create Character using Atomic Approach

```csharp

[Is("Damagable")]
public sealed class Character : AtomicEntity //derived from MonoBehaviour
{
    [Get("Health")]
    [SerializeField]
    private AtomicVariable<int> health;

    [Get("TakeDamage")]
    [SerializeField]
    private AtomicAction<int> takeDamageAction;

    [Get("DeathEvent")]
    [SerializeField]
    private AtomicEvent deathEvent;
    
    private void Awake()
    {
        //Declare damage action
        this.takeDamageAction.Compose(damage =>
        {
            this.health.Value -= damage;
        });

        //Declare death mechanics
        this.health.Subscribe(value =>
        {
            if (value <= 0) this.deathEvent.Invoke();
        });
    }
}
```

Interacting with Character
---

```csharp

IAtomicEntity character = gameObject.GetComponent<IAtomicEntity>();


//Check if damagable object 
bool isDamagable = character.Is("Damagable");

//Get health
int health = character.GetValue<int>("Health").Value;

//Set health
character.SetValue("Health", 5);

//Deal damage
character.CallAction("TakeDamage", 2);

//Subscribe on death
character.ListenEvent("DeathEvent", () => Debug.Log("I'm dead!"));
```
