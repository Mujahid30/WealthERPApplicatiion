
GO

/****** Object:  Table [dbo].[ProductAMCSchemeMapping]    Script Date: 06/11/2009 20:27:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ProductAMCSchemeMapping](
	[PASP_SchemePlanCode] [int] NULL,
	[PASC_AMC_ExternalCode] [varchar](255) NULL,
	[PASC_AMC_ExternalType] [varchar](50) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ProductAMCSchemeMapping]  WITH CHECK ADD  CONSTRAINT [FK_ProductAMCSchemeMapping_ProductAMCSchemePlan] FOREIGN KEY([PASP_SchemePlanCode])
REFERENCES [dbo].[ProductAMCSchemePlan] ([PASP_SchemePlanCode])
GO

ALTER TABLE [dbo].[ProductAMCSchemeMapping] CHECK CONSTRAINT [FK_ProductAMCSchemeMapping_ProductAMCSchemePlan]
GO

ALTER TABLE [dbo].[ProductAMCSchemeMapping]  WITH CHECK ADD  CONSTRAINT [FK_ProductAMCSchemeMapping_ProductAMCSchemePlan1] FOREIGN KEY([PASP_SchemePlanCode])
REFERENCES [dbo].[ProductAMCSchemePlan] ([PASP_SchemePlanCode])
GO

ALTER TABLE [dbo].[ProductAMCSchemeMapping] CHECK CONSTRAINT [FK_ProductAMCSchemeMapping_ProductAMCSchemePlan1]
GO


