# COMMON
Creating a website, that enables operations with the Northwind Database.

# Module 1: Introduction. Creating base website with the following:
- EF Core ORM.
- Serilog logging.
- server-side validation.
- client-side validation: jQuery.
- DI/IoC.

# Module 2: Middleware, Filters, Unit testing. Add the following:
- custom middleware for image caching.
- MVC filter for logging calls to actions.
- separate project with unit tests (NUnit).
- get and upload image.
- add alternative routing for getting images. Standard routing: {controller}/{action}/; Alternative: images/{image_id}.

# Module 3: Razor Client. Add the following:
- basic client with Razor.

# Module 4: Web API. Add the following:
- simple API Controllers, which return entities; configure routing for this: <site_root>/api.
- console .Net Core client for API Controllers.
- simple JS+HTML client for API Controllers.
- edit data action for controllers (create, update, delete, get and update image).
- swagger API documentation

# Module 5: Security. Add the following:
- authentication (Azure AD; Local user database and ASP.Net Core Identity) and authorization.

# Module 6: Hosting and Deploy. Add the following:
- two deployment profiles: deploy on IIS, deploy in Azure App Service and SQL Azure.