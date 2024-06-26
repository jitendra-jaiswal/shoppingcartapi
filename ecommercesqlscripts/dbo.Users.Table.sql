USE [ecommerce]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/05/2024 05:12:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(100001,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Role] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [Name], [Password], [Role]) VALUES (100001, N'User', N'user', N'User')
INSERT [dbo].[Users] ([UserId], [Name], [Password], [Role]) VALUES (100002, N'Admin', N'admin', N'Admin')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
