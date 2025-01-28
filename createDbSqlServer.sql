CREATE DATABASE CineDB;
USE CineDB;

-- Tabla Cine
CREATE TABLE Cines (
    CineId INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Ubicacion NVARCHAR(100) NOT NULL
);

-- Tabla Sala
CREATE TABLE Salas (
    SalaId INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL,
    CineId INT NOT NULL,
    FOREIGN KEY (CineId) REFERENCES Cines(CineId)
);

-- Tabla Pelicula
CREATE TABLE Peliculas (
    PeliculaId INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(MAX),
    Director NVARCHAR(100),
    Actores NVARCHAR(MAX),
    Genero NVARCHAR(50),
    Clasificacion NVARCHAR(50),
    Duracion INT,
    ImageUrl NVARCHAR(255),
    CartelUrl NVARCHAR(255)
);

-- Tabla Sesion
CREATE TABLE Sesiones (
    SesionId INT IDENTITY(1,1) PRIMARY KEY,
    SalaId INT NOT NULL,
    PeliculaId INT NOT NULL,
    FechaDeSesion DATE NOT NULL,
    HoraDeInicio DATETIME NOT NULL,
    FOREIGN KEY (SalaId) REFERENCES Salas(SalaId),
    FOREIGN KEY (PeliculaId) REFERENCES Peliculas(PeliculaId)
);

-- Tabla Butaca
CREATE TABLE Butacas (
    ButacaId INT IDENTITY(1,1) PRIMARY KEY,
    SalaId INT NOT NULL,
    Estado NVARCHAR(20) DEFAULT 'Disponible',
    PrecioButaca DECIMAL(10,2) NOT NULL,
    TicketId INT,
    FOREIGN KEY (SalaId) REFERENCES Salas(SalaId)
);

-- Tabla Ticket
CREATE TABLE Tickets (
    TicketId INT IDENTITY(1,1) PRIMARY KEY,
    SesionId INT NOT NULL,
    NombreInvitado NVARCHAR(100),
    EmailCompra NVARCHAR(100) NOT NULL,
    ButacaId INT NOT NULL,
    FechaDeCompra DATETIME NOT NULL,
    FOREIGN KEY (SesionId) REFERENCES Sesiones(SesionId),
    FOREIGN KEY (ButacaId) REFERENCES Butacas(ButacaId)
);

-- Insertar datos de ejemplo para el cine (basado en DatosCines.cs)
INSERT INTO Cines (Nombre, Ubicacion) 
VALUES ('MUELMO Cines Puerto Venecia', 'Zaragoza');

-- Insertar salas de ejemplo
INSERT INTO Salas (Nombre, CineId) VALUES 
('Sala 1', 1),
('Sala 2', 1),
('Sala 3', 1),
('Sala 4', 1);

-- Insertar datos de películas
INSERT INTO Peliculas (Title, Descripcion, Director, Actores, Genero, Clasificacion, Duracion, ImageUrl, CartelUrl) 
VALUES 
    ('Origen', 'Dom Cobb roba secretos del subconsciente, pero enfrenta el reto inverso: implantar una idea en otra mente. Un reto único y peligroso.', 
    'Christopher Nolan', 
    'Leonardo DiCaprio, Joseph Gordon-Levitt, Ken Watanabe, Tom Hardy, Elliot Page, Dileep Rao, Cillian Murphy, Tom Berenger, Marion Cotillard',
    'Acción, Ciencia ficción, Aventura', '12', 148,
    'https://image.tmdb.org/t/p/original/tXQvtRWfkUUnWJAn2tN3jERIUG.jpg',
    'https://image.tmdb.org/t/p/original/8ZTVqvKDQ8emSGUEMjsS4yHAwrp.jpg'),

    ('Interstellar', 'Con la Tierra al borde del colapso, un grupo de astronautas emprende una misión para explorar planetas y salvar a la humanidad.',
    'Christopher Nolan',
    'Matthew McConaughey, Anne Hathaway, Michael Caine, Jessica Chastain, Casey Affleck, Wes Bentley, Topher Grace, Mackenzie Foy, Ellen Burstyn',
    'Aventura, Drama, Ciencia ficción', '12', 169,
    'https://image.tmdb.org/t/p/original/nrSaXF39nDfAAeLKksRCyvSzI2a.jpg',
    'https://image.tmdb.org/t/p/original/l33oR0mnvf20avWyIMxW02EtQxn.jpg'),

    ('El caballero oscuro', 'Batman enfrenta al Joker, su enemigo más peligroso, quien busca llevar el caos absoluto a Gotham en un enfrentamiento épico y letal.',
    'Christopher Nolan',
    'Christian Bale, Heath Ledger, Aaron Eckhart, Michael Caine, Maggie Gyllenhaal, Gary Oldman, Morgan Freeman, Monique Gabriela Curnen, Ron Dean',
    'Drama, Acción, Crimen, Suspense', '16', 152,
    'https://image.tmdb.org/t/p/original/8QDQExnfNFOtabLDKqfDQuHDsIg.jpg',
    'https://image.tmdb.org/t/p/original/dqK9Hag1054tghRQSqLSfrkvQnA.jpg'),

    ('Matrix', 'Neo descubre que la Matrix es una simulación que controla a la humanidad y se une a la resistencia para liberar a todos de esta prisión.',
    'Lana Wachowski',
    'Keanu Reeves, Laurence Fishburne, Carrie-Anne Moss, Hugo Weaving, Gloria Foster, Joe Pantoliano, Marcus Chong, Julian Arahanga, Matt Doran',
    'Acción, Ciencia ficción', '18', 136,
    'https://image.tmdb.org/t/p/original/qK76PKQLd6zlMn0u83Ej9YQOqPL.jpg',
    'https://image.tmdb.org/t/p/original/l4QHerTSbMI7qgvasqxP36pqjN6.jpg'),

    ('El club de la lucha', 'Un insomne y un vendedor de jabón fundan un club clandestino de peleas que se transforma en una peligrosa organización.',
    'David Fincher',
    'Edward Norton, Brad Pitt, Helena Bonham Carter, Meat Loaf, Jared Leto, Zach Grenier, Holt McCallany, Eion Bailey, Richmond Arquette',
    'Drama', '18', 139,
    'https://image.tmdb.org/t/p/original/sgTAWJFaB2kBvdQxRGabYFiQqEK.jpg',
    'https://image.tmdb.org/t/p/original/n0ceI4oS4wCad1GPvnf4FMBwBie.jpg'),

    ('Pulp Fiction', 'Varias historias violentas y absurdas en Los Ángeles se entrelazan, mostrando la crudeza, el humor negro y lo impredecible del destino.',
    'Quentin Tarantino',
    'John Travolta, Samuel L. Jackson, Uma Thurman, Bruce Willis, Ving Rhames, Harvey Keitel, Eric Stoltz, Tim Roth, Amanda Plummer',
    'Suspense, Crimen', '18', 154,
    'https://image.tmdb.org/t/p/original/znOzYX1hOzt1Gd1Oybyan3hII3U.jpg',
    'https://image.tmdb.org/t/p/original/5kD1o6exM1RbauVLJjtNzseiM1Q.jpg'),

    ('Cadena perpetua', 'Andy Dufresne, preso por un crimen que no cometió, forma un lazo con otro recluso mientras busca esperanza y redención en prisión.',
    'Frank Darabont',
    'Tim Robbins, Morgan Freeman, Bob Gunton, William Sadler, Clancy Brown, Gil Bellows, James Whitmore, Mark Rolston, Jeffrey DeMunn',
    'Drama, Crimen', '12', 142,
    'https://image.tmdb.org/t/p/original/uRRTV7p6l2ivtODWJVVAMRrwTn2.jpg',
    'https://image.tmdb.org/t/p/original/wPU78OPN4BYEgWYdXyg0phMee64.jpg'),

    ('Forrest Gump', 'Forrest Gump, un hombre de corazón puro, vive aventuras extraordinarias que, sin proponérselo, transforman la historia de Estados Unidos.',
    'Robert Zemeckis',
    'Tom Hanks, Robin Wright, Gary Sinise, Sally Field, Mykelti Williamson, Michael Conner Humphreys, Hanna Hall, Haley Joel Osment, Siobhan Fallon Hogan',
    'Comedia, Drama, Romance', '12', 142,
    'https://image.tmdb.org/t/p/original/oiqKEhEfxl9knzWXvWecJKN3aj6.jpg',
    'https://image.tmdb.org/t/p/original/tlEFuIlaxRPXIYVHXbOSAMCfWqk.jpg'),

    ('El padrino', 'La familia Corleone, poderosa en el crimen organizado, enfrenta cambios y tragedias mientras el poder transforma a sus miembros.',
    'Francis Ford Coppola',
    'Marlon Brando, Al Pacino, James Caan, Robert Duvall, Richard S. Castellano, Diane Keaton, Talia Shire, Gianni Russo, Sterling Hayden',
    'Drama, Crimen', '16', 175,
    'https://image.tmdb.org/t/p/original/5HlLUsmsv60cZVTzVns9ICZD6zU.jpg',
    'https://image.tmdb.org/t/p/original/es402xnlOLE6jhbiisVwwlazrnM.jpg'),

    ('El señor de los anillos: La comunidad del anillo', 'Frodo y sus amigos emprenden una misión peligrosa para destruir el Anillo Único y evitar que Sauron lo recupere.',
    'Peter Jackson',
    'Elijah Wood, Ian McKellen, Viggo Mortensen, Sean Astin, Ian Holm, Liv Tyler, Christopher Lee, Sean Bean, Billy Boyd',
    'Aventura, Fantasía, Acción', '12', 178,
    'https://image.tmdb.org/t/p/original/9xtH1RmAzQ0rrMBNUMXstb2s3er.jpg',
    'https://image.tmdb.org/t/p/original/z51Wzj94hvAIsWfknifKTqKJRwp.jpg'),

    ('Gladiator', 'Traicionado y esclavizado, Máximo busca vengar a su familia mientras se convierte en un héroe de la arena en el Coliseo romano.',
    'Ridley Scott',
    'Russell Crowe, Joaquin Phoenix, Connie Nielsen, Oliver Reed, Richard Harris, Derek Jacobi, Djimon Hounsou, David Schofield, John Shrapnel',
    'Acción, Drama, Aventura', '12', 155,
    'https://image.tmdb.org/t/p/original/o6XhzKghQFliN49iE4M4RX94PTB.jpg',
    'https://image.tmdb.org/t/p/original/jhk6D8pim3yaByu1801kMoxXFaX.jpg'),

    ('Salvar al soldado Ryan', 'En la Segunda Guerra Mundial, soldados liderados por el Capitán Miller buscan rescatar al soldado Ryan tras líneas enemigas.',
    'Steven Spielberg',
    'Tom Hanks, Tom Sizemore, Edward Burns, Barry Pepper, Adam Goldberg, Vin Diesel, Giovanni Ribisi, Jeremy Davies, Matt Damon',
    'Drama, Historia, Bélica', '12', 169,
    'https://image.tmdb.org/t/p/original/dcKfD8xWf8XnS3tHVp7v331wdNT.jpg',
    'https://image.tmdb.org/t/p/original/9oguZXYDMH6X4LOOLiXFf1EtQzP.jpg'),

    ('El rey león', 'Simba, un joven león, debe superar sus miedos para reclamar su lugar como rey tras la trágica muerte de su padre en la sabana africana.',
    'Jon Favreau',
    'Chiwetel Ejiofor, John Oliver, Donald Glover, James Earl Jones, John Kani, Alfre Woodard, Beyoncé, JD McCrary, Shahadi Wright Joseph',
    'Aventura, Drama, Familia', 'Todos los públicos', 88,
    'https://image.tmdb.org/t/p/original/8zkIFKjfajIzTo9U0jDTf2spCzl.jpg',
    'https://image.tmdb.org/t/p/original/2XWhIg0aWX83ntm5Oq8w15vfB9c.jpg'),

    ('Avatar', 'Jake Sully, un exmarine parapléjico, se infiltra en un mundo alienígena y lucha por proteger a sus habitantes de los intereses humanos.',
    'James Cameron',
    'Sam Worthington, Zoë Saldaña, Sigourney Weaver, Stephen Lang, Michelle Rodríguez, Giovanni Ribisi, Joel David Moore, CCH Pounder, Wes Studi',
    'Acción, Aventura, Fantasía, Ciencia ficción', '7', 162,
    'https://image.tmdb.org/t/p/original/tXmTHdrZgNsULqCbThK2Dt2X9Wt.jpg',
    'https://image.tmdb.org/t/p/original/tvaFREoQ7ssrPcwfz7Xbj2A7B2t.jpg'),

    ('Jurassic Park', 'En un parque de dinosaurios, el caos surge cuando fallan las medidas de seguridad y los depredadores cazan humanos.',
    'Steven Spielberg',
    'Sam Neill, Laura Dern, Jeff Goldblum, Richard Attenborough, Bob Peck, Martin Ferrero, BD Wong, Joseph Mazzello, Ariana Richards',
    'Aventura, Ciencia ficción', '12', 127,
    'https://image.tmdb.org/t/p/original/1r8TWaAExHbFRzyqT3Vcbq1XZQb.jpg',
    'https://image.tmdb.org/t/p/original/mzFjAVLdZAD6UDT5BMRItHL5IVf.jpg'),

    ('El silencio de los corderos', 'Clarice Starling, agente del FBI, busca la ayuda del astuto Hannibal Lecter para capturar a un asesino en serie que aterroriza la región.',
    'Jonathan Demme',
    'Jodie Foster, Anthony Hopkins, Scott Glenn, Ted Levine, Anthony Heald, Brooke Smith, Diane Baker, Kasi Lemmons, Frankie Faison',
    'Crimen, Drama, Suspense', '18', 118,
    'https://image.tmdb.org/t/p/original/8FdQQ3cUCs9goEOr1qUFaHackoJ.jpg',
    'https://image.tmdb.org/t/p/original/mW2hISHs00yihyYQBqahGYoCcHy.jpg'),

    ('La lista de Schindler', 'Oskar Schindler salva a miles de judíos durante el Holocausto empleándolos en su fábrica y arriesgando todo por la humanidad.',
    'Steven Spielberg',
    'Liam Neeson, Ben Kingsley, Ralph Fiennes, Caroline Goodall, Jonathan Sagall, Embeth Davidtz, Malgorzata Gebel, Shmuel Levy, Mark Ivanir',
    'Drama, Historia, Bélica', '12', 195,
    'https://image.tmdb.org/t/p/original/xnvHaMFNXzemoH4uXHDMtKnpF7l.jpg',
    'https://image.tmdb.org/t/p/original/zb6fM1CX41D9rF9hdgclu0peUmy.jpg'),

    ('Regreso al futuro', 'Marty McFly viaja accidentalmente al pasado y debe asegurarse que sus padres se conozcan para proteger su propia existencia.',
    'Robert Zemeckis',
    'Michael J. Fox, Christopher Lloyd, Crispin Glover, Lea Thompson, Claudia Wells, Thomas F. Wilson, Marc McClure, Wendie Jo Sperber, George DiCenzo',
    'Aventura, Comedia, Ciencia ficción', 'Todos los públicos', 116,
    'https://image.tmdb.org/t/p/original/qqWOr3lDYZvywoP7PeFwp8u1NVv.jpg',
    'https://image.tmdb.org/t/p/original/hxSB02ksqnkXY4hPGAXqgO2fL01.jpg'),

    ('La guerra de las galaxias', 'Luke Skywalker, un joven granjero, se une a un grupo de rebeldes en su lucha contra el Imperio Galáctico y su gran arma, la Estrella de la Muerte.',
    'George Lucas',
    'Mark Hamill, Harrison Ford, Carrie Fisher, Peter Cushing, Alec Guinness, Anthony Daniels, Kenny Baker, Peter Mayhew, David Prowse',
    'Aventura, Acción, Ciencia ficción', '7', 121,
    'https://image.tmdb.org/t/p/original/hkHIcwbe39ywsT3CeJcFZT1RozG.jpg',
    'https://image.tmdb.org/t/p/original/4rjkUgFPDrQEOr1NnZJ5FSKzCIX.jpg'),

    ('La milla verde', 'Un guardia de prisión descubre que un preso en el corredor de la muerte tiene un don especial, cuestionando la justicia de su ejecución.',
    'Frank Darabont',
    'Tom Hanks, David Morse, Bonnie Hunt, Michael Clarke Duncan, James Cromwell, Michael Jeter, Graham Greene, Doug Hutchison, Sam Rockwell',
    'Fantasía, Drama, Crimen', '12', 189,
    'https://image.tmdb.org/t/p/original/94lRG8bVMnbi3VRS8UXAhlPmaxP.jpg',
    'https://image.tmdb.org/t/p/original/amZavErrjrdgDwhsIdpWxHNenIx.jpg'),

    ('Black Hawk Derribado', 'Soldados atrapados en Mogadiscio tras la caída de dos helicópteros luchan por sobrevivir contra fuerzas hostiles.',
    'Ridley Scott',
    'Josh Hartnett, Eric Bana, Ewan McGregor, Tom Sizemore, William Fichtner, Sam Shepard, Jason Isaacs, Ewen Bremner, Orlando Bloom',
    'Acción, Bélica, Historia', '12', 144,
    'https://image.tmdb.org/t/p/original/5chWdaHyaHWd5KaVxK94lbTDCdC.jpg',
    'https://image.tmdb.org/t/p/original/o6f1sJ0ZYg6TA2tMgoHdupB68QB.jpg');
