USE [wealthERPQA]
GO

/****** Object:  Table [dbo].[ProductMarketCapClassification]    Script Date: 06/12/2009 18:21:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ProductMarketCapClassification](
	[PMCC_MarketCapClassificationCode] [int] IDENTITY(1,1) NOT NULL,
	[PMCC_CapClassification] [varchar](20) NULL,
	[PMCC_CreatedBy] [int] NULL,
	[PMCC_CreatedOn] [datetime] NULL,
	[PMCC_ModifiedOn] [datetime] NULL,
	[PMCC_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_ProductCAPClassification] PRIMARY KEY CLUSTERED 
(
	[PMCC_MarketCapClassificationCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


