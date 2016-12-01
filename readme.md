# Advent of Code 2016
## Introduction
This repository contains my solutions for the
[Advent of Code 2016](http://adventofcode.com/2016) challenge.

The solutions are implemented in Dot Net Core and C#, this being my
first attempt to do something real with these new toys. Having never
worked with either I welcome any feedback.

## Data
The program downloads and stores your challenge data in the `/Data`
directory, so as not to put an unreasonable strain on the AoC servers
(especially when developing).

## Commands
If you have the Dot Net Core runtimes installed on your system, you can
run the project by running:
```
    $ dotnet run <session token> <day>
```
* `<session token>`: Your session token. You can find this by logging
in to AoC and looking at your brower's cookies;
* `<day>`: The day for which you want to solve.