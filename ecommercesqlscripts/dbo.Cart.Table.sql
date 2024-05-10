USE [ecommerce]
GO
/****** Object:  Table [dbo].[Cart]    Script Date: 10/05/2024 09:03:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cart](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ProductId] [varchar](50) NOT NULL,
	[UnitPrice] [decimal](10, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_Cart] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Cart] ON 

INSERT [dbo].[Cart] ([Id], [UserId], [ProductId], [UnitPrice], [Quantity]) VALUES (12, 100001, N'OM1', CAST(3.65 AS Decimal(10, 2)), 3)
INSERT [dbo].[Cart] ([Id], [UserId], [ProductId], [UnitPrice], [Quantity]) VALUES (13, 100001, N'MK1', CAST(4.75 AS Decimal(10, 2)), 1)
INSERT [dbo].[Cart] ([Id], [UserId], [ProductId], [UnitPrice], [Quantity]) VALUES (14, 100001, N'WM1', CAST(2.74 AS Decimal(10, 2)), 1)
SET IDENTITY_INSERT [dbo].[Cart] OFF
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD  CONSTRAINT [FK_Cart_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Cart] CHECK CONSTRAINT [FK_Cart_User]
GO
