
GO

/****** Object:  Table [dbo].[AdviserLOB]    Script Date: 06/11/2009 10:49:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[AdviserLOB](
	[AL_LOBId] [int] IDENTITY(1000,1) NOT NULL,
	[A_AdviserId] [int] NOT NULL,
	[XALC_LOBClassificationCode] [varchar](5) NOT NULL,
	[XALIT_IdentifierTypeCode] [varchar](5) NULL,
	[AL_OrgName] [varchar](25) NOT NULL,
	[AL_Identifier] [varchar](25) NOT NULL,
	[AL_LicenseNo] [varchar](50) NULL,
	[AL_Validity] [datetime] NULL,
	[AL_CreatedBy] [int] NOT NULL,
	[AL_CreatedOn] [datetime] NOT NULL,
	[AL_ModifiedBy] [int] NOT NULL,
	[AL_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_AdvisorLOB] PRIMARY KEY CLUSTERED 
(
	[AL_LOBId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Advisor LOB Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AdviserLOB'
GO

ALTER TABLE [dbo].[AdviserLOB]  WITH CHECK ADD  CONSTRAINT [FK_AdviserLOB_XMLAdviserLOBClassification] FOREIGN KEY([XALC_LOBClassificationCode])
REFERENCES [dbo].[XMLAdviserLOBClassification] ([XALC_LOBClassificationCode])
GO

ALTER TABLE [dbo].[AdviserLOB] CHECK CONSTRAINT [FK_AdviserLOB_XMLAdviserLOBClassification]
GO

ALTER TABLE [dbo].[AdviserLOB]  WITH CHECK ADD  CONSTRAINT [FK_AdviserLOB_XMLAdviserLOBIdentifierType] FOREIGN KEY([XALIT_IdentifierTypeCode])
REFERENCES [dbo].[XMLAdviserLOBIdentifierType] ([XALIT_IdentifierTypeCode])
GO

ALTER TABLE [dbo].[AdviserLOB] CHECK CONSTRAINT [FK_AdviserLOB_XMLAdviserLOBIdentifierType]
GO

ALTER TABLE [dbo].[AdviserLOB]  WITH CHECK ADD  CONSTRAINT [FK_AdvisorLOB_AdvisorMaster] FOREIGN KEY([A_AdviserId])
REFERENCES [dbo].[Adviser] ([A_AdviserId])
GO

ALTER TABLE [dbo].[AdviserLOB] CHECK CONSTRAINT [FK_AdvisorLOB_AdvisorMaster]
GO


