USE [Vision]
GO

-- Insert Roles
INSERT INTO [dbo].[VisionRole]
			([Name] ,[DisplayName])
VALUES		('admin' ,'Administrator')
GO

-- Insert Claims for Roles
INSERT INTO [dbo].[RoleClaim]
			([RoleId] ,[Type] ,[Value])
VALUES		(1, 'IdentityResourceAdministrator', 'true')
GO

-- Insert Scopes
INSERT INTO [dbo].[Scope]
			([Name], [DisplayName], [Description], [Required], [Emphasize], [ShowInDiscoveryDocument])
VALUES      ('api1', 'API 1', 'Sample API', 1, 1, 1),
			('openid', 'OpenId', 'OpenId', 1, 0, 1),
			('profile', 'Profile', 'Profile', 1, 1, 1)
GO

-- Insert Test Client
INSERT INTO [dbo].[Client]
			([ClientId],[Secret])
VALUES      ('Jarvis','Test')
GO

-- Insert Client-Scopes
INSERT INTO [dbo].[ClientScope]
			([ClientId],[ScopeId])
VALUES      (1,1)
GO

-- Insert API-Resources
INSERT INTO [dbo].[ApiResource]
			([Name],[DisplayName],[Description],[Enabled])
VALUES      ('api1', 'API 1', 'Sample API', 1)
GO

-- Insert API-Resource-Scopes
INSERT INTO [dbo].[ApiResourceScope]
			([ApiResourceId],[ScopeId])
VALUES      (1,1), (1,2), (1,3)
GO

-- Insert Identity-Resources
INSERT INTO [dbo].[IdentityResource]
			([Name],[DisplayName],[Description],[Enabled],[Required],[Emphasize],[ShowInDiscoveryDocument])
VALUES      ('openid', 'OpenId', 'OpenId',1, 1, 0, 1),
			('profile', 'Profile', 'Profile', 1, 1, 1, 1)
GO

-- Insert Identity-Resource-Scopes
INSERT INTO [dbo].[IdentityResourceScope]
			([IdentityResourceId],[ScopeId])
VALUES      (1,2), (1,3)
GO