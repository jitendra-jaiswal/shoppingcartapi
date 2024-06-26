USE [ecommerce]
GO
/****** Object:  Table [dbo].[Discounts]    Script Date: 11/05/2024 05:12:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Discounts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Type] [int] NOT NULL,
	[DetailsJson] [varchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ExpiryDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Discounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Discounts] ON 

INSERT [dbo].[Discounts] ([Id], [Name], [Type], [DetailsJson], [IsActive], [CreatedDate], [ExpiryDate]) VALUES (1, N'BOGO', 1, N'{"ProductCode":"CF1","CategoryCode":null,"PercentageDiscount":null,"FixedDiscount":null,"FixedPrice":null,"MaxDiscount":null,"FreeItem":null,"MinimumQuantity":null,"OnItem":null,"Condition":null,"LimitCheckout":null,"LimitforPeriod":null,"Special":"Buy-One-Get-One Free"}', 1, CAST(N'2024-05-08T00:00:00.000' AS DateTime), CAST(N'2024-06-08T00:00:00.000' AS DateTime))
INSERT [dbo].[Discounts] ([Id], [Name], [Type], [DetailsJson], [IsActive], [CreatedDate], [ExpiryDate]) VALUES (4, N'APPL', 2, N'{"ProductCode":"AP1","CategoryCode":null,"PercentageDiscount":null,"FixedDiscount":null,"FixedPrice":4.50,"MaxDiscount":null,"FreeItem":null,"MinimumQuantity":3,"OnItem":"AP1","Condition":null,"LimitCheckout":null,"LimitforPeriod":null,"Special":"Buy 3 or More bags at 4.50$"}', 1, CAST(N'2024-05-08T00:00:00.000' AS DateTime), CAST(N'2024-06-08T00:00:00.000' AS DateTime))
INSERT [dbo].[Discounts] ([Id], [Name], [Type], [DetailsJson], [IsActive], [CreatedDate], [ExpiryDate]) VALUES (6, N'CHMK', 3, N'{"ProductCode":"CH1","CategoryCode":null,"PercentageDiscount":null,"FixedDiscount":null,"FixedPrice":null,"MaxDiscount":null,"FreeItem":"MK1","MinimumQuantity":1,"OnItem":null,"Condition":null,"LimitCheckout":1,"LimitforPeriod":null,"Special":"Buy 1 Kg Chicken Get 1 Milk Free"}', 1, CAST(N'2024-05-08T00:00:00.000' AS DateTime), CAST(N'2024-06-08T00:00:00.000' AS DateTime))
INSERT [dbo].[Discounts] ([Id], [Name], [Type], [DetailsJson], [IsActive], [CreatedDate], [ExpiryDate]) VALUES (7, N'APOM', 4, N'{"ProductCode":"OM1","CategoryCode":null,"PercentageDiscount":50,"FixedDiscount":null,"FixedPrice":null,"MaxDiscount":null,"FreeItem":null,"MinimumQuantity":1,"OnItem":"AP1","Condition":null,"LimitCheckout":1,"LimitforPeriod":null,"Special":"Buy 1 Oatmeal Get 50% off on 1 Apple"}', 1, CAST(N'2024-05-08T00:00:00.000' AS DateTime), CAST(N'2024-06-08T00:00:00.000' AS DateTime))
INSERT [dbo].[Discounts] ([Id], [Name], [Type], [DetailsJson], [IsActive], [CreatedDate], [ExpiryDate]) VALUES (8, N'OMMK', 4, N'{"ProductCode":"OM1","CategoryCode":null,"PercentageDiscount":40,"FixedDiscount":null,"FixedPrice":null,"MaxDiscount":null,"FreeItem":null,"MinimumQuantity":3,"OnItem":"MK1","Condition":null,"LimitCheckout":null,"LimitforPeriod":null,"Special":"Buy 3 Oatmeals get Milk at 40%"}', 1, CAST(N'2024-05-08T00:00:00.000' AS DateTime), CAST(N'2024-06-08T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Discounts] OFF
GO
ALTER TABLE [dbo].[Discounts]  WITH CHECK ADD  CONSTRAINT [FK_Discounts_DiscountType] FOREIGN KEY([Type])
REFERENCES [dbo].[DiscountType] ([Id])
GO
ALTER TABLE [dbo].[Discounts] CHECK CONSTRAINT [FK_Discounts_DiscountType]
GO
