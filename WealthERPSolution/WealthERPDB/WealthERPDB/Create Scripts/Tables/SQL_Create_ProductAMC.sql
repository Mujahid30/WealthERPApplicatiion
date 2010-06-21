
GO

/****** Object:  Table [dbo].[ProductAMC]    Script Date: 06/11/2009 19:57:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ProductAMC](
	[PA_AMCCode] [int] IDENTITY(1,1) NOT NULL,
	[PA_AMCName] [varchar](50) NULL,
	[PA_ModifiedBy] [int] NULL,
	[PA_ModifiedOn] [datetime] NULL,
	[PA_CreatedBy] [int] NULL,
	[PA_CreatedOn] [datetime] NULL,
 CONSTRAINT [PK_ProductAMC] PRIMARY KEY CLUSTERED 
(
	[PA_AMCCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Product AMC Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductAMC'
GO


