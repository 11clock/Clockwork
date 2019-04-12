# Clockwork
A game engine built on MonoGame with a focus on ease-of-use for simple pixel art games.

Its structure is based on a hybrid of GameMaker and HaxeFlixel, taking the best of both to create an ideal pixel art workflow.

## Status

Works, but barebones and not yet useful.

## Features

### Current

- Core Game Loop
- Virtual Resolution (consistent ingame resolution across all screens)
- Content Libraries
- Scenes & Objects
- Sprites & Images, Drawing
- Sprite Animations
- Keyboard & Mouse Input
- Delta Time (fixed update loop for consistent behavior)
- Mouse Detection On Game Objects
- RNG
- Alarm Clock (delayed method calls)

### Planned

- Collision Masks & Checking
- Cameras
- Drawing Text
- Scene Enhancements, Groups
- Tile Maps, Ogmo and Tiled Support
- Aseprite Metadata As Resource (converting Aseprite metadata into sprites and animations)
- Animation: Individual Frame Delays (have some frames last longer than others without needing to register them multiple times)
- Animation: Callbacks
- Basic Audio
- More Game Control (fullscreen, changing window size post-launch, stretched mode)
- Gamepad Input
- More Collision Shapes (circles, polygons)

### Under Consideration

- Multi-Frame Collision Masks, Attaching Them To Sprites
- Sprite As Resource, Editor
- Animation As Resource, Editor
- Nested Objects (objects as components)
- Content Library Management (group content into different libraries and load and unload them as needed)
- Correcting Imperfect Pixel Scaling
- Custom-Made Content Pipeline

### Not Planned

- FMOD: Proprietary audio library, integrate it into your project yourself if you want to use it.
- Physics: This engine is focused towards pixel art, where advanced physics usually aren't needed. I don't recommend using a physics engine if your game doesn't require it. If you really do need it, integrate an existing library on your own.
- Level Editor: Level editing needs can vary heavily from game to game (or not require a level editor at all), and many advanced GameMaker users ended up just making their own editors. I will compensate with support for a couple of popular 3rd party tilemap editors, though (Ogmo and Tiled), and tools to help you easily integrate your own.
- 3D

## Why?

I grew up with GameMaker and loved working with it for the 10 or so years that I used it, but I have become increasingy frustrated by its limitations, particularly with its built-in scripting language. Also, because GameMaker is a sandboxed environment, it lends itself to be difficult to extend. I desire the flexibility of a more robust, feature complete language like C#, and running my own engine gives me the ability to extend it in whatever way I want.

I have tried other engines like Godot, Unity, and HaxeFlixel, but either I run into core issues such as frame stuttering, or the engine itself doesn't have an ideal workflow for pixel art like I had with GameMaker. I will use such engines for other types of games, but for pixel art I need a better solution.

Unfortunately, not everything from GameMaker translates well to C#, and there were parts of the engine's architecture that I did not like working with. To alleviate this, I will be taking some inspiration from the HaxeFlixel game engine as well.
