﻿USE [master]
GO
/****** Object:  Database [Vision]    Script Date: 23.06.2017 12:21:38 ******/
CREATE DATABASE [Vision]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AppFx', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\AppFx.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'AppFx_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\AppFx_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Vision] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Vision].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Vision] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Vision] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Vision] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Vision] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Vision] SET ARITHABORT OFF 
GO
ALTER DATABASE [Vision] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Vision] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Vision] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Vision] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Vision] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Vision] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Vision] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Vision] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Vision] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Vision] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Vision] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Vision] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Vision] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Vision] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Vision] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Vision] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Vision] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Vision] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Vision] SET  MULTI_USER 
GO
ALTER DATABASE [Vision] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Vision] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Vision] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Vision] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Vision] SET DELAYED_DURABILITY = DISABLED 
GO
USE [Vision]
GO
/****** Object:  Table [dbo].[Dummy]    Script Date: 23.06.2017 12:21:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dummy](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
 CONSTRAINT [PK_Dummy] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IdentityClaim]    Script Date: 23.06.2017 12:21:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentityClaim](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Type] [nvarchar](256) NOT NULL,
	[Value] [nvarchar](256) NULL,
 CONSTRAINT [PK_IdentityClaim] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IdentityRole]    Script Date: 23.06.2017 12:21:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentityRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Identifier] [uniqueidentifier] NOT NULL,
	[ApplicationId] [int] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[LoweredName] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](256) NULL,
 CONSTRAINT [PK_IdentityRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IdentityUser]    Script Date: 23.06.2017 12:21:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentityUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationId] [int] NOT NULL,
	[Identifier] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Identifier]  DEFAULT (newsequentialid()),
	[Name] [nvarchar](256) NOT NULL,
	[LoweredUserName] [nvarchar](256) NOT NULL,
	[MobileAlias] [nvarchar](16) NULL,
	[IsAnonymous] [bit] NOT NULL,
	[LastActivityDate] [datetime] NOT NULL,
	[PasswordHash] [nvarchar](256) NULL,
	[SecurityStamp] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NOT NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[Phone] [nvarchar](256) NULL,
	[PhoneConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
 CONSTRAINT [PK_IdentityUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IdentityUserLogin]    Script Date: 23.06.2017 12:21:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentityUserLogin](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProviderName] [nvarchar](255) NOT NULL,
	[ProviderKey] [nvarchar](45) NOT NULL,
	[ProviderDisplayName] [nvarchar](255) NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_IdentityUserLogin] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IdentityUserRole]    Script Date: 23.06.2017 12:21:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentityUserRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Index [IX_IdentityUser]    Script Date: 23.06.2017 12:21:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_IdentityUser] ON [dbo].[IdentityUser]
(
	[Identifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_IdentityUser_1]    Script Date: 23.06.2017 12:21:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_IdentityUser_1] ON [dbo].[IdentityUser]
(
	[LoweredUserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_IdentityUserLogin]    Script Date: 23.06.2017 12:21:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_IdentityUserLogin] ON [dbo].[IdentityUserLogin]
(
	[ProviderName] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IdentityClaim]  WITH CHECK ADD  CONSTRAINT [FK_IdentityClaim_IdentityUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[IdentityUser] ([Id])
GO
ALTER TABLE [dbo].[IdentityClaim] CHECK CONSTRAINT [FK_IdentityClaim_IdentityUser]
GO
ALTER TABLE [dbo].[IdentityUserLogin]  WITH CHECK ADD  CONSTRAINT [FK_IdentityUserLogin_IdentityUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[IdentityUser] ([Identifier])
GO
ALTER TABLE [dbo].[IdentityUserLogin] CHECK CONSTRAINT [FK_IdentityUserLogin_IdentityUser]
GO
ALTER TABLE [dbo].[IdentityUserRole]  WITH CHECK ADD  CONSTRAINT [FK_IdentityUserRole_IdentityRole] FOREIGN KEY([RoleId])
REFERENCES [dbo].[IdentityRole] ([Id])
GO
ALTER TABLE [dbo].[IdentityUserRole] CHECK CONSTRAINT [FK_IdentityUserRole_IdentityRole]
GO
ALTER TABLE [dbo].[IdentityUserRole]  WITH CHECK ADD  CONSTRAINT [FK_IdentityUserRole_IdentityUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[IdentityUser] ([Id])
GO
ALTER TABLE [dbo].[IdentityUserRole] CHECK CONSTRAINT [FK_IdentityUserRole_IdentityUser]
GO
USE [master]
GO
ALTER DATABASE [Vision] SET  READ_WRITE 
GO
