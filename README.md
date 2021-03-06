LiveSplit.TwitchPredictions
=====================
A plugin for LiveSplit allowing for managing Twitch Predictions based on Splits. **The plugin uses Beta API endpoint - it may not work correctly in the future** (see [https://dev.twitch.tv/docs/product-lifecycle](https://dev.twitch.tv/docs/product-lifecycle))

Manual Installation
-------------------
Download "Components/LiveSplit.TwitchPredictions.dll" and place it into the subdirectory "Components" of your LiveSplit folder. You can then add it to your layout (category "Control").

Developement progress
-------------------
* Obtaining oauth - 100%
* Storing loging information outside of LiveSplit (to prevent data leaking out when a user decides to share layout files) - 100%
* Connecting to chat - 80%
* Storing split-event information per game/category - 95%
* Split-prediction event editor - 90%
* Verification of split-events list - 70%
* Running predictions based on split-event list - 95%

Bugs so far:
-------------------
* Editor is really junky with refreshing preview (colors)
* Events get delayed by request thread sleep.

Possible improvements
-------------------
* Editor currently doesn't check whatever a duration of prediction is longer than a combined personal PB split seperating it from the next event.
