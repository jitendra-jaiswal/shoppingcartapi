USE [ecommerce]
GO
/****** Object:  Table [dbo].[Discounts]    Script Date: 10/05/2024 09:03:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Discounts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Type] [int] NOT NULL,
	[DiscountDetails] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ExpiryDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Discounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Discounts] ON 

INSERT [dbo].[Discounts] ([Id], [Name], [Type], [DiscountDetails], [IsActive], [CreatedDate], [ExpiryDate]) VALUES (1, N'BOGO', 1, 10001, 1, CAST(N'2024-05-08T00:00:00.000' AS DateTime), CAST(N'2024-06-08T00:00:00.000' AS DateTime))
INSERT [dbo].[Discounts] ([Id], [Name], [Type], [DiscountDetails], [IsActive], [CreatedDate], [ExpiryDate]) VALUES (4, N'APPL', 2, 10002, 1, CAST(N'2024-05-08T00:00:00.000' AS DateTime), CAST(N'2024-06-08T00:00:00.000' AS DateTime))
INSERT [dbo].[Discounts] ([Id], [Name], [Type], [DiscountDetails], [IsActive], [CreatedDate], [ExpiryDate]) VALUES (6, N'CHMK', 3, 10003, 1, CAST(N'2024-05-08T00:00:00.000' AS DateTime), CAST(N'2024-06-08T00:00:00.000' AS DateTime))
INSERT [dbo].[Discounts] ([Id], [Name], [Type], [DiscountDetails], [IsActive], [CreatedDate], [ExpiryDate]) VALUES (7, N'APOM', 4, 10004, 1, CAST(N'2024-05-08T00:00:00.000' AS DateTime), CAST(N'2024-06-08T00:00:00.000' AS DateTime))
INSERT [dbo].[Discounts] ([Id], [Name], [Type], [DiscountDetails], [IsActive], [CreatedDate], [ExpiryDate]) VALUES (8, N'OMMK', 4, 10005, 1, CAST(N'2024-05-08T00:00:00.000' AS DateTime), CAST(N'2024-06-08T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Discounts] OFF
GO
ALTER TABLE [dbo].[Discounts]  WITH CHECK ADD  CONSTRAINT [FK_Discounts_DiscountDetails] FOREIGN KEY([DiscountDetails])
REFERENCES [dbo].[DiscountDetails] ([Id])
GO
ALTER TABLE [dbo].[Discounts] CHECK CONSTRAINT [FK_Discounts_DiscountDetails]
GO
ALTER TABLE [dbo].[Discounts]  WITH CHECK ADD  CONSTRAINT [FK_Discounts_DiscountType] FOREIGN KEY([Type])
REFERENCES [dbo].[DiscountType] ([Id])
GO
ALTER TABLE [dbo].[Discounts] CHECK CONSTRAINT [FK_Discounts_DiscountType]
GO
