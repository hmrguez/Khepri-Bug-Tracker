# Project Khepri

## Summary

This is a feature and bug tracker made with **ASP.NET Core** using the **MVC** design pattern, with **MS SQL Server** as a primary database. Its purpose is to serve as a lightweight way to keep track of everything we want to implement or fix in a growing project. It essentially works as a to-do list grouped by projects. The main models of the application are **Tasks** and **Projects**. This is a classic CRUD app. It allows for creating (Tasks, Projects), reading (Tasks, Projects), updating (Tasks) and deleting(Tasks, Projects). It also allows for simple user login and register for making every task or project a private entry.

Both features and bugs are treated the same way in the app, calling it **Tasks**. Each task has several properties such as its name, description, type (Bug or Feature), date issued, status (Open, Closed and Completed) and Manager Email. However how they are implemented really is up to the user, the status of a Task is recommended to implement as the following: 
- **Opened**: the task has just been created and can be carried on at any moment
- **Closed**: the task has already been disqualified and it's not going to be carried on for the time being
- **Completed**: the task has already been completed, which serves a special place in the app: since every task has a Manager Email field, whenever a task is completed, this Manager receives an email indicating that the current user has just completed that task

## Dependencies

I'm not looking to assign collaborators for the project at the moment but if you have any suggestions I will kindly read it. The only dependency you need for running a fork of the project is to have **.NET 6 SDK** installed.

## Usage

### Get Started
1. First you need to create an account or sign in if you already have one. Both the sign in and the sign up menus can be accessed by their respective buttons on the top right corner of the navbar
2. Then you need to create your first project:
   1. Go to the project tab and click add project
   2. Fill the forms and hit create
3. Then go to the Tasks tab, where every project you currently have should appear as a card, and click See Tasks
4. Create a task the same way you can create a Project (every field is required but the Manager Email, which if empty won't send any email when the task is completed)

### More
1. You can delete any project when in the projects tab by clicking the trash can at the end of every row or in the task tab by clicking delete in the respective tab
2. When a project is selected in the Task tab you can see the list of every Task you have for every project, at the end of every row you can click the info button to see more details about the task
   1. Inside you will see every field of the task
   2. Also three buttons on the bottom of the window which will enable you to edit a task, complete it (which can also be done by editing it) and delete it.

