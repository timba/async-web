async-web
=========

This project is an attempt to show difference between async and sync data obtaining approaches in ASP.NET MVC web application.

Init setup
==========

1. The project has two data providers implemented: ADO.NET and EF6 (I used RC1). Provider which is being used is specified in appSettings, setting name "DbProvider", possible values: "ADO" and "EF" for ADO.NET and EF6 respectively.

2. Connection string is called ComputerDb; please note that "Asynchronous Processing=True;" should remain in order allow async SQL server operations. 

3. Database is created and initialized upon web aplication startup (Application_Start) by EF database initializer feature (even if ADO provider selected). The DB is filled by 15000 test records.

4. I used VS 2012, .NET Framework 4.5, MS SQL 2012 Express.

Usage
=====

Web application starts on computers list.

There is a console application async-client which loads the server with multiple requests (300 parallel tasks, pause 50 ms between requests). After start the console application waits for a command to enter: 

- "async" + Enter starts load testing using async controller 

- "sync" + Enter starts load testing using sync controller

To see current server performance results, I used Windows performance monitor, where I selected counter from group "ASP.NET Applications" called "Requests/Sec".