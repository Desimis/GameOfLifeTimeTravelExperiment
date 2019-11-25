# Time Travel In Conway's Game of Life

Disclaimer: I am a programmer not a scientist, all the conjecture, statements and observations are purely related to this simulation of the game of life with time travel, and should not be interpreted as being scientific fact in the real world.

I was reading The Quantum Garden (Book 2) by Derek Künsken the other day, and without spoiling the book(you should read it it's amazing), I thought to myself: "can you run a neural network across time? can a neural network be created where some of the neurons can travel back in time?" so, naturally, I thought that if we used a simplified neural network like the kind we see all over the place nowadays that maybe we can add time travel and simulate it! While this is possible, it is also very complex; training a neural network with time travel does not sound fun. So, I thought to myself how I could start: by first studying time travel itself. Naturally, I thought "what if I could make a simplified model of reality and simulate time travel in that?" and then game of life came to mind; only this time with time travel.

# Modeling Time

There are a few ways of thinking about time as a whole, and when I say whole I mean the collective state not the instantial state, let's take a deeper look at what that means. Although this fact is a bit simplified it is generally accepted that we are 3D in nature we have Width, Height and Depth or in other words 3 axis X, Y, Z. But this is not the only dimension we exist in we also exist in Time.
If we think of the X and Y axis being a collection of one dimensional lines stacked side by side to form a 2D plane and X Y Z being a collection of planes stacked on top of one another to form a volume(like paper in a stack forming a block), then we can think of time as a block(frame) side by side, every block storing the state of the universe at every single instance of time. Time is the root of all change without time no state change can occur.

When I spoke of the model of time from an instantial perspective, I was referring to what time is from the perspective of a single instance/frame, from this perspective(the one we think from) time has a past present and future, but there is another perspective of thinking of time has a whole, this means not choosing a point on the timeline, but instead thinking of time as an entire range, in the same way you might think of a spatial dimension, think of a 1km stretch of road, from a whole perspective you interpret it from this point to that point is 1km, ie. a whole, where the instantial perspective is pick some point on the road and go stand there, define the whole road as some meters behind me and Some meters in front of me from this perspective the road is always defined around your perspective.

Applying this to our experiment we need to decide what model of time to use, to implement the instantial model we need to use a single thread, stepping one frame at a time. To implement the whole model we need to implement multithreading so we can step every frame at once per cycle, you might be thinking well the whole model is better, and on the surface this seems true, but I ended up implementing the single instance model to understand why, let's look at the consequences of the whole model.
First, let's think about change or causality, specifically if we did change the past how long would it take for the change to propagate to the future, and the answer might surprise you, to do this we need to determine the speed of causality. This might seem a bit abstract/esoteric at first, but to answer this we need to determine what the speed of light is, this might seem unrelated at first but bear with me. What sets the speed limit why can light not move any faster, well light can move only as fast as change can allow it to move, think of a computer game where the speed of the game is directly related to how fast your computer is, if your computer is slow the game will lag if it's fast the game won't, so relative to your perspective(of time) the rate of change in the game is related to the speed of the computer. Apply this to light we can say how far can light move per minimum instance of time(frame of time), if we have some absolute measurement of time to reference against, you could say if the tick rate of physics moved faster everything in would happen faster(relative to our absolute clock), therefore the speed of change or rather the speed of causality is directly related to the speed of light, so if change propagates at the speed of light, we can say that if we changed the past, the cause and effect of that would only move forward at the speed of light. Calculating the speed of light in conway's game of life is pointless seeing as there is no light, but when referring to the speed of light, i'm using it more as an abstract concept of the speed of change rather than an actual measurement.

The big problem, and why I did not use this model, is that if you went back in time and change it the future does not magically stop moving forward, so if the future is moving forward at the speed of light and the changes we made to the past with our time machine, is moving forward at the speed of light, the two will never actually meet up(two objects moving at the same speed can't catch one another), and because the current frame of time is only calculated based on the previous frame of time, the frame before the immediate previous frame has no influence on the current frame, there for multiple versions of the same reality can exist in the same single timeline riding on waves of change propagation that will never actually touch.

Returning to why the single instance model, if we use the whole model the future would not change, instead a new future is created that lives in the unused past, although it would be possible to just look at the new wave front to see the results, it's much simpler and easier to implement the single instance model to study time travel, because it's all the same from the perspective of a single being.

# Implementation
The time travel mechanism itself is just an extra rule added onto the normal rules(B3/S23), the extra rule states if a cell has six neighbors it will jump back 5 frames in time.


# Simulation Results
### Deleting The universe

The first time I ran a simulation i delete the universe accidently, computers don't have unlimited ram so naturally I started with a frame zero in time and allocate new frames of time as they are needed, the first time I made a time machine I made it in frame zero, all excited I hit the run button and it crashed with the classic IndexOutOfBound Exception we all love and hate, still being super excited i realized im only allocating new frames for the future i added some code that allocates new frames in the past if you try to jump back before time existed. This resulted in the universe deleting it self, think about it every frame is calculated based on the previous frame of time and so on and so on, so if we add a blank frame to the start of time, its empty the next frame is then calculated based on that frame and the next and so on and so in the end the blank frame propagated over the entire system deleting everything.

To fix this problem i disabled time travel in frame 0-4 so things can jump back to before that time. I think a more elegant way would have been to calculate the past frame based on one frame in the future, but the rules of the game of life is not reversible you can't run them backwards, a future experiment that could be interesting is designing a simplified model of thermodynamics with a set of rules that is reversible for game of life stuff, and to then try this again.

### Changing the Future

And Finally lets actually change the future with time travel Here is our starting state(with time travel disabled):

![Start State Time Travel Disabled](/img/0D.png)


And the Ending State(with time travel disabled):

![End State Time Travel Disabled](/img/100D.png)

No for the same thing but with time travel enabled
Starting State:

![Start State Time Travel Enabled](/img/0E.png)

Ending State:

![Start State Time Travel Enabled](/img/100E.png)



As we can see we have totally different results il edit the frames of both together and allow you to take a look at it and see what happen for yourself (Hint the first time travel happens on frame 21):

Time Travel Disabled:

![Time Travel Disabled](/img/d.png)

Time Travel Enabled:

![Time Travel Disabled](/img/e.png)
