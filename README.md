# PugTrack - web app 
Simple task managing app created with ASP.NET Core, React.js and Entity Framework Core. 

## Table of Contents
* [General info](#general-info)
* [Features](#features)
* [Technologies](#technologies)
* [Status](#status)

## General info
PugTrack is a single page application that provides a ticketing system to record and follow the progress of every project and task created by users. 

## Features
* adding projects, tasks & comments, every task is associated with project and every comments is assigned to the task & commenting user;
* machine state used to maintain status of the task, preventing from changes on different states, e.g. cannot edit finished task;
* every task can have one assigned user, different priority, deadline;
* authentication made by separate IdentityServer4 app;
* unit tests for Domain project

## Technologies
* Asp.Net Core - ver. 3.1
* EntityFramework Core - ver. 3.1.2
* IdentityServer4 - ver. 3.1.2
* MediatR - ver. 8.0.1
* CSharpFunctionalExtensions - ver. 1.19.1
* Stateless - ver. 5.1.0
* Xunit - ver. 2.4.0
* React - ver. 16.9.13
* TypeScript - ver. 3.7.2

## Status
In progress.
