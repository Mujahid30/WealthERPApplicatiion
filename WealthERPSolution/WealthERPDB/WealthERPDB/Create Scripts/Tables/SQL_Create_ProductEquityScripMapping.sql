USE [wealthERPQA]
GO

/****** Object:  Table [dbo].[ProductEquityScripMapping]    Script Date: 06/12/2009 18:19:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ProductEquityScripMapping](
	[PEM_ScripCode] [int] NULL,
	[PESM_IdentifierType] [varchar](30) NULL,
	[PESM_IdentifierName] [varchar](30) NULL,
	[PESM_Identifier] [varchar](25) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ProductEquityScripMapping]  WITH CHECK ADD  CONSTRAINT [FK_ProductEquityScripMapping_ProductEquityMaster] FOREIGN KEY([PEM_ScripCode])
REFERENCES [dbo].[ProductEquityMaster] ([PEM_ScripCode])
GO

ALTER TABLE [dbo].[ProductEquityScripMapping] CHECK CONSTRAINT [FK_ProductEquityScripMapping_ProductEquityMaster]
GO


