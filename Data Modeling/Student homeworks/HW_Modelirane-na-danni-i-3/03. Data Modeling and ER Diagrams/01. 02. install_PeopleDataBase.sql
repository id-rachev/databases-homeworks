USE [master]
GO
/****** Object:  Database [People]    Script Date: 14-Jul-13 17:44:44 ******/
CREATE DATABASE [People]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'People', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\People.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'People_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\People_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [People] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [People].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [People] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [People] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [People] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [People] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [People] SET ARITHABORT OFF 
GO
ALTER DATABASE [People] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [People] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [People] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [People] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [People] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [People] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [People] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [People] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [People] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [People] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [People] SET  DISABLE_BROKER 
GO
ALTER DATABASE [People] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [People] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [People] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [People] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [People] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [People] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [People] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [People] SET RECOVERY FULL 
GO
ALTER DATABASE [People] SET  MULTI_USER 
GO
ALTER DATABASE [People] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [People] SET DB_CHAINING OFF 
GO
ALTER DATABASE [People] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [People] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'People', N'ON'
GO
USE [People]
GO
/****** Object:  Table [dbo].[Addresses]    Script Date: 14-Jul-13 17:44:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Addresses](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[address_text] [nvarchar](150) NOT NULL,
	[town_id] [int] NOT NULL,
 CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Continents]    Script Date: 14-Jul-13 17:44:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Continents](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Continents] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Contries]    Script Date: 14-Jul-13 17:44:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contries](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[continent_id] [int] NOT NULL,
 CONSTRAINT [PK_Contries] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[People]    Script Date: 14-Jul-13 17:44:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[People](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[first_name] [nvarchar](50) NOT NULL,
	[last_name] [nvarchar](50) NOT NULL,
	[address_id] [int] NOT NULL,
 CONSTRAINT [PK_People] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Towns]    Script Date: 14-Jul-13 17:44:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Towns](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[country_id] [int] NOT NULL,
 CONSTRAINT [PK_Towns_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Addresses] ON 

INSERT [dbo].[Addresses] ([id], [address_text], [town_id]) VALUES (2, N'Lulin', 1)
INSERT [dbo].[Addresses] ([id], [address_text], [town_id]) VALUES (1, N'Nadejda', 1)
INSERT [dbo].[Addresses] ([id], [address_text], [town_id]) VALUES (7, N'Lulin', 2)
SET IDENTITY_INSERT [dbo].[Addresses] OFF
SET IDENTITY_INSERT [dbo].[Continents] ON 

INSERT [dbo].[Continents] ([id], [name]) VALUES (8, N'Europe')
INSERT [dbo].[Continents] ([id], [name]) VALUES (9, N'Asia')
INSERT [dbo].[Continents] ([id], [name]) VALUES (10, N'SouthAmerica')
INSERT [dbo].[Continents] ([id], [name]) VALUES (11, N'NorthAmerica')
INSERT [dbo].[Continents] ([id], [name]) VALUES (12, N'Africa')
INSERT [dbo].[Continents] ([id], [name]) VALUES (13, N'Australia')
SET IDENTITY_INSERT [dbo].[Continents] OFF
SET IDENTITY_INSERT [dbo].[Contries] ON 

INSERT [dbo].[Contries] ([id], [name], [continent_id]) VALUES (1, N'Mexico', 10)
INSERT [dbo].[Contries] ([id], [name], [continent_id]) VALUES (2, N'Bulgaria', 8)
INSERT [dbo].[Contries] ([id], [name], [continent_id]) VALUES (3, N'China', 9)
SET IDENTITY_INSERT [dbo].[Contries] OFF
SET IDENTITY_INSERT [dbo].[People] ON 

INSERT [dbo].[People] ([id], [first_name], [last_name], [address_id]) VALUES (1, N'Kolio', N'Piandeto', 1)
SET IDENTITY_INSERT [dbo].[People] OFF
SET IDENTITY_INSERT [dbo].[Towns] ON 

INSERT [dbo].[Towns] ([id], [name], [country_id]) VALUES (1, N'Sofia', 2)
INSERT [dbo].[Towns] ([id], [name], [country_id]) VALUES (2, N'Varna', 2)
INSERT [dbo].[Towns] ([id], [name], [country_id]) VALUES (4, N'Mecixo City', 3)
SET IDENTITY_INSERT [dbo].[Towns] OFF
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Addresses]    Script Date: 14-Jul-13 17:44:44 ******/
ALTER TABLE [dbo].[Addresses] ADD  CONSTRAINT [IX_Addresses] UNIQUE NONCLUSTERED 
(
	[town_id] ASC,
	[address_text] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Addresses]  WITH CHECK ADD  CONSTRAINT [FK_Addresses_Towns] FOREIGN KEY([town_id])
REFERENCES [dbo].[Towns] ([id])
GO
ALTER TABLE [dbo].[Addresses] CHECK CONSTRAINT [FK_Addresses_Towns]
GO
ALTER TABLE [dbo].[Contries]  WITH CHECK ADD  CONSTRAINT [FK_Contries_Continents] FOREIGN KEY([continent_id])
REFERENCES [dbo].[Continents] ([id])
GO
ALTER TABLE [dbo].[Contries] CHECK CONSTRAINT [FK_Contries_Continents]
GO
ALTER TABLE [dbo].[People]  WITH CHECK ADD  CONSTRAINT [FK_People_Addresses] FOREIGN KEY([address_id])
REFERENCES [dbo].[Addresses] ([id])
GO
ALTER TABLE [dbo].[People] CHECK CONSTRAINT [FK_People_Addresses]
GO
ALTER TABLE [dbo].[Towns]  WITH CHECK ADD  CONSTRAINT [FK_Towns_Contries] FOREIGN KEY([country_id])
REFERENCES [dbo].[Contries] ([id])
GO
ALTER TABLE [dbo].[Towns] CHECK CONSTRAINT [FK_Towns_Contries]
GO
USE [master]
GO
ALTER DATABASE [People] SET  READ_WRITE 
GO
