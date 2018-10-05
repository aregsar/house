An Opinionated ASP.NET Core 2.1 project boilerplate

The boilerplate uses sqlite database to be able to demo on Mac OSX

1-Global Routes:

The top level Routes.cs file can be used to optionally add global route map instead of attribute routing

The example route routes.Get("/", "Home.Index") maps a Http Get request for the root url / to the HomeController.Index action and sets the route name to Home.Index

There are methods for all Http Verbs

An overload of the method takes an explicit route name.

So the routes.Get("/", "Home.Index", "Home_Index") method will map to HomeController.Index action with aroute name of Home_Index

2-Global Exception Handling 

Global exception handling is configured for MVC and API requests:

The global exception handling returns and error response in a format based on the request accept header. 
For non json accept headers  the exception handler redirects to an error action for an html error response
For json accept headers for API requests the exception handler returns a json error response

3-ViewModels

View models provide a container for all data that a particular view template needs. 
It is important to note that view models are asocciated with a particular view template and not with any action or data model
Depending on the view that an action returns it needs to pass along the appropriete view model associated with the view template.

4-ActionModels

Action models on the other hand are associated with a particular action and are a container for all the input data that the action needs

5-ResponseModels

Response models provide a container that is used to shape the json response for api actions

6-Html Form Tag helpers
The model used to bind to form fields can be nested inside a ViewModel property. In that case the ActionModel of the action that the form posts to needs to also define a nested models property of the same name that also has properties with same name as the properties of the model that was bound to the form via tag helpers

7-Action Method Dependency injection for dependancies that are only used in some but not all controller actions

8-Logging and configuration configured outside of Startup.cs and injected in.

TODO:

This boilerplate development is in progress and I plan to add the following features in the futue:

-Serilog structured logging

-Integration testing infrastructure to support controller tests with web test host

-Adding Logging middleware and\or action filters

-Adding ASP.NET Identity support

-adding client side validation js files

-and more




