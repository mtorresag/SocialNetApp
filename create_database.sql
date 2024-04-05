Use SOCIAL

-- Tabla Usuario
CREATE TABLE Usuarios (
    UsuarioId INT PRIMARY KEY IDENTITY,
    NombreUsuario VARCHAR(100) NOT NULL,
	ApellidoUsuario VARCHAR(100) NOT NULL,
    CorreoElectronico VARCHAR(100) NOT NULL,
	FechaNacimiento DATETIME NOT NULL,
	EstadoUsuario INT NOT NULL,  --1 Activo, 2 Inactivo
    Password VARCHAR(300) NOT NULL

);

-- Tabla Publicación
CREATE TABLE Publicaciones (
    PublicacionId INT PRIMARY KEY IDENTITY,
    Contenido VARCHAR(MAX) NOT NULL,
    FechaHoraPublicacion DATETIME NOT NULL,
    UsuarioId INT NOT NULL,
	EstadoPublicacion INT NOT NULL,  --1 Activo, 2 Inactivo
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(UsuarioId)
);

-- Tabla Comentario
CREATE TABLE Comentarios (
    ComentarioId INT PRIMARY KEY IDENTITY,
    Contenido VARCHAR(MAX) NOT NULL,
    FechaHoraComentario DATETIME NOT NULL,
    UsuarioId INT NOT NULL,
    PublicacionId INT NOT NULL,
	EstadoComentario INT NOT NULL  --1 Activo, 2 Inactivo
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(UsuarioId),
    FOREIGN KEY (PublicacionId) REFERENCES Publicaciones(PublicacionId)
);

-- Tabla Amistad
CREATE TABLE EstadoAmistad (
    EstadoAmistadId INT PRIMARY KEY IDENTITY,
    Estado VARCHAR(20) NOT NULL -- Por ejemplo: Pendiente(1), Aceptada(2), Rechazada(3)
);

-- Tabla Amistad
CREATE TABLE Amistades (
    AmistadId INT PRIMARY KEY IDENTITY,
    UsuarioSolicitanteId INT NOT NULL,
    UsuarioAceptadoId INT NOT NULL,
    EstadoAmistad INT NOT NULL, -- Por ejemplo: Pendiente(1), Aceptada(2), Rechazada(3)
    FOREIGN KEY (UsuarioSolicitanteId) REFERENCES Usuarios(UsuarioId),
    FOREIGN KEY (UsuarioAceptadoId) REFERENCES Usuarios(UsuarioId),
    FOREIGN KEY (EstadoAmistad ) REFERENCES EstadoAmistad(EstadoAmistadId)
);

