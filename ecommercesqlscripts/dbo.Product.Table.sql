USE [ecommerce]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 11/05/2024 05:12:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(10001,1) NOT NULL,
	[ProductCode] [varchar](50) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Category] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Properties] [varchar](max) NULL,
	[UnitPrice] [decimal](10, 2) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([Id], [ProductCode], [Name], [Category], [IsActive], [Properties], [UnitPrice]) VALUES (10003, N'CH1', N'Chicken', 10004, 1, NULL, CAST(3.11 AS Decimal(10, 2)))
INSERT [dbo].[Product] ([Id], [ProductCode], [Name], [Category], [IsActive], [Properties], [UnitPrice]) VALUES (10004, N'AP1', N'Apple', 10001, 1, NULL, CAST(6.00 AS Decimal(10, 2)))
INSERT [dbo].[Product] ([Id], [ProductCode], [Name], [Category], [IsActive], [Properties], [UnitPrice]) VALUES (10005, N'CF1', N'Coffee', 10005, 1, NULL, CAST(11.22 AS Decimal(10, 2)))
INSERT [dbo].[Product] ([Id], [ProductCode], [Name], [Category], [IsActive], [Properties], [UnitPrice]) VALUES (10007, N'MK1', N'Milk', 10002, 1, NULL, CAST(4.75 AS Decimal(10, 2)))
INSERT [dbo].[Product] ([Id], [ProductCode], [Name], [Category], [IsActive], [Properties], [UnitPrice]) VALUES (10008, N'OM1', N'Oats', 10003, 1, NULL, CAST(3.65 AS Decimal(10, 2)))
INSERT [dbo].[Product] ([Id], [ProductCode], [Name], [Category], [IsActive], [Properties], [UnitPrice]) VALUES (10010, N'WM1', N'Watermelon', 10001, 1, NULL, CAST(2.74 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_ProductCategory] FOREIGN KEY([Category])
REFERENCES [dbo].[ProductCategory] ([CategoryCode])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_ProductCategory]
GO
