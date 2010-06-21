USE [wealthERPQA]
GO

/****** Object:  Table [dbo].[ProductEquityCorpAction]    Script Date: 06/12/2009 18:18:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ProductEquityCorpAction](
	[PECA_DailyCorpAxnId] [int] IDENTITY(1,1) NOT NULL,
	[PECAM_CorpAxnId] [int] NOT NULL,
	[PECA_SourceDate] [datetime] NOT NULL,
	[PEM_ScripCode1] [int] NOT NULL,
	[PEM_ScripCode2] [int] NULL,
	[PECA_EffectiveStartDate] [datetime] NULL,
	[PECA_EffectiveEndDate] [datetime] NULL,
	[PECA_ExDate] [datetime] NULL,
	[PECA_RecordDate] [datetime] NULL,
	[PECA_AnnouncementDate] [datetime] NULL,
	[PECA_Ratio1] [numeric](6, 3) NULL,
	[PECA_Ratio2] [numeric](6, 3) NULL,
	[PECA_FaceValueExisting] [numeric](10, 0) NULL,
	[PECA_FaceValueOffer] [numeric](10, 0) NULL,
	[PECA_PremiumPrice] [numeric](18, 6) NULL,
	[PECA_Remark] [varchar](100) NULL,
	[PECA_CreatedBy] [int] NULL,
	[PECA_CreatedOn] [datetime] NULL,
	[PECA_ModifiedOn] [datetime] NULL,
	[PECA_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_ProductEquityDailyCorpAction_1] PRIMARY KEY CLUSTERED 
(
	[PECA_DailyCorpAxnId] ASC,
	[PECAM_CorpAxnId] ASC,
	[PECA_SourceDate] ASC,
	[PEM_ScripCode1] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


