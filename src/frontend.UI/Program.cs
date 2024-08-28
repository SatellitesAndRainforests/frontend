using frontend.UI.Extensions;
using frontend.UI.Middleware;
using GovUk.Frontend.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages()

    .AddRazorPagesOptions(options =>
    {
        options.Conventions.AuthorizePage("/ProtectedPage", "userPolicy");
        options.Conventions.AuthorizeFolder("/ProtectedFolder", "userPolicy");
    })
    .AddViewOptions(options =>
    {
        options.HtmlHelperOptions.ClientValidationEnabled = false;
    });
//.AddMicrosoftIdentityUi();

builder.Services.AddGovUkFrontend();

builder.Services.AddHttpClient();
builder.Services.AddHealthChecks();

// azure event source listener createconstolelogger?
// azure credential options - new deafault azure credential options . . .

// azure key vault configuration

// Authentication
// Internal Portal Session
//builder.Configuration.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromHours(10000);
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//    options.Cookie.Name = ".front.end.service";
//});

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("userPolicy", policy => policy.RequireRole("userRole"));
//});

// Authorisation
// builder.Services.AddAuthentication . . .
// builder.services.Configure . . . access desned path

builder.Services.AddHttpContextAccessor();
// builder.Services.AddAzureConfiguration();
// builder.Services.AddFeatureManagement();

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

app.UseForwardedHeaders();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // more azure app configuration
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
} 
else
{
    app.UseDeveloperExceptionPage();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
// app.UseAuthorization();

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

















