# SpriteAnimator is a light-weight sprite animation library for Unity

SpriteAnimator will allow you to create simple sprite frame-by-frame animations in Unity. Animations are created as scriptable objects and played via the SpriteAnimator MonoBehaviour.

The intention of the SpriteAnimator is to have a very light-weight way to play simple sprite animations without the need for Unity's complex animation system.

Should work in all versions of Unity as it only uses basic C# language and Unity features.

## How to use SpriteAnimator

Include the following files in your project (in src folder):

	SpriteAnimation.cs
	SpriteAnimator.cs

## Create Animation

To create an animation, right-click a folder of your choosing in the Project Explorer, choose Create > SpriteAnimation

A Sprite Animation has the following properties:

- Title: A string that allows you to name your animation
- Frames: A collection of frames stored as an Array. Each frame holds a Sprite and a Duration. This allows you to give each frame it's own duration (in seconds)
- Looping: When ticked, the animation will continue looping
- Show First Frame At End: When ticked, the first frame of the animation will be shown when the animation ends.

## Play an Animation

Add a SpriteAnimator component to your GameObject. If the GameObject does not have a SpriteRenderer component on it, it will be added automatically.

The SpriteAnimator component has the following properties:

- Play On Awake: When ticked, the default animation will start playing when the GameObject is added to the scene
- Default Animation: A default SpriteAnimation that will always be referenced by the SpriteAnimator. If PlayOnAwake is ticked (true), then this animation will be played when the GameObject is added to the scene.

## Play an Animation from code

If you wish to play a SpriteAnimation from code, it can be done in the following way:

	[SerializeField]
	private SpriteAnimator animator;
	
	[SerializeField]
	private SpriteAnimation idleAnimation;
 	
	private void Start() {
		// Play the idle animation
		animator.Play(idleAnimation);
  		
    		// Play the idle animation starting at frame 2
    		animator.Play(idleAnimation, 2);
		
    		// Force the idle animation to play starting at frame 2
    		// By default, calling the Play method with the same SpriteAnimation that is already playing will be ignored.
    		// Using force, you can ensure the SpriteAnimation is always played no matter what.
    		animator.Play(idleAnimation, 2, true)
		
    		// Play the idle animation with a callback that will run once the animation ends. Callback will never be executed for looping animations though
    		animator.Play(idleAnimation, 2, false, () => {
      			Debug.Log($"Animation {animator.CurrentAnimation.title} finished");
    		});
  	}

The SpriteAnimator also has the following method:

- Stop: Stop an animation that is playing
