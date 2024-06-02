# QU Developer Challenge Word Finder

## Objective
Presented with a character Matrix and a large stream of works, this project solves the problem to search the matrix to look for the words from the word stream. Words may appear 
horizontally, from left to rigth,or vertically, from top to bottom.

## About the project
- In the solution developed I tried to apply the SOLID principles which are a very important part of general Design Principles.
By applying these principles in the solution, I  could create a code that is easier to maintain, extend, and modify, leading to more robust, flexible, and reusable software. 
So that the code becomes more modular and easier to work with.
I tried  that the  classes  depend upon interfaces  instead of concrete classes and functions:


![image](https://github.com/Jodebonis/QU.WordFinderAPI/assets/22944478/43365c03-8008-478b-9fdc-ce6f79b8e681)


- Also I developed the project trying to implement it in a high performance way in terms of efficient algorithm and utilization of system resources.
So I used Redis that it makes the application faster and more efficient. By using caching, we can make our applications response quickly, reduce delays and an overall improved user experience.

- I secured the .NET core web API project with JWT which is a popular way of authenticating .NET core web APIs. They could only be accessed by authenticated users.
The main advantages of using a JWT are that it is more compact and therefore has a smaller size.

- For business validation rules for WordFinder entity, I used Fluent Validation which is a popular .NET library that allow me to define validation rules in a fluent, intuitive way, making the code more readable and maintainable.

## Structure of the project

- QU.WordFinderAPI: 
It is the API that handles the WordFinder method. It also handles the Generation of the token for authorization.

- QU.WordFinderAPI.Domain:
Here I create the Models of the solution, WordFinder and LoginRequest. In WordFinder class, I create the Matrix  and WordStream properties.

- QU.WordFinderAPI.Interfaces
IWordFinderCache: I define an interface for caching word search results based on matrix input.
IWordFinderService:  It is the interface for finding words within a matrix using various search patterns.

- QU.WordFinderAPI.Cache: 
Application performance and resource utilization are always considered important aspects of software development. So I decide to use Redis to make efficient use of the resources assigned to them.
By storing frequently accessed data in a cache, the applications can dramatically reduce the response time and resource used to retrieve the data. 

- QU.WordFinderAPI.Services
Provides functionality for finding words within a matrix using various search patterns.

- QU.WordFinderAPI.UnitTest
I created the UnitTest project using Xunit and Moq.

![image](https://github.com/Jodebonis/QU.WordFinderAPI/assets/22944478/dc22e46a-f32e-4859-9ea5-04a8d4dd559c)


## Prerequisites
- NET 8.0
- Redis

## Installation
- 1.Clone the repository to your local machine: git clone https://github.com/Jodebonis/QU.WordFinderAPI.git
- 2.Navigate to the project directory: cd QU.WordFinderAPI
- 3.Restore the project dependencies: dotnet restore
- 4.Configure Redis settings in appsettings.json.
- 5.Run the application.
- 6.dotnet run

## Generating Auth Tokens 
Time to get access! To interact with the WordFinderAPI, you'll need an authorization token from the JWT service.

- 1.Replace the User Name and Password in the appsettings.json file  in the QU.WordFinderAPI folder.

- 2.Execute API Endpoint Login and gets the Token.

- 3.Then execute the API Endpoint WordFinder with the Token generated in the previuos step, and send the Matrix and WordStream parameters to get the Word finder results.

## Running Tests 
To run the tests project and ensure everything's smooth, follow these simple steps:

- 1.Navigate to the project's root folder.
- 2.Jump into the WordFinderAPI.UnitTest folder: cd WordFinderAPI.UnitTest
- 3.Execute the tests using this command: dotnet run test
