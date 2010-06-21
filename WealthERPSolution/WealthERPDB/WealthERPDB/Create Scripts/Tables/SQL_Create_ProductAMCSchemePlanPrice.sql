USE [wealthERPQA]
GO

/****** Object:  Table [dbo].[ProductAMCSchemePlanPrice]    Script Date: 06/12/2009 17:02:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProductAMCSchemePlanPrice](
	[PSP_SchemePriceId] [int] IDENTITY(1,1) NOT NULL,
	[PASP_SchemePlanCode] [int] NULL,
	[PSP_NetAssetValue] [numeric](18, 4) NULL,
	[PSP_RepurchasePrice] [numeric](18, 4) NULL,
	[PSP_SalePrice] [numeric](18, 4) NULL,
	[PSP_PostDate] [datetime] NULL,
	[PSP_Date] [datetime] NULL,
	[PSP_CreatedBy] [int] NULL,
	[PSP_CreatedOn] [datetime] NULL,
	[PSP_ModifiedOn] [datetime] NULL,
	[PSP_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_ProductAMCSchemePlanPrice] PRIMARY KEY CLUSTERED 
(
	[PSP_SchemePriceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ProductAMCSchemePlanPrice]  WITH CHECK ADD  CONSTRAINT [FK_ProductAMCSchemePlanPrice_ProductAMCSchemePlan] FOREIGN KEY([PASP_SchemePlanCode])
REFERENCES [dbo].[ProductAMCSchemePlan] ([PASP_SchemePlanCode])
GO

ALTER TABLE [dbo].[ProductAMCSchemePlanPrice] CHECK CONSTRAINT [FK_ProductAMCSchemePlanPrice_ProductAMCSchemePlan]
GO


