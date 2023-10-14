var builder = WebApplication.CreateBuilder(args);

// add services to the container

builder.Services.AddControllers();
// learn more about configuring swagger/openAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region MongoDbSettings
///// get values from this file: appsettings.Development.json /////
// get section
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection(nameof(MongoDbSettings)));

// get values
builder.Services.AddSingleton<IMongoDbSettings>(serviceProvider =>
serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);

// get connectionString to the db
builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    MongoDbSettings uri = serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value;


    return new MongoClient(uri.ConnectionString);
});
#endregion MongoDbSettings

#region Cors: baraye ta'eede Angular HttpClient requests
builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(policy => 
            policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));
    });
#endregion Cors

#region Dependency Injections
// builder.Services.AddSingleton<IRegisterRepository, RegisterRepository>(); App LifeCycle
builder.Services.AddScoped<IRegisterRepository, RegisterRepository>(); //conttroller LifeCycle
builder.Services.AddScoped<IPeopleRepository, PeopleRepository>();

#endregion Dependency Injections
// Add services to the container.

// builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
