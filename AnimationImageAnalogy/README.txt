=====================================================

						    FINAL PROJECT README

=====================================================

Note: I created this project using C# rather than Matlab, because it is a project
I am working on for my BXA Capstone Project. I plan to continue working on this
program and ultimately want to create an animation tool that I can use in future
animation projects. I might be using this tool beyond my time at CMU (and therefore
beyond the time that I have access to a Matlab license) so I decided to do the project
in C#. 

Also, I have a Github repo for this project since it is a longterm project which
I want to keep backed up under version control. The git repo can be found here: https://github.com/sylviakosowski/Image_Analogy. (Just thought I should mention its existence in case you
do any plagiarism checks on the web and find it there.)

Here is a desription of the code content:

ImageAnalogy.cs - Contains the bulk of the methods for performing the steps of
the image analogy algorithm.

Node.cs - A class for representing a node used in the graphs for the Dijkstra's 
algorithm step.

PatchGraph.cs - Class which represents overlapping patches of pixels as a graph. 
Contains methods to create and initialize graphs, as well as perform Dijkstra's 
algorithm to find the shortest path in overlapping patches. 

Program.cs - Contains main method.

Utilities.cs - Contains methods to read and save image data.