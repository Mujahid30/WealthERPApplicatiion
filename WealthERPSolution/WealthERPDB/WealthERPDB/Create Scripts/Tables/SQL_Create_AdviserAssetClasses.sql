
GO

/****** Object:  Table [dbo].[AdviserAssetClasses]    Script Date: 06/11/2009 10:46:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[AdviserAssetClasses](
	[AAC_AssetClassId] [int] IDENTITY(1000,1) NOT NULL,
	[A_AdvisorId] [int] NOT NULL,
	[AAC_AssetClassCode] [varchar](25) NOT NULL,
	[AAC_CreatedBy] [int] NOT NULL,
	[AAC_CreatedOn] [datetime] NOT NULL,
	[AAC_ModifiedBy] [int] NOT NULL,
	[AAC_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_AdvisorAssetClass] PRIMARY KEY CLUSTERED 
(
	[AAC_AssetClassId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Advisor Asset Class Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AdviserAssetClasses'
GO

ALTER TABLE [dbo].[AdviserAssetClasses]  WITH NOCHECK ADD  CONSTRAINT [FK_AdvisorAssetClass_AdvisorMaster] FOREIGN KEY([A_AdvisorId])
REFERENCES [dbo].[Adviser] ([A_AdviserId])
GO

ALTER TABLE [dbo].[AdviserAssetClasses] CHECK CONSTRAINT [FK_AdvisorAssetClass_AdvisorMaster]
GO


