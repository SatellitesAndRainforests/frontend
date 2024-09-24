using frontend.UI.Extensions;
using frontend.UI.Middleware;
using GovUk.Frontend.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// builder.WebHost.UseUrls("http://*:80");

if ( builder.Environment.IsDevelopment() ) {
	builder.WebHost.ConfigureKestrel( serverOptions => {
		serverOptions.ListenAnyIP( 5000 );
		serverOptions.ListenAnyIP( 5001, listenOptions => 
			{
				listenOptions.UseHttps( "/https/localhost.pfx", "myPassword" );  
			});
	});
}
else {
	builder.WebHost.ConfigureKestrel( serverOptions => {
		serverOptions.ListenAnyIP( 80 );
		serverOptions.ListenAnyIP( 443, listenOptions => 
			{
				listenOptions.UseHttps( "/https/localhost.pfx", "myPassword" );  
			});
	});
}


// Add services to the container.
builder.Services.AddRazorPages()

    .AddRazorPagesOptions(options =>
    {
        //options.Conventions.ConfigureFilter(new AutoValidateAntiforgeryTokenAttribute()); // from microsoft?
        options.Conventions.AuthorizePage("/ProtectedPage", "userPolicy");
        options.Conventions.AuthorizeFolder("/ProtectedFolder", "userPolicy");
    })
    .AddViewOptions(options =>
    {
        options.HtmlHelperOptions.ClientValidationEnabled = false;
    });
//.AddMicrosoftIdentityUi();

// builder.Services.Configure<GovUkOidcConfigureConfiguration>(builder.Configuration.GetSection(nameof(GovUkOidcConfiguration)));

builder.Services.AddGovUkFrontend();

builder.Services.AddHttpClient();
builder.Services.AddHealthChecks();

// azure event source listener createconstolelogger?
// azure credential options - new deafault azure credential options . . .

// azure key vault configuration

// Authentication
// Internal Portal Session


// - internal -----------------------------------------------------------------------------------
//builder.Configuration.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromHours(10000);
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//    options.Cookie.Name = ".front.end.service";
//});

// - external -----------------------------------------------------------------------------------
//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromHours(10);
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//    options.Cookie.Name = ".from external portal";

//});

//builder.Host.UseSerilog((context, services, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

//app.Use(async (context, next) =>
//{
//    context.Response.Headers.Append("X-Frame-Options", "DENY");
//});

 // ----------------------------------------------------------------------------------------------



//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("userPolicy", policy => policy.RequireRole("userRole"));
//});

// Authorisation
// builder.Services.AddAuthentication . . .
// builder.services.Configure . . . access desned path

builder.Services.AddHttpContextAccessor();
// builder.Services.AddAzureConfiguration();
//builder.Services.AddFeatureManagement();

builder.Services.ConfigureApplicationServices(builder.Configuration);

//builder.Services.Configure<HeaderOptions>(builder.Configuration.GetSection(HeaderOptions.Name));

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor |
                                Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto;
});





 // ----------------------------------------------------------------------------------------------

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// app.UseForwardedHeaders();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // more azure app configuration
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
		app.UseHttpsRedirection();
} 
else
{
    app.UseDeveloperExceptionPage();
}

// from external
//app.Use(async (context, next) =>
//{
//    context.Response.Headers.Append("X-Frame-Options", "DENY");
//});

//app.UseSerilogRequestLogging();
//app.UseSession();


app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
// app.UseAuthorization();
//app.MapSignout();

// app.UseWhen(context => context.Request.Path.StartsWithSegments("/word"), applicationBuilder =>
// {
//     applicationBuilder.UseMiddleware<CustomMiddleware>();
// });

//app.UseSession();
app.MapRazorPages();
// app.UseStatusCodePagesWithReExecute("/Errors/{0}");

app.MapHealthChecks("/health");

//if (!app.Environment.IsDevelopment())
//{
//    app.UseAzureAppConfiguration();
//}

app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapRazorPages();
});

app.Run();

















