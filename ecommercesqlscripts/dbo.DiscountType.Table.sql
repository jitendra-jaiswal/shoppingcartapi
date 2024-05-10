USE [ecommerce]
GO
/****** Object:  Table [dbo].[DiscountType]    Script Date: 10/05/2024 09:03:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DiscountType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DiscountType] [varchar](50) NOT NULL,
 CONSTRAINT [PK_DiscountType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[DiscountType] ON 

INSERT [dbo].[DiscountType] ([Id], [DiscountType]) VALUES (1, N'BOGO')
INSERT [dbo].[DiscountType] ([Id], [DiscountType]) VALUES (2, N'PriceDrop')
INSERT [dbo].[DiscountType] ([Id], [DiscountType]) VALUES (3, N'Freeitem')
INSERT [dbo].[DiscountType] ([Id], [DiscountType]) VALUES (4, N'PercentageDiscount')
SET IDENTITY_INSERT [dbo].[DiscountType] OFF
GO
