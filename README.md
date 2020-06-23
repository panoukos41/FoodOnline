# FoodOnline

This project was made for [Web Applications in Production](https://eclass.uniwa.gr/courses/IDPE104/) at [University of West Attica](https://www.uniwa.gr/) department of [Industrial Design and Production Engineering](http://www.idpe.uniwa.gr/)

The project is a web application for online purchasing of fast food from varius shops that are registered in the application.  

Some of the features are realtime updates to the order from the shop to the client, User registration to save stores as favorites etc, Search for stores based on the client's addres and many more.

The application uses [MySQL](https://www.mysql.com/) as the database, [ASP.NET](https://dotnet.microsoft.com/apps/aspnet) as the backend and [Blazor WebAssemlby](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor) as the frontend.

# Build and run

### - What you will need
- [.Net Core 3.1+ SDK](https://dotnet.microsoft.com/download). If you have the latest version of Visual Studio then you will probably have it already so you can skip this step.
- A mysql instance running at port 3306. The porject uses the user "root" with a password "password".  
- One database named foodonline  
- One database named foodonline_users  
- The [Ef Core Tools](https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet) to run migrations on the databases.  

### - Before you run
- Use the "ef-app.ps1" and "ef-foodonline.ps1" scripts with the command "update" to apply the migrations to the databases.

### - Using Visual Studio:
1. The latest version.  
2. Open the solution.  
3. Set WebApi as the startup project and click run.

### - Using Vs Code:
1. TODO

### - Using the dotnet CLI
1. Navigate to the project folder named "WebApi"
2. Run the command "dotnet run"

# License
[The MIT License](https://github.com/panoukos41/FoodOnline/blob/master/LICENSE.md)

## Resources read and watched to make the project
- [GOTO 2019 • Clean Architecture with ASP.NET Core 3.0 • Jason Taylor](https://www.youtube.com/watch?v=dK4Yb6-LxAk)
- [Clean Code with Entity Framework Core | Brendan Richards](https://www.youtube.com/watch?v=LDRxo6wDIE0)
- [Real-time web applications with ASP.NET Core SignalR](https://www.youtube.com/watch?v=YwezzKWrFuo)
- [SignalR: To Chat and Beyond - David Pine](https://www.youtube.com/watch?v=i3RXbOY6-0I)
- [Background Photo](https://pixabay.com/el/photos/%CF%80%CE%AF%CF%84%CF%83%CE%B1-%CE%BC%CE%B1%CE%B3%CE%B5%CE%AF%CF%81%CE%B5%CE%BC%CE%B1-fast-food-%CF%83%CE%BD%CE%B1%CE%BA-2068272/)
- [Blazorstrap](https://blazorstrap.io/)
- [Css-tricks](https://css-tricks.com/fun-viewport-units/)
- [Css loaders](https://projects.lukehaas.me/css-loaders/)
- [Auth](https://chrissainty.com/securing-your-blazor-apps-authentication-with-clientside-blazor-using-webapi-aspnet-core-identity/)
- [Validation](https://shawtyds.wordpress.com/2019/11/16/making-blazor-validation-play-nice-with-bootstrap-4/)

## Notes
The project supports openid, but it's not impleneted in a clear manner, the project also is missing some features and pages in the frontend.

I will be updating the project whenever i can to make it what i imagined it to be at first! 
