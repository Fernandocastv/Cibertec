Use master
Go

If DB_ID('BD_LIBROS') Is Not Null
    Drop Database BD_LIBROS
Go

Create Database BD_LIBROS
Go

Use BD_LIBROS
Go

Create Table Autor(
IDAutor Int Primary Key Identity,
Nombre_Autor Varchar(100))
Go

Create Table Editorial(
IDEditorial Int Primary Key Identity,
Nombre_Editorial Varchar(100))
Go

Create Table Libro(
IDLibro Int Primary Key Identity,
Titulo Varchar(100),
IDAutor Int Foreign Key References Autor,
NroPaginas Int,
IDEditorial Int Foreign Key References Editorial,
Estado Char(1))
Go

Insert Into Autor
Values ('Miguel de Cervantes Saavedra'),
       ('William Shakespeare'),
	   ('Jane Austen'),
	   ('Charles John Huffam Dickens'),
	   ('Paulo Coelho de Souza')
Go

Insert Into Editorial
Values ('El Libro de Arena'),
       ('La Puerta de los Sueños'),
	   ('El Bosque de las Letras'),
	   ('El Faro de la Lectura'),
	   ('La Voz de los Libros')
Go

Insert Into Libro
Values ('El ingenioso hidalgo don Quijote de la Mancha', 1, 1320, 1, 'A'),
       ('Romeo y Julieta', 2, 224, 2, 'A'),
	   ('Sentido y sensibilidad', 3, 352, 3, 'A'),
	   ('Oliver Twist', 4, 384, 4, 'A'),
	   ('El alquimista', 5, 224, 5, 'A'),
	   ('Los trabajos de Persiles y Segismunda', 1, 1008, 4, 'A'),
	   ('Macbeth', 2, 192, 3, 'A'),
	   ('Mansfield Park', 3, 400, 2, 'A'),
	   ('Grandes esperanzas', 4, 576, 1, 'A'),
	   ('El peregrino de Compostela', 5, 224, 5, 'A')
Go

Create Procedure sp_listaAutores
As
Select * From Autor
Go

Create Procedure sp_listaEditoriales
As
Select * From Editorial
Go

Create Procedure sp_listaLibros
As
Select L.IDLibro, L.Titulo, A.IDAutor, A.Nombre_Autor, L.NroPaginas, E.IDEditorial,
       E.Nombre_Editorial, L.NroPaginas, L.Estado
From Libro L
Join Autor A On L.IDAutor = A.IDAutor
Join Editorial E On L.IDEditorial = E.IDEditorial
Where L.Estado = 'A'
Go

Exec sp_listaLibros
Go

Create Procedure sp_registrarLibro
@Titulo Varchar (100),
@IDAutor Int,
@NroPaginas Int,
@IDEditorial Int
As
Insert Into Libro
Values (@Titulo, @IDAutor, @NroPaginas, @IDEditorial, 'A')
Go

Create Procedure sp_actualizarLibro
@IDLibro Int,
@Titulo Varchar(100),
@IDAutor Int,
@NroPaginas Int,
@IDEditorial Int
As
Update Libro
Set Titulo = @Titulo, IDAutor = @IDAutor, NroPaginas = @NroPaginas,
    IDEditorial = @IDEditorial
Where IDLibro = @IDLibro
Go

Create Procedure sp_eliminarLibro
@IDLibro Int
As
Update Libro
Set Estado = 'A'
Where IDLibro = @IDLibro
Go