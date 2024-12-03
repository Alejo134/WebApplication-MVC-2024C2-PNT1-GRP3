USE [master]
GO
/** Object:  Database [CineDB]    Script Date: 2/12/2024 22:57:16 **/
CREATE DATABASE [CineDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CineDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\CineDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CineDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\CineDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [CineDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CineDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CineDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CineDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CineDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CineDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CineDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [CineDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CineDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CineDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CineDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CineDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CineDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CineDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CineDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CineDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CineDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CineDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CineDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CineDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CineDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CineDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CineDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CineDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CineDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CineDB] SET  MULTI_USER 
GO
ALTER DATABASE [CineDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CineDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CineDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CineDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CineDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CineDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [CineDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [CineDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [CineDB]
GO
/** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 2/12/2024 22:57:16 **/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/** Object:  Table [dbo].[NuevoUsuario]    Script Date: 2/12/2024 22:57:16 **/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NuevoUsuario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Usuario] [nvarchar](50) NOT NULL,
	[Contrasenia] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Puntos] [int] NOT NULL,
 CONSTRAINT [PK_NuevoUsuario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/** Object:  Table [dbo].[Peliculas]    Script Date: 2/12/2024 22:57:16 **/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Peliculas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Titulo] [nvarchar](max) NOT NULL,
	[Descripcion] [nvarchar](max) NOT NULL,
	[Clasificacion] [int] NULL,
	[NroDeSala] [int] NOT NULL,
	[Fecha] [datetime2](7) NOT NULL,
	[Precio] [float] NOT NULL,
	[CantButacas] [int] NOT NULL,
 CONSTRAINT [PK_Peliculas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/** Object:  Table [dbo].[Ventas]    Script Date: 2/12/2024 22:57:16 **/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ventas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdPelicula] [int] NOT NULL,
	[Fecha] [datetime2](7) NOT NULL,
	[CantButacas] [int] NOT NULL,
	[Total] [float] NOT NULL,
	[IDUsuario] [int] NOT NULL,
	[Promocion] [bit] NOT NULL,
 CONSTRAINT [PK_Ventas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241022011142_inicial', N'8.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241105002619_NuevaBD', N'8.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241105003228_NuevaBD2', N'8.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241105003457_NuevaBD3', N'8.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241105011011_NuevaDB8', N'8.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241105013436_NuevaDB9', N'8.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241113234442_nuevousuario', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241113235035_nuevoUsuario2', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241114131848_Venta1', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241114133651_Venta2', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241115020316_NuevaVenta', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241115023834_NuevaVenta2', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241115030100_Ventas3', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241115191920_Ventas4', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241115200235_Ventas5', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241115204233_Ventas6', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241117140421_VentaConIDUsuario1', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241117143309_AgregadoDePuntosAUsuario1', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241117144610_AgregadoDePuntosAUsuario2', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241117145934_AgregadoDePuntosAUsuario3', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241117173959_AgregadoDePuntosAUsuario4', N'8.0.11')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241117182418_AgregadoDePuntosAUsuario5', N'8.0.11')
GO
SET IDENTITY_INSERT [dbo].[NuevoUsuario] ON 
GO
INSERT [dbo].[NuevoUsuario] ([Id], [Usuario], [Contrasenia], [Email], [Puntos]) VALUES (7, N'UsuarioConPuntos', N'123456', N'usuarioConPuntos@gmail.com', 1000)
GO
SET IDENTITY_INSERT [dbo].[NuevoUsuario] OFF
GO
SET IDENTITY_INSERT [dbo].[Peliculas] ON 
GO
INSERT [dbo].[Peliculas] ([Id], [Titulo], [Descripcion], [Clasificacion], [NroDeSala], [Fecha], [Precio], [CantButacas]) VALUES (1, N'Spider-Man (2002)', N'Spider-Man (2002) es una película de superhéroes dirigida por Sam Raimi, basada en el personaje de cómic del mismo nombre creado por Stan Lee y Steve Ditko para Marvel Comics. Es la primera entrega de una trilogía de Spider-Man protagonizada por Tobey Maguire como Peter Parker / Spider-Man.', 0, 1, CAST(N'2025-01-01T18:30:00.0000000' AS DateTime2), 10000, 15)
GO
INSERT [dbo].[Peliculas] ([Id], [Titulo], [Descripcion], [Clasificacion], [NroDeSala], [Fecha], [Precio], [CantButacas]) VALUES (2, N'Spider-Man (2002)', N'Spider-Man (2002) es una película de superhéroes dirigida por Sam Raimi, basada en el personaje de cómic del mismo nombre creado por Stan Lee y Steve Ditko para Marvel Comics. Es la primera entrega de una trilogía de Spider-Man protagonizada por Tobey Maguire como Peter Parker / Spider-Man.', 0, 1, CAST(N'2025-01-08T18:30:00.0000000' AS DateTime2), 10000, 19)
GO
INSERT [dbo].[Peliculas] ([Id], [Titulo], [Descripcion], [Clasificacion], [NroDeSala], [Fecha], [Precio], [CantButacas]) VALUES (3, N'Spider-Man 2 (2004)', N'Spider-Man 2 (2004) es la secuela de Spider-Man (2002), dirigida nuevamente por Sam Raimi y protagonizada por Tobey Maguire como Peter Parker / Spider-Man. La película sigue la evolución de Peter Parker mientras lucha por equilibrar su vida como superhéroe y como persona, enfrentando nuevos desafíos tanto emocionales como físicos.', 0, 2, CAST(N'2025-01-01T18:30:00.0000000' AS DateTime2), 10000, 19)
GO
INSERT [dbo].[Peliculas] ([Id], [Titulo], [Descripcion], [Clasificacion], [NroDeSala], [Fecha], [Precio], [CantButacas]) VALUES (4, N'Spider-Man 2 (2004)', N'Spider-Man 2 (2004) es la secuela de Spider-Man (2002), dirigida nuevamente por Sam Raimi y protagonizada por Tobey Maguire como Peter Parker / Spider-Man. La película sigue la evolución de Peter Parker mientras lucha por equilibrar su vida como superhéroe y como persona, enfrentando nuevos desafíos tanto emocionales como físicos.', 0, 2, CAST(N'2025-01-08T18:30:00.0000000' AS DateTime2), 10000, 20)
GO
INSERT [dbo].[Peliculas] ([Id], [Titulo], [Descripcion], [Clasificacion], [NroDeSala], [Fecha], [Precio], [CantButacas]) VALUES (5, N'Spider-Man 3 (2007)', N'Spider-Man 3 (2007) es la tercera y última entrega de la trilogía dirigida por Sam Raimi sobre el icónico superhéroe de Marvel. Aunque esta película continúa explorando los dilemas personales de Peter Parker / Spider-Man, se distingue por su tono más oscuro y la inclusión de varios villanos, lo que llevó a una trama más compleja y, en algunos aspectos, más dividida entre acción y desarrollo de personajes.', 0, 3, CAST(N'2025-01-01T18:30:00.0000000' AS DateTime2), 10000, 20)
GO
INSERT [dbo].[Peliculas] ([Id], [Titulo], [Descripcion], [Clasificacion], [NroDeSala], [Fecha], [Precio], [CantButacas]) VALUES (6, N'Spider-Man 3 (2007)', N'Spider-Man 3 (2007) es la tercera y última entrega de la trilogía dirigida por Sam Raimi sobre el icónico superhéroe de Marvel. Aunque esta película continúa explorando los dilemas personales de Peter Parker / Spider-Man, se distingue por su tono más oscuro y la inclusión de varios villanos, lo que llevó a una trama más compleja y, en algunos aspectos, más dividida entre acción y desarrollo de personajes.', 0, 3, CAST(N'2025-01-08T18:30:00.0000000' AS DateTime2), 10000, 20)
GO
SET IDENTITY_INSERT [dbo].[Peliculas] OFF
GO
ALTER TABLE [dbo].[NuevoUsuario] ADD  DEFAULT ((0)) FOR [Puntos]
GO
ALTER TABLE [dbo].[Peliculas] ADD  DEFAULT ((0)) FOR [CantButacas]
GO
ALTER TABLE [dbo].[Ventas] ADD  DEFAULT ((0)) FOR [IDUsuario]
GO
USE [master]
GO
ALTER DATABASE [CineDB] SET  READ_WRITE 
GO