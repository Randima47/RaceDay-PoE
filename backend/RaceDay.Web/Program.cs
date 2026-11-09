using RaceDay.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Add MVC Controllers & Views
builder.Services.AddControllersWithViews();

// 2. Register HttpClient & ApiService for Dependency Injection
builder.Services.AddHttpClient<ApiService>();

// 3. Register In-Memory Session Storage
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 4. Enable Session Middleware 
app.UseSession();

app.UseAuthorization();

// 5. Default Route Configuration
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Events}/{action=Index}/{id?}");

app.Run();