
GO

/****** Object:  Table [dbo].[WerpQuestionMaster]    Script Date: 06/12/2009 18:39:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[WerpQuestionMaster](
	[WQM_QuestionId] [int] IDENTITY(1000,1) NOT NULL,
	[WQC_QCatId] [bigint] NOT NULL,
	[WQM_Question] [varchar](max) NULL,
	[WQM_AnswerType] [varchar](10) NULL,
	[WQM_Order] [int] NULL,
	[WQM_CreatedBy] [bigint] NOT NULL,
	[WQM_CreatedOn] [datetime] NOT NULL,
	[WQM_ModifiedBy] [bigint] NOT NULL,
	[WQM_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_QuestionMaster] PRIMARY KEY CLUSTERED 
(
	[WQM_QuestionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Question Master Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WerpQuestionMaster'
GO


