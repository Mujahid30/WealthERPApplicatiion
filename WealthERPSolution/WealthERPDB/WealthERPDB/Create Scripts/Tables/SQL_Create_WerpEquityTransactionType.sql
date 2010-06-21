USE [wealthERPQA]
GO

/****** Object:  Table [dbo].[WerpEquityTransactionType]    Script Date: 06/12/2009 18:23:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[WerpEquityTransactionType](
	[WETT_TransactionCode] [tinyint] IDENTITY(1,1) NOT NULL,
	[WETT_TransactionTypeName] [varchar](30) NULL,
	[WETT_IsCorpAxn] [tinyint] NULL,
	[WETT_IsCorpAxnOffer] [tinyint] NULL,
	[WETT_IsImpactingTransaction] [tinyint] NULL,
	[WETT_UIName] [varchar](30) NULL,
	[WETT_CreatedBy] [int] NULL,
	[WETT_CreatedOn] [datetime] NULL,
	[WETT_ModifiedBy] [int] NULL,
	[WETT_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_WerpEquityTransactionType] PRIMARY KEY CLUSTERED 
(
	[WETT_TransactionCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


