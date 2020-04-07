# synth

A programmable synthesizer built with C# on NAudio.

## Why?

For users to compose music and play it back in video games. 

![Say what?](https://media.giphy.com/media/L1W47cPwMyrUs7zEjP/giphy.gif)

[Remember ZZT](https://en.wikipedia.org/wiki/ZZT)? This wonderful little ASCII based game was an early gem from Tim Sweeney of Epic Games. It allowed users to create and program their own worlds from inside the game using [the zzt-oop programming language](http://apocalyptech.com/games/zzt/manual/langref.html). This language included some grammar for playing back composed music, which the programmer would specify with a series of tokens indicating things like pitch and duration.

The purpose of this tool is to play back the music for such a game. It is very much tied to my goal of reimplementing the zzt-oop spec [using a more modern syntax](https://github.com/sslotsky/zzt-oop/blob/master/Tests/ParserTest.fs).

## Usage

See [SongTest](https://github.com/sslotsky/synth/blob/master/Test/SongTest.cs) for example usage.

To integrate this into your own game, you would parse the user's notation and translate the input into the objects exposed by this project.

### Parsing User Notation

An example of this can be found in [the music parsing test](https://github.com/sslotsky/zzt-oop/blob/master/Tests/MusicTest.fs) in the interpreter project. The important thing is that the notation you define can map easily to instances of [the Note object in this project](https://github.com/sslotsky/synth/blob/master/SongStreamer/Note.cs).

### Driving the Synth

Once user input is parsed, you can use it to define tracks [as shown in the test](https://github.com/sslotsky/synth/blob/master/Test/SongTest.cs#L100-L138).
