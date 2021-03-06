/****** Object:  Table [dbo].[XMLProof]    Script Date: 06/23/2009 19:03:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLProof]') AND type in (N'U'))
DROP TABLE [dbo].[XMLProof]
GO
/****** Object:  Table [dbo].[XMLProof]    Script Date: 06/23/2009 19:03:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLProof]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[XMLProof](
	[XP_ProofCode] [int] IDENTITY(1,1) NOT NULL,
	[XP_ProofName] [varchar](max) NULL,
	[XP_ProofCategory] [varchar](30) NULL,
	[XP_CreatedBy] [int] NULL,
	[XP_CreatedOn] [datetime] NULL,
	[XP_ModifiedBy] [int] NULL,
	[XP_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_XMLProof] PRIMARY KEY CLUSTERED 
(
	[XP_ProofCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
