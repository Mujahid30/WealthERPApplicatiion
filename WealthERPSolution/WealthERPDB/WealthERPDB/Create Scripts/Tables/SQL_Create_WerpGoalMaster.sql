USE [wealthERPQA]
GO

/****** Object:  Table [dbo].[WerpGoalMaster]    Script Date: 06/12/2009 18:24:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[WerpGoalMaster](
	[WGM_GoalId] [bigint] IDENTITY(1000,1) NOT NULL,
	[WQC_QCatId] [bigint] NOT NULL,
	[WGM_GoalName] [varchar](50) NOT NULL,
	[WGM_CreatedBy] [bigint] NOT NULL,
	[WGM_ModifiedBy] [bigint] NOT NULL,
	[WGM_CreatedOn] [datetime] NOT NULL,
	[WGM_ModifiedOn] [datetime] NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Goal Master Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WerpGoalMaster'
GO


