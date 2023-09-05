using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Project_HiveView_Web.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
    .AddCookie()
    .AddOpenIdConnect("oidc", options =>
    {
        options.MetadataAddress = builder.Configuration["Cognito:MetadataAddress"];
        options.Authority = builder.Configuration["Cognito:Autority"];
        options.ClientId = builder.Configuration["Cognito:ClientId"];
        options.ClientSecret = builder.Configuration["Cognito:ClientSecret"];
        options.ResponseType = builder.Configuration["Cognito:ResponseType"];
        options.CallbackPath = builder.Configuration["Cognito:CallbackPath"];
        options.SaveTokens = true;
        options.GetClaimsFromUserInfoEndpoint = true;
        options.UseTokenLifetime = false;
        options.Scope.Add("openid");
        options.Scope.Add("profile");
        options.Scope.Add("email");
        options.TokenValidationParameters = new TokenValidationParameters { NameClaimType = "name" };

        options.Events = new OpenIdConnectEvents
        {
            OnAccessDenied = context =>
            {
                context.HandleResponse();
                context.Response.Redirect("/");
                return Task.CompletedTask;
            }
        };
    });
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication();
app.UseAuthorization();

//app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

