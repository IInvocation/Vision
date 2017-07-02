USE [master]
GO
/****** Object:  Database [Vision]    Script Date: 02.07.2017 18:48:01 ******/
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
/****** Object:  Table [dbo].[ApiResource]    Script Date: 02.07.2017 18:48:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApiResource](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[DisplayName] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Enabled] [bit] NULL,
 CONSTRAINT [PK_ApiResource] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ApiResourceClaim]    Script Date: 02.07.2017 18:48:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApiResourceClaim](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ApiResourceId] [int] NOT NULL,
	[ClaimType] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_ApiResourceClaim] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ApiResourceScope]    Script Date: 02.07.2017 18:48:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApiResourceScope](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ApiResourceId] [int] NOT NULL,
	[ScopeId] [int] NOT NULL,
 CONSTRAINT [PK_ApiResourceScope] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Client]    Script Date: 02.07.2017 18:48:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClientId] [nvarchar](255) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Secret] [nvarchar](max) NOT NULL,
	[RedirectUri] [nvarchar](max) NULL,
	[PostLogoutUri] [nvarchar](max) NULL,
	[AllowOfflineAccess] [bit] NOT NULL,
	[GrantTypes] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_VisionClient] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ClientScope]    Script Date: 02.07.2017 18:48:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientScope](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClientId] [int] NOT NULL,
	[ScopeId] [int] NOT NULL,
 CONSTRAINT [PK_ClientScope] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Dummy]    Script Date: 02.07.2017 18:48:01 ******/
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
/****** Object:  Table [dbo].[IdentityClaim]    Script Date: 02.07.2017 18:48:01 ******/
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
/****** Object:  Table [dbo].[IdentityClient]    Script Date: 02.07.2017 18:48:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentityClient](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClientId] [nvarchar](255) NOT NULL,
	[ClientName] [nvarchar](255) NOT NULL,
	[ClientSecet] [nvarchar](max) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[AllowOfflineAccess] [bit] NOT NULL,
	[AllowRememberConsent] [bit] NOT NULL,
	[AlwaysIncludeUserClaimsInIdToken] [bit] NOT NULL,
	[EnableLocalLogin] [bit] NOT NULL,
	[AbsoluteRefreshTokenLifetime] [int] NOT NULL,
	[AccessTokenLifetime] [int] NOT NULL,
	[AuthorizationCodeLifetime] [int] NOT NULL,
	[IdentityTokenLifetime] [int] NOT NULL,
	[SlidingRefreshTokenLifetime] [int] NOT NULL,
	[LogoUri] [nvarchar](max) NULL,
	[RefreshTokenExpiration] [int] NOT NULL,
	[RefreshTokenUsage] [int] NOT NULL,
 CONSTRAINT [PK_IdentityClient] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IdentityResource]    Script Date: 02.07.2017 18:48:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentityResource](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[DisplayName] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Enabled] [bit] NOT NULL,
	[Required] [bit] NOT NULL,
	[Emphasize] [bit] NOT NULL,
	[ShowInDiscoveryDocument] [bit] NOT NULL,
 CONSTRAINT [PK_IdentityResource] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IdentityResourceClaim]    Script Date: 02.07.2017 18:48:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentityResourceClaim](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdentityResourceId] [int] NOT NULL,
	[ClaimType] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_IdentityResourceClaim] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IdentityResourceScope]    Script Date: 02.07.2017 18:48:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentityResourceScope](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdentityResourceId] [int] NOT NULL,
	[ScopeId] [int] NOT NULL,
 CONSTRAINT [PK_IdentityResourceScope] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IdentityRole]    Script Date: 02.07.2017 18:48:01 ******/
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
/****** Object:  Table [dbo].[IdentityUser]    Script Date: 02.07.2017 18:48:01 ******/
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
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[LockedOutTill] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_IdentityUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IdentityUserLogin]    Script Date: 02.07.2017 18:48:01 ******/
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
/****** Object:  Table [dbo].[IdentityUserRole]    Script Date: 02.07.2017 18:48:01 ******/
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
/****** Object:  Table [dbo].[Scope]    Script Date: 02.07.2017 18:48:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Scope](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[DisplayName] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Required] [bit] NOT NULL,
	[Emphasize] [bit] NOT NULL,
	[ShowInDiscoveryDocument] [bit] NOT NULL,
 CONSTRAINT [PK_Scope] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_IdentityClient]    Script Date: 02.07.2017 18:48:01 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_IdentityClient] ON [dbo].[IdentityClient]
(
	[ClientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_IdentityClient_1]    Script Date: 02.07.2017 18:48:01 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_IdentityClient_1] ON [dbo].[IdentityClient]
(
	[ClientName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_IdentityUser]    Script Date: 02.07.2017 18:48:01 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_IdentityUser] ON [dbo].[IdentityUser]
(
	[Identifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_IdentityUser_1]    Script Date: 02.07.2017 18:48:01 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_IdentityUser_1] ON [dbo].[IdentityUser]
(
	[LoweredUserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_IdentityUserLogin]    Script Date: 02.07.2017 18:48:01 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_IdentityUserLogin] ON [dbo].[IdentityUserLogin]
(
	[ProviderName] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IdentityClient] ADD  CONSTRAINT [DF_IdentityClient_Enabled]  DEFAULT ((1)) FOR [Enabled]
GO
ALTER TABLE [dbo].[IdentityClient] ADD  CONSTRAINT [DF_IdentityClient_AllowOfflineAccess]  DEFAULT ((1)) FOR [AllowOfflineAccess]
GO
ALTER TABLE [dbo].[IdentityClient] ADD  CONSTRAINT [DF_IdentityClient_AllowRememberConsent]  DEFAULT ((1)) FOR [AllowRememberConsent]
GO
ALTER TABLE [dbo].[IdentityClient] ADD  CONSTRAINT [DF_IdentityClient_AlwaysIncludeUserClaimsInIdToken]  DEFAULT ((0)) FOR [AlwaysIncludeUserClaimsInIdToken]
GO
ALTER TABLE [dbo].[IdentityClient] ADD  CONSTRAINT [DF_IdentityClient_EnableLocalLogin]  DEFAULT ((1)) FOR [EnableLocalLogin]
GO
ALTER TABLE [dbo].[IdentityClient] ADD  CONSTRAINT [DF_IdentityClient_AbsoluteRefreshTokenLifetime]  DEFAULT ((2592000)) FOR [AbsoluteRefreshTokenLifetime]
GO
ALTER TABLE [dbo].[IdentityClient] ADD  CONSTRAINT [DF_IdentityClient_AccessTokenLifetime]  DEFAULT ((3600)) FOR [AccessTokenLifetime]
GO
ALTER TABLE [dbo].[IdentityClient] ADD  CONSTRAINT [DF_IdentityClient_AuthorizationCodeLifetime]  DEFAULT ((3600)) FOR [AuthorizationCodeLifetime]
GO
ALTER TABLE [dbo].[IdentityClient] ADD  CONSTRAINT [DF_IdentityClient_IdentityTokenLifetime]  DEFAULT ((300)) FOR [IdentityTokenLifetime]
GO
ALTER TABLE [dbo].[IdentityClient] ADD  CONSTRAINT [DF_IdentityClient_SlidingRefreshTokenLifetime]  DEFAULT ((1296000)) FOR [SlidingRefreshTokenLifetime]
GO
ALTER TABLE [dbo].[IdentityClient] ADD  CONSTRAINT [DF_IdentityClient_RefreshTokenExpiration]  DEFAULT ((0)) FOR [RefreshTokenExpiration]
GO
ALTER TABLE [dbo].[IdentityClient] ADD  CONSTRAINT [DF_IdentityClient_RefreshTokenUsage]  DEFAULT ((0)) FOR [RefreshTokenUsage]
GO
ALTER TABLE [dbo].[ApiResourceClaim]  WITH CHECK ADD  CONSTRAINT [FK_ApiResourceClaim_ApiResource] FOREIGN KEY([ApiResourceId])
REFERENCES [dbo].[ApiResource] ([Id])
GO
ALTER TABLE [dbo].[ApiResourceClaim] CHECK CONSTRAINT [FK_ApiResourceClaim_ApiResource]
GO
ALTER TABLE [dbo].[ApiResourceScope]  WITH CHECK ADD  CONSTRAINT [FK_ApiResourceScope_ApiResource] FOREIGN KEY([ApiResourceId])
REFERENCES [dbo].[ApiResource] ([Id])
GO
ALTER TABLE [dbo].[ApiResourceScope] CHECK CONSTRAINT [FK_ApiResourceScope_ApiResource]
GO
ALTER TABLE [dbo].[ApiResourceScope]  WITH CHECK ADD  CONSTRAINT [FK_ApiResourceScope_Scope] FOREIGN KEY([ScopeId])
REFERENCES [dbo].[Scope] ([Id])
GO
ALTER TABLE [dbo].[ApiResourceScope] CHECK CONSTRAINT [FK_ApiResourceScope_Scope]
GO
ALTER TABLE [dbo].[ClientScope]  WITH CHECK ADD  CONSTRAINT [FK_ClientScope_Client] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Client] ([Id])
GO
ALTER TABLE [dbo].[ClientScope] CHECK CONSTRAINT [FK_ClientScope_Client]
GO
ALTER TABLE [dbo].[ClientScope]  WITH CHECK ADD  CONSTRAINT [FK_ClientScope_Scope] FOREIGN KEY([ScopeId])
REFERENCES [dbo].[Scope] ([Id])
GO
ALTER TABLE [dbo].[ClientScope] CHECK CONSTRAINT [FK_ClientScope_Scope]
GO
ALTER TABLE [dbo].[IdentityClaim]  WITH CHECK ADD  CONSTRAINT [FK_IdentityClaim_IdentityUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[IdentityUser] ([Id])
GO
ALTER TABLE [dbo].[IdentityClaim] CHECK CONSTRAINT [FK_IdentityClaim_IdentityUser]
GO
ALTER TABLE [dbo].[IdentityResourceClaim]  WITH CHECK ADD  CONSTRAINT [FK_IdentityResourceClaim_IdentityResource] FOREIGN KEY([IdentityResourceId])
REFERENCES [dbo].[IdentityResource] ([Id])
GO
ALTER TABLE [dbo].[IdentityResourceClaim] CHECK CONSTRAINT [FK_IdentityResourceClaim_IdentityResource]
GO
ALTER TABLE [dbo].[IdentityResourceScope]  WITH CHECK ADD  CONSTRAINT [FK_IdentityResourceScope_IdentityResource] FOREIGN KEY([IdentityResourceId])
REFERENCES [dbo].[IdentityResource] ([Id])
GO
ALTER TABLE [dbo].[IdentityResourceScope] CHECK CONSTRAINT [FK_IdentityResourceScope_IdentityResource]
GO
ALTER TABLE [dbo].[IdentityResourceScope]  WITH CHECK ADD  CONSTRAINT [FK_IdentityResourceScope_Scope] FOREIGN KEY([ScopeId])
REFERENCES [dbo].[Scope] ([Id])
GO
ALTER TABLE [dbo].[IdentityResourceScope] CHECK CONSTRAINT [FK_IdentityResourceScope_Scope]
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
