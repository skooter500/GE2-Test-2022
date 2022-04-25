# Game Engines 2 2022 - Lab Test

## Rules

- Please stay on teams for the duration of the test
- You can access the [Unity Reference](https://docs.oracle.com/javase/7/docs/api/) and the [Unity reference](https://git-scm.com/docs) if you need to look something up
- No use of notes or previously written code
- No collaboration or communication

Nematodes, also called roundworms, are multicellular organisms that occur as parasites in animals and plants or as free-living forms in soil and water. Many species of nematodes are microscopic, however, some species such as tapeworms can grow to several meters in length. They are the most abundant lifeforms on the planet and there are over 400 quintillion individual nematodes on earth. In today's lab test you will be coding elements of this Nematode simulation (click the image for a video):

[![YouTube](http://img.youtube.com/vi/Sixvl_2LgLg/0.jpg)](https://youtu.be/Sixvl_2LgLg)

## Instructions

- Fork this repo. It contains all the code and examples we made this semester.
- Set up origin and upstream remotes on your fork
- Open the nematodes scene. 
- Write a class called Nematode that has appropriate fields, a constructor, accessor methods and a toString method. Include in your soultion a constructor that takes a TableRow parameter from the Processing library as a parameter
- Write a method loadNematodes on the class NematodeVisualiser. It should load the csv file and populate an ArrayList of Nematodes
- Write code to visualise the nematodes
- Write code to cycle through the nematodes using the arrow keys 
- [Submit your solution]()

## Marking Scheme

| Marks | Description |
|-------|-------------|
| 15 | Creating the Nematode class |
| 15 | Loading the dataset |
| 40 | Visualising the dataset |
| 20 | Navigating through the dataset |
| 10 | Correct use of git |