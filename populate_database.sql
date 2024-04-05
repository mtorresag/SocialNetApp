Use SOCIAL

INSERT INTO Usuarios (NombreUsuario, ApellidoUsuario, CorreoElectronico, FechaNacimiento, EstadoUsuario, Password)
VALUES 
('Juan', 'Pérez', 'juan@example.com', '1990-05-15', 1,'password123'),
('María', 'Gómez', 'maria@example.com', '1988-10-25',1, 'maria123'),
('Pedro', 'Martínez', 'pedro@example.com', '1995-03-20',1, 'pedro456'),
('Ana', 'López', 'ana@example.com', '1992-07-12',1, 'ana789'),
('Luis', 'Rodríguez', 'luis@example.com', '1985-12-30',1, 'luis007'),
('Laura', 'Sánchez', 'laura@example.com', '1998-02-18',1, 'laura2022'),
('Carlos', 'García', 'carlos@example.com', '1982-09-05',1, 'carlos456'),
('Sofía', 'Hernández', 'sofia@example.com', '1993-11-08',1, 'sofia789'),
('David', 'Díaz', 'david@example.com', '1997-06-22',1, 'david2020'),
('Paula', 'Torres', 'paula@example.com', '1990-04-03',1, 'paula123');

-- Insertar datos en la tabla Publicaciones
INSERT INTO Publicaciones (Contenido, FechaHoraPublicacion, UsuarioId, EstadoPublicacion)
VALUES 
('Primer post de Juan', '2024-04-03 09:00:00', 1,1),
('Segundo post de María', '2024-04-02 15:30:00', 2,1),
('Tercer post de Pedro', '2024-04-01 12:45:00', 3,1),
('Cuarto post de Ana', '2024-03-31 18:20:00', 4,1),
('Quinto post de Luis', '2024-03-30 10:10:00', 5,1),
('Sexto post de Laura', '2024-03-29 14:00:00', 6,1),
('Séptimo post de Carlos', '2024-03-28 11:55:00', 7,1),
('Octavo post de Sofía', '2024-03-27 09:30:00', 8,1),
('Noveno post de David', '2024-03-26 16:45:00', 9,1),
('Décimo post de Paula', '2024-03-25 13:20:00', 10,1);

-- Insertar datos en la tabla Comentarios
INSERT INTO Comentarios (Contenido, FechaHoraComentario, UsuarioId, PublicacionId, EstadoComentario)
VALUES 
('Comentario en el post de Juan', '2024-04-03 09:30:00', 2, 1,1),
('Comentario en el post de María', '2024-04-02 16:00:00', 3, 2,1),
('Comentario en el post de Pedro', '2024-04-01 13:00:00', 4, 3,1),
('Comentario en el post de Ana', '2024-03-31 18:50:00', 5, 4,1),
('Comentario en el post de Luis', '2024-03-30 10:30:00', 6, 5,1),
('Comentario en el post de Laura', '2024-03-29 14:30:00', 7, 6,1),
('Comentario en el post de Carlos', '2024-03-28 12:15:00', 8, 7,1),
('Comentario en el post de Sofía', '2024-03-27 10:00:00', 9, 8,1),
('Comentario en el post de David', '2024-03-26 17:00:00', 10, 9,1),
('Comentario en el post de Paula', '2024-03-25 13:45:00', 1, 10,1);

-- Insertar datos en la tabla EstadoAmistad
INSERT INTO EstadoAmistad (Estado)
VALUES 
('Pendiente'),
('Aceptada'),
('Rechazada');

-- Insertar datos en la tabla Amistades
INSERT INTO Amistades (UsuarioSolicitanteId, UsuarioAceptadoId, EstadoAmistad)
VALUES 
(1, 2, 2),
(2, 3, 2),
(3, 4, 2),
(4, 5, 2),
(5, 6, 2),
(6, 7, 2),
(7, 8, 2),
(8, 9, 2),
(9, 10, 2),
(10, 1, 2);