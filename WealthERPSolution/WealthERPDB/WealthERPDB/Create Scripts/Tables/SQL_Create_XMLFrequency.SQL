/****** Object:  Table [dbo].[XMLFrequency]    Script Date: 06/23/2009 19:03:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLFrequency]') AND type in (N'U'))
DROP TABLE [dbo].[XMLFrequency]
GO
/****** Object:  Table [dbo].[XMLFrequency]    Script Date: 06/23/2009 19:03:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLFrequency]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[XMLFrequency](
	[XF_FrequencyCode] [varchar](5) NOT NULL,
	[XF_Frequency] [varchar](25) NULL,
	[XF_CreatedBy] [int] NULL,
	[XF_CreatedOn] [datetime] NULL,
	[XF_ModifiedBy] [int] NULL,
	[XF_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_WerpFrequency_XML] PRIMARY KEY CLUSTERED 
(
	[XF_FrequencyCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
