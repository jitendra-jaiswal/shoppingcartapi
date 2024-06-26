USE [ecommerce]
GO
/****** Object:  Table [dbo].[DiscountDetails]    Script Date: 11/05/2024 05:12:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DiscountDetails](
	[Id] [int] IDENTITY(10001,1) NOT NULL,
	[ProductCode] [varchar](50) NULL,
	[CategoryCode] [int] NULL,
	[PercentageDiscount] [int] NULL,
	[FixedDiscount] [decimal](10, 2) NULL,
	[FixedPrice] [decimal](10, 2) NULL,
	[MaxDiscount] [decimal](10, 2) NULL,
	[FreeItem] [varchar](50) NULL,
	[MinimumQuantity] [int] NULL,
	[OnItem] [varchar](50) NULL,
	[Condition] [nvarchar](500) NULL,
	[LimitCheckout] [int] NULL,
	[LimitforPeriod] [int] NULL,
	[Special] [varchar](200) NULL,
 CONSTRAINT [PK_DiscountDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[DiscountDetails] ON 

INSERT [dbo].[DiscountDetails] ([Id], [ProductCode], [CategoryCode], [PercentageDiscount], [FixedDiscount], [FixedPrice], [MaxDiscount], [FreeItem], [MinimumQuantity], [OnItem], [Condition], [LimitCheckout], [LimitforPeriod], [Special]) VALUES (10001, N'CF1', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Buy-One-Get-One Free')
INSERT [dbo].[DiscountDetails] ([Id], [ProductCode], [CategoryCode], [PercentageDiscount], [FixedDiscount], [FixedPrice], [MaxDiscount], [FreeItem], [MinimumQuantity], [OnItem], [Condition], [LimitCheckout], [LimitforPeriod], [Special]) VALUES (10002, N'AP1', NULL, NULL, NULL, CAST(4.50 AS Decimal(10, 2)), NULL, NULL, 3, N'AP1', NULL, NULL, NULL, N'Buy 3 or More bags at 4.50$')
INSERT [dbo].[DiscountDetails] ([Id], [ProductCode], [CategoryCode], [PercentageDiscount], [FixedDiscount], [FixedPrice], [MaxDiscount], [FreeItem], [MinimumQuantity], [OnItem], [Condition], [LimitCheckout], [LimitforPeriod], [Special]) VALUES (10003, N'CH1', NULL, NULL, NULL, NULL, NULL, N'MK1', 1, NULL, NULL, 1, NULL, N'Buy 1 Kg Chicken Get 1 Milk Free')
INSERT [dbo].[DiscountDetails] ([Id], [ProductCode], [CategoryCode], [PercentageDiscount], [FixedDiscount], [FixedPrice], [MaxDiscount], [FreeItem], [MinimumQuantity], [OnItem], [Condition], [LimitCheckout], [LimitforPeriod], [Special]) VALUES (10004, N'OM1', NULL, 50, NULL, NULL, NULL, NULL, 1, N'AP1', NULL, 1, NULL, N'Buy 1 Oatmeal Get 50% off on 1 Apple')
INSERT [dbo].[DiscountDetails] ([Id], [ProductCode], [CategoryCode], [PercentageDiscount], [FixedDiscount], [FixedPrice], [MaxDiscount], [FreeItem], [MinimumQuantity], [OnItem], [Condition], [LimitCheckout], [LimitforPeriod], [Special]) VALUES (10005, N'OM1', NULL, 40, NULL, NULL, NULL, NULL, 3, N'MK1', NULL, NULL, NULL, N'Buy 3 Oatmeals get Milk at 40%')
SET IDENTITY_INSERT [dbo].[DiscountDetails] OFF
GO
