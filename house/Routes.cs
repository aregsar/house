using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing.Constraints;

using house.Helpers;


namespace house
{
    public class Routes
    {

        //Maps routes Globally in one place. Use this if you don't fancy attribute routing for MVC routes!
        public static void BuildRoutes(IRouteBuilder routes)
        {
            //MVC routes

            //GET route name: Home.Index, controller: Home, action: Index
            routes.Get("/", "Home.Index");
               
            routes.Get("/Home/Error", "Home.Error");

           
            routes.Get("/house/index", "House.Index");

            routes.Get("/house/show/{id:int}", "House.Show");

            routes.Get("/house/new", "House.New");

            routes.Post("/house/create", "House.Create");

            routes.Get("/house/Edit/{id:int}", "House.Edit");

            routes.Post("/house/Update", "House.Update");

            routes.Get("/house/Delete/{id:int}", "House.Delete");

            routes.Post("/house/Destroy", "House.Destroy");


            routes.Get("/signup/new", "Signup.New");

            routes.Post("/signup/create", "Signup.Create");


            routes.Get("/signin/new", "Signin.New");

            routes.Post("/signin/create", "Signin.Create");

            routes.Post("/signin/Remove", "Signin.Remove");



            routes.CatchAll(controller: "Home", action: "Error");

        }

    }
}
