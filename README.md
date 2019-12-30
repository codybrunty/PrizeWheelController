# PrizeWheelController

The PrizeWheelController can be used for any pre determined prize wheel type mechanic.
For example just attach this script to a 2d prize wheel image and adjust the prizeWheelNumbers 
in the script to match your wheel. 

You can have the script randomly pick a prize for the spin destination or feed the script 
a predetermined prize that you want it to land on. And it will spin to that number.

There are other factors that you can adjust to make wheels spin more your own like the duration the spin
lasts for and the number of times you want the wheel to spin around before landing on your desired 
section.

To handle the ease out of the spin animation I attached a unity AnimationCurve to the spin coroutine 
so the spin will start fast and then come to a nice slow end. 

Thanks and have fun

Cody Brunty
