USE [MyDemo]
GO
/****** Object:  Table [dbo].[User_Role]    Script Date: 06/27/2021 17:20:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [int] NOT NULL,
	[role_id] [int] NOT NULL,
	[status] [int] NOT NULL,
 CONSTRAINT [PK_User_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[User_Role] ON
INSERT [dbo].[User_Role] ([Id], [Uid], [role_id], [status]) VALUES (6, 8, 13, 1)
INSERT [dbo].[User_Role] ([Id], [Uid], [role_id], [status]) VALUES (10, 9, 16, 1)
SET IDENTITY_INSERT [dbo].[User_Role] OFF
/****** Object:  Table [dbo].[User]    Script Date: 06/27/2021 17:20:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[is_admin] [tinyint] NOT NULL,
	[status] [nvarchar](250) NOT NULL,
	[email] [nvarchar](250) NULL,
	[mobile_phone] [nvarchar](250) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[User] ON
INSERT [dbo].[User] ([ID], [Name], [Password], [is_admin], [status], [email], [mobile_phone]) VALUES (1, N'Jack', N'E1ADC3949BA59ABBE56E057F2F883E', 1, N'1', N'123456789@163.com', N'15151989757')
INSERT [dbo].[User] ([ID], [Name], [Password], [is_admin], [status], [email], [mobile_phone]) VALUES (8, N'Elsa', N'E1ADC3949BA59ABBE56E057F2F883E', 0, N'1', N'2714000135@qq.com', N'15151989787')
INSERT [dbo].[User] ([ID], [Name], [Password], [is_admin], [status], [email], [mobile_phone]) VALUES (9, N'Chen', N'E1ADC3949BA59ABBE56E057F2F883E', 0, N'1', N'2714000134@qq.com', N'15151989777')
SET IDENTITY_INSERT [dbo].[User] OFF
/****** Object:  Table [dbo].[Role_Access]    Script Date: 06/27/2021 17:20:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role_Access](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Role_Id] [int] NOT NULL,
	[Access_Id] [int] NOT NULL,
	[status] [int] NOT NULL,
 CONSTRAINT [PK_Role_Access] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Role_Access] ON
INSERT [dbo].[Role_Access] ([Id], [Role_Id], [Access_Id], [status]) VALUES (98, 13, 1, 0)
INSERT [dbo].[Role_Access] ([Id], [Role_Id], [Access_Id], [status]) VALUES (99, 13, 2, 0)
INSERT [dbo].[Role_Access] ([Id], [Role_Id], [Access_Id], [status]) VALUES (100, 13, 3, 0)
INSERT [dbo].[Role_Access] ([Id], [Role_Id], [Access_Id], [status]) VALUES (101, 13, 4, 1)
INSERT [dbo].[Role_Access] ([Id], [Role_Id], [Access_Id], [status]) VALUES (102, 13, 5, 1)
INSERT [dbo].[Role_Access] ([Id], [Role_Id], [Access_Id], [status]) VALUES (103, 13, 6, 1)
INSERT [dbo].[Role_Access] ([Id], [Role_Id], [Access_Id], [status]) VALUES (104, 13, 7, 1)
INSERT [dbo].[Role_Access] ([Id], [Role_Id], [Access_Id], [status]) VALUES (105, 13, 8, 1)
INSERT [dbo].[Role_Access] ([Id], [Role_Id], [Access_Id], [status]) VALUES (106, 13, 9, 0)
INSERT [dbo].[Role_Access] ([Id], [Role_Id], [Access_Id], [status]) VALUES (107, 13, 31, 0)
INSERT [dbo].[Role_Access] ([Id], [Role_Id], [Access_Id], [status]) VALUES (108, 13, 32, 0)
INSERT [dbo].[Role_Access] ([Id], [Role_Id], [Access_Id], [status]) VALUES (109, 13, 33, 1)
INSERT [dbo].[Role_Access] ([Id], [Role_Id], [Access_Id], [status]) VALUES (110, 13, 34, 1)
INSERT [dbo].[Role_Access] ([Id], [Role_Id], [Access_Id], [status]) VALUES (137, 16, 1, 0)
INSERT [dbo].[Role_Access] ([Id], [Role_Id], [Access_Id], [status]) VALUES (138, 16, 2, 0)
INSERT [dbo].[Role_Access] ([Id], [Role_Id], [Access_Id], [status]) VALUES (139, 16, 3, 0)
INSERT [dbo].[Role_Access] ([Id], [Role_Id], [Access_Id], [status]) VALUES (140, 16, 4, 0)
INSERT [dbo].[Role_Access] ([Id], [Role_Id], [Access_Id], [status]) VALUES (141, 16, 5, 0)
INSERT [dbo].[Role_Access] ([Id], [Role_Id], [Access_Id], [status]) VALUES (142, 16, 6, 0)
INSERT [dbo].[Role_Access] ([Id], [Role_Id], [Access_Id], [status]) VALUES (143, 16, 7, 0)
INSERT [dbo].[Role_Access] ([Id], [Role_Id], [Access_Id], [status]) VALUES (144, 16, 8, 0)
INSERT [dbo].[Role_Access] ([Id], [Role_Id], [Access_Id], [status]) VALUES (145, 16, 9, 0)
INSERT [dbo].[Role_Access] ([Id], [Role_Id], [Access_Id], [status]) VALUES (146, 16, 31, 1)
INSERT [dbo].[Role_Access] ([Id], [Role_Id], [Access_Id], [status]) VALUES (147, 16, 32, 1)
INSERT [dbo].[Role_Access] ([Id], [Role_Id], [Access_Id], [status]) VALUES (148, 16, 33, 0)
INSERT [dbo].[Role_Access] ([Id], [Role_Id], [Access_Id], [status]) VALUES (149, 16, 34, 0)
INSERT [dbo].[Role_Access] ([Id], [Role_Id], [Access_Id], [status]) VALUES (150, 17, 1, 0)
INSERT [dbo].[Role_Access] ([Id], [Role_Id], [Access_Id], [status]) VALUES (151, 17, 2, 0)
INSERT [dbo].[Role_Access] ([Id], [Role_Id], [Access_Id], [status]) VALUES (152, 17, 3, 0)
INSERT [dbo].[Role_Access] ([Id], [Role_Id], [Access_Id], [status]) VALUES (153, 17, 4, 0)
INSERT [dbo].[Role_Access] ([Id], [Role_Id], [Access_Id], [status]) VALUES (154, 17, 5, 0)
INSERT [dbo].[Role_Access] ([Id], [Role_Id], [Access_Id], [status]) VALUES (155, 17, 6, 0)
INSERT [dbo].[Role_Access] ([Id], [Role_Id], [Access_Id], [status]) VALUES (156, 17, 7, 0)
INSERT [dbo].[Role_Access] ([Id], [Role_Id], [Access_Id], [status]) VALUES (157, 17, 8, 0)
INSERT [dbo].[Role_Access] ([Id], [Role_Id], [Access_Id], [status]) VALUES (158, 17, 9, 0)
INSERT [dbo].[Role_Access] ([Id], [Role_Id], [Access_Id], [status]) VALUES (159, 17, 31, 0)
INSERT [dbo].[Role_Access] ([Id], [Role_Id], [Access_Id], [status]) VALUES (160, 17, 32, 0)
INSERT [dbo].[Role_Access] ([Id], [Role_Id], [Access_Id], [status]) VALUES (161, 17, 33, 0)
INSERT [dbo].[Role_Access] ([Id], [Role_Id], [Access_Id], [status]) VALUES (162, 17, 34, 0)
SET IDENTITY_INSERT [dbo].[Role_Access] OFF
/****** Object:  Table [dbo].[Role]    Script Date: 06/27/2021 17:20:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Status] [tinyint] NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Role] ON
INSERT [dbo].[Role] ([Id], [Name], [Status]) VALUES (13, N'MeEngineer', 1)
INSERT [dbo].[Role] ([Id], [Name], [Status]) VALUES (16, N'PEManager', 1)
INSERT [dbo].[Role] ([Id], [Name], [Status]) VALUES (17, N'PE', 1)
SET IDENTITY_INSERT [dbo].[Role] OFF
/****** Object:  Table [dbo].[Access]    Script Date: 06/27/2021 17:20:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Access](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[authName] [nvarchar](max) NOT NULL,
	[NodeLevel] [int] NOT NULL,
	[UpId] [int] NOT NULL,
	[path] [nvarchar](255) NULL,
 CONSTRAINT [PK_Access] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Access] ON
INSERT [dbo].[Access] ([Id], [authName], [NodeLevel], [UpId], [path]) VALUES (1, N'DashBoard', 1, 0, NULL)
INSERT [dbo].[Access] ([Id], [authName], [NodeLevel], [UpId], [path]) VALUES (2, N'User Management', 1, 0, NULL)
INSERT [dbo].[Access] ([Id], [authName], [NodeLevel], [UpId], [path]) VALUES (3, N'User List', 2, 2, N'Users')
INSERT [dbo].[Access] ([Id], [authName], [NodeLevel], [UpId], [path]) VALUES (4, N'Permisson Management', 1, 0, NULL)
INSERT [dbo].[Access] ([Id], [authName], [NodeLevel], [UpId], [path]) VALUES (5, N'Permisson List', 2, 4, N'Permission')
INSERT [dbo].[Access] ([Id], [authName], [NodeLevel], [UpId], [path]) VALUES (6, N'Role List', 2, 4, N'Role')
INSERT [dbo].[Access] ([Id], [authName], [NodeLevel], [UpId], [path]) VALUES (7, N'Data Statistics', 1, 0, NULL)
INSERT [dbo].[Access] ([Id], [authName], [NodeLevel], [UpId], [path]) VALUES (8, N'Data Report', 2, 7, N'Report')
INSERT [dbo].[Access] ([Id], [authName], [NodeLevel], [UpId], [path]) VALUES (9, N'Dash Board', 2, 1, N'DashBoard')
INSERT [dbo].[Access] ([Id], [authName], [NodeLevel], [UpId], [path]) VALUES (31, N'Tools', 1, 0, N'')
INSERT [dbo].[Access] ([Id], [authName], [NodeLevel], [UpId], [path]) VALUES (32, N'Excel', 2, 31, N'Excel')
INSERT [dbo].[Access] ([Id], [authName], [NodeLevel], [UpId], [path]) VALUES (33, N'ReadButton', 3, 8, N'/api/ReadButton')
INSERT [dbo].[Access] ([Id], [authName], [NodeLevel], [UpId], [path]) VALUES (34, N'Edit', 3, 8, N'/api/EditButton')
SET IDENTITY_INSERT [dbo].[Access] OFF
/****** Object:  Default [DF_Role_Status]    Script Date: 06/27/2021 17:20:55 ******/
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Role_Status]  DEFAULT ((1)) FOR [Status]
GO
/****** Object:  Default [DF_User_is_admin]    Script Date: 06/27/2021 17:20:55 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_is_admin]  DEFAULT ((0)) FOR [is_admin]
GO
/****** Object:  Default [DF_User_status]    Script Date: 06/27/2021 17:20:55 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_status]  DEFAULT ((1)) FOR [status]
GO
