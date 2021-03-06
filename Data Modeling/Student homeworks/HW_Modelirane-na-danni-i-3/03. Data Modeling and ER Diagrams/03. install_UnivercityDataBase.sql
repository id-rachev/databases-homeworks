USE [master]
GO
/****** Object:  Database [Univercity]    Script Date: 14-Jul-13 18:04:53 ******/
CREATE DATABASE [Univercity]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Univercity', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Univercity.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Univercity_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Univercity_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Univercity] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Univercity].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Univercity] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Univercity] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Univercity] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Univercity] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Univercity] SET ARITHABORT OFF 
GO
ALTER DATABASE [Univercity] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Univercity] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [Univercity] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Univercity] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Univercity] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Univercity] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Univercity] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Univercity] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Univercity] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Univercity] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Univercity] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Univercity] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Univercity] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Univercity] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Univercity] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Univercity] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Univercity] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Univercity] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Univercity] SET RECOVERY FULL 
GO
ALTER DATABASE [Univercity] SET  MULTI_USER 
GO
ALTER DATABASE [Univercity] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Univercity] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Univercity] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Univercity] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Univercity', N'ON'
GO
USE [Univercity]
GO
/****** Object:  Table [dbo].[Courses]    Script Date: 14-Jul-13 18:04:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Courses](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[ProfessorId] [int] NOT NULL,
	[DepartmentId] [int] NOT NULL,
 CONSTRAINT [PK_Courses] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Departments]    Script Date: 14-Jul-13 18:04:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departments](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[FacultyId] [int] NOT NULL,
 CONSTRAINT [PK_departments] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Faculties]    Script Date: 14-Jul-13 18:04:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Faculties](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Faculties] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Professors]    Script Date: 14-Jul-13 18:04:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Professors](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[DepartmentId] [int] NOT NULL,
 CONSTRAINT [PK_Professors] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Students]    Script Date: 14-Jul-13 18:04:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[FacultyId] [int] NOT NULL,
 CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StudentsCourses]    Script Date: 14-Jul-13 18:04:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentsCourses](
	[StudentId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
 CONSTRAINT [PK_StudentsCourses] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC,
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Titles]    Script Date: 14-Jul-13 18:04:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Titles](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Titles_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TitlesProfessors]    Script Date: 14-Jul-13 18:04:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TitlesProfessors](
	[TitleId] [int] NOT NULL,
	[ProfessorId] [int] NOT NULL,
 CONSTRAINT [PK_TitlesProfessors] PRIMARY KEY CLUSTERED 
(
	[TitleId] ASC,
	[ProfessorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Courses] ON 

INSERT [dbo].[Courses] ([id], [name], [ProfessorId], [DepartmentId]) VALUES (1, N'Molecular Matematics', 1, 1)
INSERT [dbo].[Courses] ([id], [name], [ProfessorId], [DepartmentId]) VALUES (2, N'Practical Biology', 2, 2)
INSERT [dbo].[Courses] ([id], [name], [ProfessorId], [DepartmentId]) VALUES (3, N'Regular Math', 3, 3)
INSERT [dbo].[Courses] ([id], [name], [ProfessorId], [DepartmentId]) VALUES (4, N'Mechanics', 1, 3)
INSERT [dbo].[Courses] ([id], [name], [ProfessorId], [DepartmentId]) VALUES (5, N'Antimats', 1, 1)
SET IDENTITY_INSERT [dbo].[Courses] OFF
SET IDENTITY_INSERT [dbo].[Departments] ON 

INSERT [dbo].[Departments] ([id], [name], [FacultyId]) VALUES (1, N'SU', 1)
INSERT [dbo].[Departments] ([id], [name], [FacultyId]) VALUES (2, N'IUT', 1)
INSERT [dbo].[Departments] ([id], [name], [FacultyId]) VALUES (3, N'EZAM', 1)
SET IDENTITY_INSERT [dbo].[Departments] OFF
SET IDENTITY_INSERT [dbo].[Faculties] ON 

INSERT [dbo].[Faculties] ([id], [name]) VALUES (1, N'FA')
SET IDENTITY_INSERT [dbo].[Faculties] OFF
SET IDENTITY_INSERT [dbo].[Professors] ON 

INSERT [dbo].[Professors] ([id], [name], [DepartmentId]) VALUES (1, N'Cherkezov ', 1)
INSERT [dbo].[Professors] ([id], [name], [DepartmentId]) VALUES (2, N'Kolio Piandeto', 1)
INSERT [dbo].[Professors] ([id], [name], [DepartmentId]) VALUES (3, N'Spiro ot Nadejda', 1)
SET IDENTITY_INSERT [dbo].[Professors] OFF
SET IDENTITY_INSERT [dbo].[Students] ON 

INSERT [dbo].[Students] ([id], [name], [FacultyId]) VALUES (1, N'Moncho Donkin', 1)
INSERT [dbo].[Students] ([id], [name], [FacultyId]) VALUES (2, N'Dimo ot Kavarna', 1)
INSERT [dbo].[Students] ([id], [name], [FacultyId]) VALUES (3, N'Dora ot Zornica', 1)
SET IDENTITY_INSERT [dbo].[Students] OFF
INSERT [dbo].[StudentsCourses] ([StudentId], [CourseId]) VALUES (1, 1)
INSERT [dbo].[StudentsCourses] ([StudentId], [CourseId]) VALUES (1, 2)
INSERT [dbo].[StudentsCourses] ([StudentId], [CourseId]) VALUES (1, 3)
INSERT [dbo].[StudentsCourses] ([StudentId], [CourseId]) VALUES (2, 3)
INSERT [dbo].[StudentsCourses] ([StudentId], [CourseId]) VALUES (3, 1)
SET IDENTITY_INSERT [dbo].[Titles] ON 

INSERT [dbo].[Titles] ([id], [name]) VALUES (1, N'PhD')
INSERT [dbo].[Titles] ([id], [name]) VALUES (2, N'Academician')
INSERT [dbo].[Titles] ([id], [name]) VALUES (3, N'Senior Assistant')
SET IDENTITY_INSERT [dbo].[Titles] OFF
INSERT [dbo].[TitlesProfessors] ([TitleId], [ProfessorId]) VALUES (1, 1)
INSERT [dbo].[TitlesProfessors] ([TitleId], [ProfessorId]) VALUES (1, 2)
INSERT [dbo].[TitlesProfessors] ([TitleId], [ProfessorId]) VALUES (1, 3)
INSERT [dbo].[TitlesProfessors] ([TitleId], [ProfessorId]) VALUES (2, 1)
INSERT [dbo].[TitlesProfessors] ([TitleId], [ProfessorId]) VALUES (2, 3)
INSERT [dbo].[TitlesProfessors] ([TitleId], [ProfessorId]) VALUES (3, 2)
INSERT [dbo].[TitlesProfessors] ([TitleId], [ProfessorId]) VALUES (3, 3)
ALTER TABLE [dbo].[Courses]  WITH CHECK ADD  CONSTRAINT [FK_Courses_Departments] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Departments] ([id])
GO
ALTER TABLE [dbo].[Courses] CHECK CONSTRAINT [FK_Courses_Departments]
GO
ALTER TABLE [dbo].[Courses]  WITH CHECK ADD  CONSTRAINT [FK_Courses_Professors] FOREIGN KEY([ProfessorId])
REFERENCES [dbo].[Professors] ([id])
GO
ALTER TABLE [dbo].[Courses] CHECK CONSTRAINT [FK_Courses_Professors]
GO
ALTER TABLE [dbo].[Departments]  WITH CHECK ADD  CONSTRAINT [FK_departments_Faculties] FOREIGN KEY([FacultyId])
REFERENCES [dbo].[Faculties] ([id])
GO
ALTER TABLE [dbo].[Departments] CHECK CONSTRAINT [FK_departments_Faculties]
GO
ALTER TABLE [dbo].[Professors]  WITH CHECK ADD  CONSTRAINT [FK_Professors_Departments] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Departments] ([id])
GO
ALTER TABLE [dbo].[Professors] CHECK CONSTRAINT [FK_Professors_Departments]
GO
ALTER TABLE [dbo].[Students]  WITH CHECK ADD  CONSTRAINT [FK_Students_Faculties1] FOREIGN KEY([FacultyId])
REFERENCES [dbo].[Faculties] ([id])
GO
ALTER TABLE [dbo].[Students] CHECK CONSTRAINT [FK_Students_Faculties1]
GO
ALTER TABLE [dbo].[StudentsCourses]  WITH CHECK ADD  CONSTRAINT [FK_StudentsCourses_Courses] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Courses] ([id])
GO
ALTER TABLE [dbo].[StudentsCourses] CHECK CONSTRAINT [FK_StudentsCourses_Courses]
GO
ALTER TABLE [dbo].[StudentsCourses]  WITH CHECK ADD  CONSTRAINT [FK_StudentsCourses_Students] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Students] ([id])
GO
ALTER TABLE [dbo].[StudentsCourses] CHECK CONSTRAINT [FK_StudentsCourses_Students]
GO
ALTER TABLE [dbo].[TitlesProfessors]  WITH CHECK ADD  CONSTRAINT [FK_TitlesProfessors_Professors1] FOREIGN KEY([ProfessorId])
REFERENCES [dbo].[Professors] ([id])
GO
ALTER TABLE [dbo].[TitlesProfessors] CHECK CONSTRAINT [FK_TitlesProfessors_Professors1]
GO
ALTER TABLE [dbo].[TitlesProfessors]  WITH CHECK ADD  CONSTRAINT [FK_TitlesProfessors_Titles] FOREIGN KEY([ProfessorId])
REFERENCES [dbo].[Titles] ([id])
GO
ALTER TABLE [dbo].[TitlesProfessors] CHECK CONSTRAINT [FK_TitlesProfessors_Titles]
GO
USE [master]
GO
ALTER DATABASE [Univercity] SET  READ_WRITE 
GO
