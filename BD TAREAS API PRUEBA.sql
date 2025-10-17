CREATE DATABASE classroom_docentes;
use classroom_docentes;

CREATE TABLE usuarios (
Id INT primary key auto_increment,
Nombre_Usuario varchar(50),
Apellidos_Paterno varchar(60),
Apellido_Materno varchar(60),
Nombre varchar(60),
Password_Usuario varchar(70),
Rol ENUM('USER', 'ADMIN', 'DOCENTE') DEFAULT 'USER'
);

CREATE TABLE datos_Usuario (
Id INT primary key auto_increment,
Id_Usuario INT, 
Url_Perfil TEXT,
Cantidad_Grupos INT,
foreign key (Id_Usuario) references usuarios(Id) on delete cascade
);

CREATE TABLE grupos(
Id INT primary key auto_increment,
Nombre varchar(50),
Id_Docente INT,
Semestre varchar(20),
Descripcion varchar(100),
foreign key (Id_Docente) references usuarios(Id) on delete cascade
);

CREATE TABLE alumnos_Por_Grupo (
Id INT primary key auto_increment,
Id_Alumno INT,
Id_Grupo INT,
foreign key (Id_Alumno) references usuarios(Id) on delete cascade,
foreign key (Id_Grupo) references grupos(Id) on delete cascade
);

CREATE TABLE docentes_Por_Grupo (
Id INT primary key auto_increment,
Id_Docente INT,
Id_Grupo INT,
foreign key (Id_Docente) references usuarios(Id) on delete cascade,
foreign key (Id_Grupo) references grupos(Id) on delete cascade
);

CREATE TABLE tareas ( /*TAREAS QUE DEJA UN DOCENTE*/
Id INT auto_increment primary key,
Titulo varchar(60),
Descripcion varchar(80),
FechaEntrega datetime,
Material_Adicional TEXT,
Puntuacion INT,
Id_Grupo INT,
foreign key (Id_Grupo) references grupos(Id) on delete cascade
);

CREATE TABLE tareas_Entregar ( /*TAREAS QUE SUBE UN ALUMNO*/
Id INT primary key auto_increment,
Id_Tarea INT,
Id_Alumno INT, 
Estatus ENUM('SIN ENTREGAR', 'ENTREGADO', 'RETARDADO', 'CALIFICADO') DEFAULT 'SIN ENTREGAR',
Material_Adjunto TEXT,
Puntuacion INT,
Comentarios VARCHAR(300),
foreign key (Id_Tarea) references tareas(Id) on delete cascade,
foreign key (Id_Alumno) references usuarios(Id) on delete cascade
);

CREATE TABLE asistencias_Diarias (
Id INT primary key auto_increment,
Id_Maestro INT,
IdGrupo int,
fecha datetime,
foreign key (IdGrupo) references grupos(Id) on delete cascade,
foreign key (Id_Maestro) references usuarios(Id) on delete cascade
);

CREATE TABLE asistencia_Alumnos (
Id INT primary key auto_increment,
Id_Asistencia_Dia INT,
Id_Alumno INT,
Asistencia boolean,
foreign key (Id_Alumno) references usuarios(Id) on delete cascade,
foreign key (Id_Asistencia_Dia) references asistencias_Diarias(Id) on delete cascade
);

/* INSERTS DE DATOS */
/* ALUMNOS */
INSERT INTO usuarios (Nombre_Usuario, Apellidos_Paterno, Apellido_Materno, Nombre, Password_Usuario, Rol)
VALUES
('alumno01', 'ASTORGA', 'OCHOA', 'CARLOS PAÚL', '1234', 'USER'),
('alumno02', 'VIZCARRA', 'ASTORGA', 'HECTOR PAUL', '4321', 'USER'),
('alumno03', 'ALATORRE', 'NORIS', 'JOSE JUAN', '123', 'USER'),
('alumno04', 'PARRA', 'PARRA', 'LUIS MARIO', '321', 'USER'),
('alumno05', 'RUEDA', 'LOPEZ', 'CRISTOPHER', '1111', 'USER'),
('alumno06', 'ARAQUE', 'ZAZUETA', 'VALERIA', '2222', 'USER'),
('alumno07', 'RODRIGUEZ', 'CARDENAS', 'KARIME LIZETH', '3333', 'USER'),
('alumno08', 'GARCÍA', 'GARCÍA', 'JOSÉ ANTONIO', '4444', 'USER'),
('alumno09', 'GARCIA', 'GONZALEZ', 'KRYSTIAN YAHIR', '5555', 'USER'),
('alumno10', 'GONZALEZ', 'BENITEZ', 'BRANDON ISAIAS', '6666', 'USER'),
('alumno11', 'BERNAL', 'GUTIERREZ', 'DAVID', '7777', 'USER'),
('alumno12', 'CASTAÑOS', 'QUIÑONEZ', 'SANTIAGO', '8888', 'USER'),
('alumno13', 'CÁRDENAS', 'LUGO', 'JUAN MANUEL', '9999', 'USER'),
('alumno14', 'RENDÓN', 'CASTRO', 'JOSÉ MANUEL', '1234', 'USER'),
('alumno15', 'CONTRERAS', 'VALDEZ', 'DAVID ELIAS', '4321', 'USER'),
('alumno16', 'FELIX', 'SERNA', 'DONOVAN ALEXEY', '123', 'USER'),
('alumno17', 'ZEPEDA', 'BELTRAN', 'CHRISTOPHER', '321', 'USER'),
('alumno18', 'VERDUGO', 'ECHEVERRIA', 'JESÚS ADAIR', '1111', 'USER'),
('alumno19', 'GARCIA', 'MANZO', 'ANDREA CAROLINA', '2222', 'USER'),
('alumno20', 'CORDERO', 'GARCIA', 'CARLOS JAVIER', '3333', 'USER'),
('alumno21', 'MADRID', 'LÓPEZ', 'KEVIN JARED', '4444', 'USER'),
('alumno22', 'RAMOS', 'MARQUEZ', 'JUAN ANTONIO', '5555', 'USER'),
('alumno23', 'ROMERO', 'GONZALEZ', 'LUIS ALFONSO', '6666', 'USER'),
('alumno24', 'BERNAL', 'HERNANDEZ', 'MARIELI', '7777', 'USER'),
('alumno25', 'ATIENZO', 'PONCE', 'JESSICA MICHELLE', '8888', 'USER'),
('alumno26', 'NUÑEZ', 'SANCHEZ', 'JOSE GUILLERMO', '9999', 'USER'),
('alumno27', 'OSUNA', 'VEGA', 'JESUS ABRAHAM', '1234', 'USER'),
('alumno28', 'CARVAJAL', 'VALDEZ', 'CONCEPCION', '4321', 'USER'),
('alumno29', 'GASTELUM', 'MADRID', 'MIGUEL ANGEL', '123', 'USER'),
('alumno30', 'RAMIREZ', 'ESPARZA', 'GAEL SEBASTIAN', '321', 'USER'),
('alumno31', 'OLVERA', 'PADILLA', 'ULISES AUREL', '1111', 'USER'),
('alumno32', 'MARTINEZ', 'VILLA', 'VILLA SAMUEL', '2222', 'USER'),
('alumno33', 'QUIÑONES', 'QUIÑONES', 'ALEXIS ALBERTO', '3333', 'USER'),
('alumno34', 'UREÑA', 'ARROYO', 'JOEL ALBASHI', '4444', 'USER');


UPDATE usuarios SET Rol = 'ADMIN' WHERE Id = 1;
INSERT INTO datos_Usuario (Id_Usuario, Url_Perfil, Cantidad_Grupos)
VALUES (1, NULL, 0);

INSERT INTO grupos (Nombre, Id_Docente, Semestre, Descripcion) 
VALUES ('Desarrollo de Frontend', 1, 'Semestre 1', 'Grupo enfocado en tecnologías frontend');
INSERT INTO grupos (Nombre, Id_Docente, Semestre, Descripcion) 
VALUES ('Desarrollo de Backend', 1, 'Semestre 1', 'Grupo enfocado en tecnologías backend');
INSERT INTO docentes_Por_Grupo (Id_Docente, Id_Grupo) 
VALUES (1, 1);
INSERT INTO docentes_Por_Grupo (Id_Docente, Id_Grupo) 
VALUES (1, 2);

INSERT INTO tareas (Titulo, Descripcion, FechaEntrega, Material_Adicional, Puntuacion, Id_Grupo)
VALUES ('Lectura de capítulo 3', 'Leer y resumir el capítulo 3 del libro de texto.', '2025-10-20 23:59:00', 'Libro: Fundamentos de Programación, PDF en Classroom.', 10, 1),
('Proyecto HTML básico', 'Crear una página web simple usando etiquetas básicas de HTML.', '2025-10-25 18:00:00', 'Plantilla de ejemplo disponible en el aula virtual.', 15, 1),
('Cuestionario de redes', 'Responder el cuestionario sobre topologías de red.', '2025-10-18 12:00:00', 'Enlace al cuestionario: https://forms.gle/redes123', 8, 1),
('Exposición en equipo', 'Preparar una exposición sobre inteligencia artificial.', '2025-10-30 09:00:00', 'Presentación PowerPoint (mínimo 10 diapositivas).', 20, 1);

INSERT INTO alumnos_Por_Grupo (Id_Alumno, Id_Grupo)
VALUES
(2, 1), (3, 1), (4, 1), (5, 1), (6, 1), (7, 1), (8, 1);
 
SELECT * FROM datos_Usuario WHERE Id_Usuario = 1;

SELECT grupos.Id, grupos.Nombre, grupos.Id_Docente, grupos.Semestre, grupos.Descripcion 
FROM grupos INNER JOIN docentes_Por_Grupo ON grupos.Id = docentes_Por_Grupo.Id_Grupo ORDER BY grupos.Id ASC;

/* VER TABLAS */
select * from usuarios;
select * from datos_Usuario;
select * from grupos;
select * from docentes_Por_Grupo;
select * from alumnos_Por_Grupo;
select * from tareas;
select * from tareas_Entregar;
select * from asistencias_Diarias;
select * from asistencia_Alumnos;
DROP TABLE usuarios;

DROP DATABASE classroom_docentes;