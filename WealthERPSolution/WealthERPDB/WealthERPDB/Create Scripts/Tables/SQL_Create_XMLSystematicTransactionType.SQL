/****** Object:  Table [dbo].[XMLSystematicTransactionType]    Script Date: 06/23/2009 19:03:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLSystematicTransactionType]') AND type in (N'U'))
DROP TABLE [dbo].[XMLSystematicTransactionType]
GO
/****** Object:  Table [dbo].[XMLSystematicTransactionType]    Script Date: 06/23/2009 19:03:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLSystematicTransactionType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[XMLSystematicTransactionType](
	[XSTT_SystematicTypeCode] [varchar](5) NOT NULL,
	[XSTT_SystematicType] [varchar](30) NULL,
	[XSTT_CreatedBy] [int] NULL,
	[XSTT_CreatedOn] [datetime] NULL,
	[XSTT_ModifiedBy] [int] NULL,
	[XSTT_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_XMLSystematicTransactionType] PRIMARY KEY CLUSTERED 
(
	[XSTT_SystematicTypeCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
