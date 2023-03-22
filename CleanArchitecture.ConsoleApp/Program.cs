using CleanArchitecture.ConsoleApp;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

StreamerDbContext streamerDbContext = null;














/**   Patron Mediator  **/

ConcreteMediator m = new ConcreteMediator();

ConcreteColleague1 c1 = new ConcreteColleague1(m);
ConcreteColleague2 c2 = new ConcreteColleague2(m);

m.Colleague1 = c1;
m.Colleague2 = c2;

c1.Send("Hola, ¿cómo estás?");
c2.Send("Muy bien gracias.");






/**  FIN Patron Mediator  **/













//await AddNewRecords();

//QueryStreaming();

//await QueryFilter();

//await QueryMethods();

//await QueryLinq();

//await TrackingAndNoTracking();

//await AddNewStreamerWithVideo();

//await AddNewStreamerWithVideoId();

//await AddNewActorWithVideo();

//await AddDirectorWithVideo();

//await MultipleEntitiesQuery();

Console.ReadKey();

// Copnsultas multiples compuestas
async Task MultipleEntitiesQuery()
{
    //var videoWithActor = await streamerDbContext!.Videos!.Include(q => q.Actors).FirstOrDefaultAsync(q => q.Id == 1);

    //var actor = await streamerDbContext!.Actors!.Select(q => q.Name).ToListAsync();


    var videoWithDirectors = await streamerDbContext.Videos!
                            .Where(x => x.Director != null)
                            .Include(q => q.Director)
                            .Select(e =>
                                new
                                {
                                    DirectorMio = e.Director.Name + e.Director.LastName,
                                    movie = e.Name
                                }
                            ).ToListAsync();


    foreach (var item in videoWithDirectors)
    {
        Console.WriteLine($"{item.DirectorMio} --- {item.movie}");
    }
}

// insersic+on de uno a uno
async Task AddDirectorWithVideo()
{

    var diurector = new Director { Name = "Eugenio", LastName = "Derbez", VideoId = 3 };

    await streamerDbContext.AddAsync(diurector);
    await streamerDbContext.SaveChangesAsync();


}

// insersion muhcos a muchos
async Task AddNewActorWithVideo()
{
    var actor = new Actor
    {
        Name = "Chris Evans",
        LastName = "Evanseses"
    };

    await streamerDbContext.AddAsync(actor);
    await streamerDbContext.SaveChangesAsync();

    var videoActor = new VideoActor
    {
        ActorId = actor.Id,
        VideoId = 1
    };

    await streamerDbContext.AddAsync(videoActor);
    await streamerDbContext.SaveChangesAsync();
}

// insetar registros en conjunto por id
async Task AddNewStreamerWithVideoId()
{

    var ResidentEvil = new Video
    {
        Name = "Resident Evil",
        StreamerId = 5
    };

    await streamerDbContext.AddAsync(ResidentEvil);
    await streamerDbContext.SaveChangesAsync();
}

// insetar registros en conjunto por objetos
async Task AddNewStreamerWithVideo()
{
    var pantaya = new Streamer
    {
        Name = "Pantalla"
    };

    var hungerGames = new Video
    {
        Name = "Hunger Games",
        Streamer = pantaya
    };

    await streamerDbContext.AddAsync(hungerGames);  
    await streamerDbContext.SaveChangesAsync();
}

// tracking y no tracking
async Task TrackingAndNoTracking()
{
    var streamerWithTracking = await streamerDbContext!.Streamers!.FirstOrDefaultAsync(x => x.Id == 1);

    // no se puede utilizar el notracking con un findAsync, lo que hace esque libera de memoria el objeto obtenido, 
    var streamerWithNotTracking = await streamerDbContext!.Streamers!.AsNoTracking().FirstOrDefaultAsync(x => x.Id == 2);


    streamerWithTracking!.Name = "Netflix Super";
    streamerWithNotTracking!.Name = "Amazon Poderoso";

    await streamerDbContext.SaveChangesAsync();
}

// consultasc con linq 
async Task QueryLinq()
{
    Console.WriteLine("Ingresa una compañia de streamig: ");
    string nameStreaming = Console.ReadLine();

    List<Streamer> streamers = await 
        (from s in streamerDbContext.Streamers 
         where EF.Functions.Like(s.Name, $"%{nameStreaming}%")
         select s).ToListAsync();

    foreach (Streamer streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} {streamer.Name}");
    }
}

// Metodos lanvda 
async Task QueryMethods()
{
    var context = streamerDbContext!.Streamers!;

    // regresa el primer registro y si no lo encuentra hay excepciones
    Streamer streamer1 = await context.Where(y => y.Name.Contains("a")).FirstAsync();

    // regresa el primer registro y si no lo encuentra regresa un null
    Streamer streamer2 = await context.Where(y => y.Name.Contains("a")).FirstOrDefaultAsync();

    Streamer streamer3 = await context.FirstOrDefaultAsync(y => y.Name.Contains("a"));

    // el resultado solo es un valor , si hay más lanza excepción
    Streamer streamer4 = await context.SingleAsync(x => x.Id == 1);
    Streamer streamer5 = await context.SingleOrDefaultAsync(x => x.Id == 1);


    var result = await context.FindAsync(1);
}

// Consulta con fitro
async Task QueryFilter()
{

    Console.WriteLine("Ingresa una compañia de streamig: ");
    string nameStreaming = Console.ReadLine();


    // OPCIONES DE FILTRADO CON EF

    //var streamers = await streamerDbContext.Streamers!.Where(x => x.Name == nameStreaming).ToListAsync();

    //var streamers = await streamerDbContext.Streamers!.Where(x => x.Name.Equals(nameStreaming)).ToListAsync();

    //var streamers = await streamerDbContext.Streamers!.Where(x => x.Name.Contains(nameStreaming)).ToListAsync();

    // Busca todos los nombre que contengan el parametro, utilizando EF, los porcentajes son para que sea una busqueda parcial
    var streamers = await streamerDbContext.Streamers!.Where(x => EF.Functions.Like(x.Name, $"%{nameStreaming}%")).ToListAsync();

    foreach (var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Name}");
    }

}

//Consulta general
void QueryStreaming()
{
    var streamers = streamerDbContext!.Streamers!.ToList();

    foreach (Streamer streamer in streamers)
    {
        Console.WriteLine($"{streamer.Name} {streamer.Id}");
    }
}

// Agrega registros a la base
async Task AddNewRecords()
{
    Streamer streamer = new()
    {
        Name = "Disney Plus +",
        Url = "https://www.disney.com"
    };

    streamerDbContext!.Streamers!.Add(streamer);

    await streamerDbContext.SaveChangesAsync();


    List<Video> movies = new List<Video>()
    {
        new Video
        {
            Name="Cenicienta",
            StreamerId=streamer.Id,
        },
        new Video
        {
            Name ="Pinocho",
            StreamerId=streamer.Id,
        },
        new Video
        {
            Name = "101 Dalmatas",
            StreamerId = streamer.Id
        },
        new Video
        {
            Name = "Sirenita",
            StreamerId = streamer.Id
        }
    };

    await streamerDbContext.AddRangeAsync(movies);
    await streamerDbContext.SaveChangesAsync();
}

