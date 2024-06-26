USE [ecommerce]
GO
/****** Object:  Table [dbo].[ProductCategory]    Script Date: 11/05/2024 05:12:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCategory](
	[CategoryCode] [int] IDENTITY(10001,1) NOT NULL,
	[CategoryName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ProductCategory] PRIMARY KEY CLUSTERED 
(
	[CategoryCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ProductCategory] ON 

INSERT [dbo].[ProductCategory] ([CategoryCode], [CategoryName]) VALUES (10001, N'Fruits')
INSERT [dbo].[ProductCategory] ([CategoryCode], [CategoryName]) VALUES (10002, N'Dairy')
INSERT [dbo].[ProductCategory] ([CategoryCode], [CategoryName]) VALUES (10003, N'Grains')
INSERT [dbo].[ProductCategory] ([CategoryCode], [CategoryName]) VALUES (10004, N'Meat')
INSERT [dbo].[ProductCategory] ([CategoryCode], [CategoryName]) VALUES (10005, N'Others')
SET IDENTITY_INSERT [dbo].[ProductCategory] OFF
GO
