# The super shader challenge party

Heyo! I'm Noto 🤖 and I like retro! But my issue is that people don't do retro anymore... times have changed...\
But you can help me making retro great again using shaders. I have a few things in mind, maybe you'll know how to make them?

> For this challenge, you get to write glsl (shader) code to make/transform/generate/conceive/reproduce images.
> You'll be using [DSD](https://github.com/Akahara/ShadersWorkspace/tree/master/DynamicShaderDisplay), and the IDE of your choice - VSCode+hlsl extension works well -
> DSD is a simple program that runs shaders and allows you to edit uniforms (shader variables) at runtime. It makes use of the comments after `uniform` declaration, avoid changing those.
> - Noto is a robot but he's a bit selfish (*don't tell him*), don't use any kind of AI! No chatgpt or Copilot. Feel free to wander around on the internet though
> - You'll be given commands to run with DSD, run "`dsd run ...`" with `extern\jdk-27-windows\bin\java.exe -jar extern\dsd.jar -- run ...` or make yourself an alias/shortcut. You can always find more info on commands by running `dsd ? <command>` or `dsd --help <command>`. **Pay attention to command lines, sometimes you'll execute .json files instead of .fs**

## Quick guide on how to write shaders
> *Skip if you already know what shaders are*

Shaders are your usual programs, except that they run on gpu. We'll keep it simple by working on *fragment shaders* mostly, the idea is that the main function is executed once for each pixel, and with one color output per pixel.

You get access to uniforms (global variables) and glsl built-ins too, so if you wanted to make the left half green you would write:
```glsl
void main() {
    color = gl_FragCoord.x < u_resolution.x/2 ? vec4(0,0,1,1) : vec4(0,0,0,1);
}
```
Where `gl_FragCoord` is the position of the pixel and `u_resolution` the size of the screen.

The challenges will rely quite a bit on math, in the "really understand simple math" sense rather than "[compute the distance to an isosurface that is defined through an implicit scalar field f(x)](https://iquilezles.org/articles/distance/)".

You can find some useful glsl functions here:
> https://www.desmos.com/calculator/le4fddxtws

Most of the code templates are documented (**read comments!**) a bit and solving challenges will make others easier, do skip and come back to ones you don't yet know how to solve. Now, good luck!

## Noto's memories (45)

🤖 I have a few images on my 4096Kb usb stick, but I can't remember how I made them, can you write code to reproduce them?

> You get a template code, and must try to reproduce the image in `examples/<challenge>.png`. You get points for each step you succeed to do, don't spend too much time on a single challenge if you get stuck, and **don't try to reproduce the image exactly!** Scoring will be quite lenient.
> For each challenge, run `dsd run <challenge>.fs` and write directly in the .fs file
> To score, an image will be generated from your shader (see [make_screenshots.bat](./eval/make_screenshots.bat)). You cannot use extra files, but you're free to do anything else.

1. **Maze pattern** (8)

Do you remember [this pattern](https://trixter.oldskool.org/2012/12/17/maze-generation-in-thirteen-bytes/#jp-carousel-839) made of slash and backslashes?
Maybe one of the oldest examples of [generative art](https://www.reddit.com/r/generative/).
> Challenge 3 can help you understand how to pixelize your image if needed
- (4) the pattern is random
- (2) lines are either antialised (=blurred, =no hard edges), or heavily pixelated
- (1) use a different color for slashes and background, pick an old color scheme
- (1) you can move around the pattern using your mouse

2. **Ascii art** (6)
   
I like *old* ascii art, where we replaced pixels by small or big characters. But a touch of color is nice too.
> Challenge 3 can help you understand how to pixelize your image if needed
- (4) making ascii art!
- (1) using smaller characters for darker spots
- (1) coloring the symbols

3. **Bayer pattern** (7)
   
This image has too much color and noise! It would be nicer if it were pixelated. If you don't know exactly what Bayer patterns are, check out the [wiki](https://en.wikipedia.org/wiki/Ordered_dithering)
- (1) make the image monochrome
- (4) applying the Bayer pattern
- (1) make pixels square
- (1) the image isn't stretched (google "letterbox aspect ratio")

4. **A nice sun** (5)
   
Remember those posters? It shouldn't be too hard to make using a bit of math
> Run with `dsd run 4_sun.fs -w 500 -h 888`
- (2) make a colored circle sun
- (3) make colored sun beams

5. **Mandlebrot set** (6)
   
This one has been made on very old hardware, I wonder how it will go with high res graphics? Here's the [wiki](https://en.wikipedia.org/wiki/Mandelbrot_set), the idea is to repeat `z<-f(z)` and detect *when* z diverges (eg. `length(z)>=2` here)
> Run with `dsd run "5_mandlebrot.fs" -w 888 -h 500`
- (4) the set is visible
- (1) apply color (lighter when it takes longer for z to diverge)
- (1) be able to zoom in around u_zoomTarget

6. **Stars** (8)
   
Let's make one of those old screen savers, it's been a while since I've seen one.
- (4) the stars glow smoothly
- (3) the glow does not clip (you'll see it clearly if the glow is large)
- (1) size and colors are random per star

7. **Sierpinsky carpet** (5)
   
While you're at it, mind doing this one too? It's always nice having a nice carpet.
- (4) the carpet is square
- (1) make the edges smooth

## Golfing (20)

I don't have a lot of memory left, before my mind melts away, could you hack my mind real quick to make some space?
> Golfing is making a piece of code the smallest it can be. It's extra easy for shader code because we usually use 1-letter names for everything anyway :p
> You get `a_golfing.fs`, you can do anything to it and score depending on the number of characters left. The output of the shader mustn't change too much!
- (0) points at 2500 characters, the full (20) at 800 characters or less
- I'll deduct points for each thing I can tell isn't the same as the original

## Game of life (20)

I want to play now, won't you make the [game of life](https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life) for me? It can be done quite easily with shaders.
> Run with `dsd run b_gameoflife.json -rte -w 500 -h 500`.
- (10) the game of life works
- (5) the update speed can be adjusted with a uniform
- (5) the grid wraps around

## Snake? (35)

Ok, so I had forgotten that the game of life is a 0 player game, but I'm 1. Could you make snake instead? It's definitely possible to make playable games too! but it's quite a bit harder...
> This time you'll use compute shaders in addition to fragment shaders, those ones are executed on the gpu but are not meant to draw anything. Run with `dsd run c_snake.json -rte`.
- (3) draw a grid
- (5) show the snake in the cells it's in
- (7) make the snake move toward the cursor
- (5) spawn a fruit, and respawn it when it's eaten (don't bother spawning it away from the snake)
- (5) make the snake grow when it eats a fruit
- (5) reset the game when the snake hits itself
- (2) add googly eyes o_O
- (3) add a proper background

## Path tracing (40)

Time to make bigger things, I made a sphere for you, can you make it a solar system? You might need to combine 2D and 3D effects for that one
- (10) make multiple planets orbiting a sun
- (5) color the planets
- (5) make the sun glow
- (5) add stars in the background
- (5) make an asteroid field around a planet
- (5) make the image smooth by applying a [gaussian blur](https://en.wikipedia.org/wiki/Kernel_(image_processing)), all in the same shader
- (5) add rings for the planet trajectories

## A retro feel (30)

I bought a new computer recently, but it's not booting, can you make it run?
> Run with `dsd run e_retro.json`
- (10) take any of the previous shaders, and make it run on the screen
- (5) add color and a background, feel free to modify the scene (json) file
- (12) apply post effects on the screen, 4 points each: vignette, chromatic aberration, static
- (3) apply another gaussian blur to make the image smooth, but in `e_retro_gaussian.fs`

## Thanks!

Thank you for doing all that! I won't remember your name but make sure you remember me as Noto🤖 the robot with a super cool computer running super cool games!

### Test & submit

First, don't forget that the uniform values you set while the app was running are only saved on *your* computer! Make sure to copy them in the shader files, you can press the "Copy" button in the app and press "Reset" after to test.

To submit your results, `cd` into `eval/` and run `make_submit.ps1/.sh`. Upload the generated `submit.zip` [here](https://forms.gle/BN4zFAeHVdGD9Hbi6).
Remember that you mustn't have created new files!

To see exactly what you'll be judged on, `cd` into `eval/` and run `make_screenshots.ps1/.sh`. Only the first few seconds of each videos are evaluated.
