/****** Object:  Table [dbo].[XMLQualification]    Script Date: 06/23/2009 19:03:43 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLQualification]') AND type in (N'U'))
DROP TABLE [dbo].[XMLQualification]
GO
/****** Object:  Table [dbo].[XMLQualification]    Script Date: 06/23/2009 19:03:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLQualification]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[XMLQualification](
	[XQ_QualificationCode] [varchar](5) NOT NULL,
	[XQ_Qualification] [varchar](30) NULL,
	[XQ_CreatedBy] [int] NULL,
	[XQ_CreatedOn] [datetime] NULL,
	[XQ_ModifiedBy] [int] NULL,
	[XQ_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_XMLQualification] PRIMARY KEY CLUSTERED 
(
	[XQ_QualificationCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
