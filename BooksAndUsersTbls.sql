USE [SchoolDB]
GO
/****** Object:  Table [dbo].[Books]    Script Date: 2/27/2021 2:19:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Author] [nvarchar](max) NULL,
	[Title] [nvarchar](max) NOT NULL,
	[ISBN] [nvarchar](max) NULL,
	[Pages] [int] NULL,
	[UserId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2/27/2021 2:19:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[MiddleName] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
	[AdminPrivilege] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Books] ON 

INSERT [dbo].[Books] ([Id], [Author], [Title], [ISBN], [Pages], [UserId]) VALUES (1, N'Булгаков М.А.', N'Мастер и Маргарита', N'9781604448719', 256, 1)
INSERT [dbo].[Books] ([Id], [Author], [Title], [ISBN], [Pages], [UserId]) VALUES (2, N'Толстой Л.Н.', N'Война и мир', N'9785699088607', 928, 1)
INSERT [dbo].[Books] ([Id], [Author], [Title], [ISBN], [Pages], [UserId]) VALUES (3, N'Булгаков М.А.', N'Белая гвардия', N'9785389099173', 320, 2)
SET IDENTITY_INSERT [dbo].[Books] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [LastName], [FirstName], [MiddleName], [Email], [Phone], [AdminPrivilege]) VALUES (1, N'Стареев', N'Вадим', N'Евгеньевич', N'vstareev@gmail.com', N'4043243475', 1)
INSERT [dbo].[Users] ([Id], [LastName], [FirstName], [MiddleName], [Email], [Phone], [AdminPrivilege]) VALUES (2, N'Стареева', N'Наталья', N'Вадимовна', N'nstareeva@gmail.com', N'4043243475', 1)
INSERT [dbo].[Users] ([Id], [LastName], [FirstName], [MiddleName], [Email], [Phone], [AdminPrivilege]) VALUES (3, N'Глоба', N'Ирина', N'Викторовна', N'irinagloba@gmail.com', N'4043243475', 0)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
