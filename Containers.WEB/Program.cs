using LogisticaContainers.Managers.Managers;
using LogisticaContainers.Managers.ModelFactories;
using LogisticaContainers.Managers.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

/*
    Registra el servicio IContainerManager y su implementación ContainerManager
    en el contenedor de dependencias con un ciclo de vida "Scoped".
    "Scoped" significa que una nueva instancia de ContainerManager será creada
    para cada solicitud HTTP individual y compartida dentro de esa misma solicitud.
    Esto asegura que cualquier componente que necesite IContainerManager dentro de una
    misma solicitud recibirá la misma instancia.
*/

// ----- MANAGERS ----- //
builder.Services.AddScoped<IContainerManager, ContainerManager>();

// ----- REPOSITORIOS ----- //
builder.Services.AddScoped<IContainerRepository>(_ => new ContainerRepository(builder.Configuration["Db:ConnectionString"]));
builder.Services.AddScoped<IEstadoContainerRepository>(_ => new EstadoContainerRepository(builder.Configuration["Db:ConnectionString"]));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
