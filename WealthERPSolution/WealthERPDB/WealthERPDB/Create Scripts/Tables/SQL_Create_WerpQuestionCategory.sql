
GO

/****** Object:  Table [dbo].[WerpQuestionCategory]    Script Date: 06/12/2009 18:37:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[WerpQuestionCategory](
	[WQC_QCatId] [bigint] NOT NULL,
	[WQC_QuestionCategory] [varchar](max) NULL,
	[WQC_CreatedBy] [bigint] NOT NULL,
	[WQC_CreatedOn] [datetime] NOT NULL,
	[WQC_ModifiedBy] [bigint] NOT NULL,
	[WQC_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_QuestionCategory] PRIMARY KEY CLUSTERED 
(
	[WQC_QCatId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Question Category Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WerpQuestionCategory'
GO


