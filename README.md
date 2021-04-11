# Buzz, Buzz I'm a Bee (March 2021)
This is a simple game I made for the [Great Spring Game Jam](https://itch.io/jam/great-spring-game-jam-2021).

TODO YT Video

![Apr-11-2021 12-30-16](https://user-images.githubusercontent.com/41971262/114300841-5bd5c000-9ac2-11eb-8db0-54d7021bd4af.gif)

![Apr-11-2021 12-30-34](https://user-images.githubusercontent.com/41971262/114300847-6001dd80-9ac2-11eb-91ec-e1d989629d41.gif)

![Apr-11-2021 12-31-50](https://user-images.githubusercontent.com/41971262/114300854-642dfb00-9ac2-11eb-9528-23a4446ed599.gif)

![Screenshot 2021-03-20 at 03 38 47](https://user-images.githubusercontent.com/41971262/114300859-6d1ecc80-9ac2-11eb-862b-00a5a0b94d7d.png)

![Screenshot 2021-03-20 at 03 39 59](https://user-images.githubusercontent.com/41971262/114300866-73ad4400-9ac2-11eb-901b-4707c3f91725.png)

![Screenshot 2021-03-20 at 03 40 12](https://user-images.githubusercontent.com/41971262/114300870-77d96180-9ac2-11eb-99d9-93a3da1800c2.png)

![Screenshot 2021-03-20 at 03 41 00](https://user-images.githubusercontent.com/41971262/114300878-7f990600-9ac2-11eb-8d58-64bf323b7986.png)

 ![Screenshot 2021-03-20 at 03 44 04](https://user-images.githubusercontent.com/41971262/114300886-8758aa80-9ac2-11eb-890b-2eef1747a03d.png)

# Creative challenges
During the course of development (which spanned about a week), I pivoted multiple times. Initially I wanted to make a game where a bee bounced around on flowers around a planet. After trying this out, I realized that the player didn't have enough influence over what was happening in the game. It became too random and too much of a game of chance. I felt that you could simply just let the game play itself and might fare better than if you actually tried to play. I didn't like this so I decided to change it to a game where player had to fly around a planet while being hunted down by angry bees. This mechanic also proved a bit dull so I changed it a bit again where the player just had to pollinate all of the flowers as fast as possibe. I would spawn fewer, what would now be wasps, and additionally also spawned friendly little bugs to create that spring feeling.

# Technical challenges
On the technical side, the most challening aspect was the sphere. I underestimated how difficult it would be to figure out how to move things around the sphere. I have even more respect for the developers of Mario Galaxy after having built this simple game. The movement of the player's bee around the sphere was not trivial and the movement of the enemies and other bugs around the sphere was tricky as well. I had more exposure to Quaternions than I was comfortable having but I learned a great deal about them. I still can't tell you how they work under the hood but I guess the light-bulb moment I had was when I realized this: when dealing with circles (2D space), the unit circle and all the trigonometric tools around it are your friends. When you deal with spheres (3D space), Quaternions are your very complicated, confusing friends. Luckily Unity provides powerful APIs to work with Quaternions so I got by by mainly understanding how to use Quaternions instead of how they work in detail. I will not go into the nitty gritty of how I implemented everything but I would like to share how I ended up implementing the movement of the NPCs. I did not come up with this on my own. I found a tip for this on, I believe, StackOverflow. But essentially it works as follows:

![Bee_Game_NPCs](https://user-images.githubusercontent.com/41971262/114300892-92abd600-9ac2-11eb-9089-dd02bf3eafd9.png)

Essentially the NPC consisted of an anchor that was located at the center of the planet. The NPC that would appear on the surface was the anchor's child object. When I wanted the NPC on the surface to move to a given point on the surface, the anchor would just have to look at that target point and the NPC on the surface would then move to it. Think of the NPC on the surface being attached to an arm that would swivel to a given location when the anchor turned to look at it. I then played around with various interpolation algorithms to get the speed ove the swiveling right.

# Open issues
Since this was a submission for a game jam, I did not invest any more time into it after I submitted it. I was therefore not able to address all of the issues. The main issues this game still suffers from are problems related to the camera and to the player's orientation on the surface of the sphere. The camera is pretty jittery, especially when changing directions. Something is also still off with the direction of the player. It seems as if the player sometimes gets into the situation where he/she doesn't fly around the planet in a "straight line" but somehow constantly kind of turns. To me this is noticeable but was still subtle enough that I just left it in for the submission. Also, I ran out of time and nerves ;).
