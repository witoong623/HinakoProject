# HinakoProject
Automatically adjust computer sound volume when plug in speaker/headphone.

## What is the problem that I faced?
My current laptop doesn't automatically ajust sound volume when I plug in or unplug headphone, unplug is ok but plug in is not because high volume will damage
my ears.

## How does it work?
There is a Win32 API that allow client code to subscribe to audio endpoint device change.  However, plug in or unplug headphone doesn't trigger any event except `OnPropertyValueChanged` (at least on my laptops).

Whenever I plug in or unplug headphone, `OnPropertyValueChanged` event is triggered more than 10 times, each time has differnt key and value arguments that I don't know meaning of them.  Adjust audio volume also triggers `OnPropertyValueChanged` event a few times.  

Because of that reason, I detect plug in/unplug by couting the number of event that is triggered during 1 second time window, if event was triggers more than 10 times, it means I plug in or unplug headphone which I distinguish by showing a window to ask.

## Why this program was named HinakoProject?
Hinako is the name of my favorite character from one of animes that I was watching when I created this project.
## License
HinakoProject is licensed under [MIT license](https://github.com/witoong623/HinakoProject/blob/master/LICENSE)
