
GO

/****** Object:  Table [dbo].[ProductAMCSchemePlanCorpAction]    Script Date: 06/11/2009 20:48:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProductAMCSchemePlanCorpAction](
	[PASPCA_CorpAxnId] [int] NOT NULL,
	[PASP_SchemePlanCode] [int] NULL,
	[PASPCA_CreatedBy] [int] NULL,
	[PASPCA_CreatedOn] [datetime] NULL,
	[PASPCA_ModifiedBy] [int] NULL,
	[PASPCA_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_ProductAMCCorpAction] PRIMARY KEY CLUSTERED 
(
	[PASPCA_CorpAxnId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ProductAMCSchemePlanCorpAction]  WITH CHECK ADD  CONSTRAINT [FK_ProductAMCSchemePlanCorpAction_ProductAMCSchemePlan] FOREIGN KEY([PASP_SchemePlanCode])
REFERENCES [dbo].[ProductAMCSchemePlan] ([PASP_SchemePlanCode])
GO

ALTER TABLE [dbo].[ProductAMCSchemePlanCorpAction] CHECK CONSTRAINT [FK_ProductAMCSchemePlanCorpAction_ProductAMCSchemePlan]
GO


